using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeeSharpSoft.MonoRobots.Plugin;
using System.IO;
using SeeSharpSoft.Plugin;
using SeeSharpSoft.MonoRobots.Plugin.Impl;

namespace SeeSharpSoft.MonoRobots
{
    public delegate void PlayCards(RoboPlayerPlugin sender, RoboCard[] cards, Exception exception);

	/// <summary>
	/// This class is supposed to manage a game of robots.
	/// </summary>
    public class RoboManager
    {
        public RoboManager()
        {
            //PluginManager = new RoboPluginManager();
            AvailablePlugins = new List<RoboPlayerPlugin>();
            GameState = RoboGameState.Stopped;
            Difficulty = Difficulty.Hard;
            Interaction = RoboPlayerInteraction.Ignored;
        }

        private const String PLUGIN_DIRECTORY = "Plugins";
        public IEnumerable<RoboPlayerPlugin> ActivePlayers {
            get {
                return AvailablePlugins.Where(plugin => plugin.Player != null);
            }
        }
        //public RoboPluginManager PluginManager { set; get; }
        public RoboGameState GameState { private set; get; }
        public RoboBoard Board { set; get; }
        public Difficulty Difficulty { set; get; }
        public RoboPlayerInteraction Interaction { set; get; }
        public List<RoboPlayerPlugin> AvailablePlugins { private set; get; }

        public event EventHandler<RoboManager, EventArgs<RoboGameState>> GameStateChange;
        protected virtual void OnGameStateChange(EventArgs<RoboGameState> args)
        {
            GameState = args.Value;
            GameStateChange?.Invoke(this, args);
        }

        public void LoadPlugins()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "/" + PLUGIN_DIRECTORY);
            if (!dirInfo.Exists) return;
            foreach (FileInfo file in dirInfo.GetFiles("*.dll", SearchOption.TopDirectoryOnly))
            {
                PluginHelper.RegisterPluginAssembly(file.FullName);
            }

            foreach (Type type in PluginHelper.GetRegisteredPlugins())
            {
                if (type.IsSubclassOf(typeof(RoboPlayerPlugin)))
                {
                    AvailablePlugins.Add((RoboPlayerPlugin)Activator.CreateInstance(type));
                }
            }
        }

        public RoboPlayerPlugin ActivatePlugin(RoboPlayerPlugin plugin)
        {
            if (plugin == null) return null;

            plugin.Player = new RoboPlayer();
            plugin.Player.Name = plugin.Name;
            plugin.ActivatePlugin(null);

            return plugin;
        }

        public RoboPlayerPlugin ActivatePlugin(String name)
        {
            return ActivatePlugin(AvailablePlugins.FirstOrDefault(elem => elem.Name == name));
        }

        public void DeactivatePlugin(RoboPlayerPlugin plugin)
        {
            if (plugin == null) return;

            plugin.Player = null;
            plugin.DeactivatePlugin(null);
        }

        public void DeactivatePlugin(String name)
        {
            DeactivatePlugin(AvailablePlugins.FirstOrDefault(elem => elem.Name == name));
        }

        public RoboPlayerPlugin FindActivePlugin(RoboPlayer roboplayer)
        {
            return ActivePlayers.Where(plugin => plugin.Player.Equals(roboplayer)).FirstOrDefault();
        }

        public void StartGame()
        {
            RoboCard[] pile = RoboUtils.CreateCardPile();
            StartGame(Board, pile);
        }

        public void StartGame(RoboBoard board, RoboCard[] pile)
        {
            Board = board;

            OnGameStateChange(EventArgs<RoboGameState>.create(RoboGameState.StartGame));

            foreach (RoboPlayerPlugin elem in ActivePlayers)
            {
                elem.PlayCardsCallback = RoboPlayerCallback;
                elem.StartGame(Board.CopyBoard());
                elem.Player.CardPile = new List<RoboCard>(pile);
                elem.Player.StartGame();
                elem.Player.PlayerState = RoboPlayerState.Ready;

                elem.Player.Position.Assign(Board.GetStartPosition());
            }

            OnGameStateChange(EventArgs<RoboGameState>.create(RoboGameState.Ready));
        }

        public void StartRound()
        {
            if (GameState != RoboGameState.Ready) return;

            OnGameStateChange(EventArgs<RoboGameState>.create(RoboGameState.DrawingCards));

            foreach (RoboPlayerPlugin elem in ActivePlayers)
            {
                if (elem.Player.PlayerState != RoboPlayerState.Ready) continue;

                if (!elem.Player.StartRound())
                {
                    elem.Player.PlayerState = RoboPlayerState.Dead;
                    continue;
                }

                (elem.PluginSettings as RoboPlayerPluginSettings).PlayerCollision = Interaction == RoboPlayerInteraction.Blocked;

                Action<RoboPosition, ICollection<RoboCard>, IEnumerable<RoboPosition>> pluginCaller = elem.StartRound;
                pluginCaller.BeginInvoke(
                    elem.Player.Position.Clone() as RoboPosition,
                    elem.Player.Cards,
                    ActivePlayers.Select(plugin => plugin.Player.Position.Clone() as RoboPosition),
                    null,
                    elem);

                elem.Player.PlayerState = RoboPlayerState.Thinking;
            }

            OnGameStateChange(EventArgs<RoboGameState>.create(RoboGameState.ChoosingCards));

            if (ActivePlayers.Count(elem => elem.Player.PlayerState == RoboPlayerState.Thinking) == 0)
            {
                OnGameStateChange(EventArgs<RoboGameState>.create(RoboGameState.Stopped));
            }
        }

        public bool IsChoosingCardsFinished
        {
            get
            {
                return ActivePlayers.Count(elem => elem.Player.PlayerState == RoboPlayerState.Thinking) == 0;
            }
        }

        private void DoPlayingCards()
        {
            if (GameState != RoboGameState.ChoosingCards || !IsChoosingCardsFinished) return;

            GameState = RoboGameState.PlayingCards;

            for (int i = 0; i < 5; i++)
            {
                foreach (RoboPlayerPlugin elem in ActivePlayers.OrderBy(elem => elem.Player.TimeEndRound.Ticks))
                {
                    if (elem.Player.PlayerState != RoboPlayerState.Decided) continue;
                    RoboUtils.PlayCardCore(GetBoard(Board, elem.Player, ActivePlayers.Select(plugin => plugin.Player)), elem.Player.ChosenCards[i], elem.Player.Position);
                    elem.Player.TotalPlayedCards++;

                    if (Board.GetField(elem.Player.Position).IsDestination) elem.Player.PlayerState = RoboPlayerState.Finished;
                    else if (elem.Player.Position.IsDead) elem.Player.PlayerState = RoboPlayerState.Dead;
                    else if (i == 4)
                    {
                        elem.Player.PlayerState = RoboPlayerState.Ready;
                    }
                }
            }

            GameState = RoboGameState.Ready;

            Action starter = StartRound;
            starter.BeginInvoke(null, starter);
        }

        private RoboBoard GetBoard(RoboBoard original, RoboPlayer actualPlayer, IEnumerable<RoboPlayer> players)
        {
            RoboBoard board = Board.CopyBoard();

            if (Interaction == RoboPlayerInteraction.Blocked)
            {
                foreach (RoboPlayer player in players)
                {
                    if (player == actualPlayer ||
                        board.GetField(player.Position).IsDestination ||
                        player.Position.IsDead) continue;

                    board.Fields[player.Position.X, player.Position.Y] =
                        RoboField.CreateField(FieldType.WallDown | FieldType.WallUp | FieldType.WallLeft | FieldType.WallRight);
                }
            }

            return board;
        }

        private void RoboPlayerCallback(RoboPlayerPlugin plugin, RoboCard[] chosenCards, Exception ex)
        {
            RoboPlayer player = plugin.Player;
            if (player == null || !ActivePlayers.Contains(plugin)) return;
            if (player.PlayerState != RoboPlayerState.Thinking || ex != null)
            {
                player.PlayerState = RoboPlayerState.Error;
            }
            else
            {
                if (player.EndRound(chosenCards))
                {
                    player.PlayerState = RoboPlayerState.Decided;
                } else
                {
                    player.PlayerState = RoboPlayerState.Error;
                }
            }

            Action playingCards = DoPlayingCards;
            playingCards.BeginInvoke(null, playingCards);
        }
    }

    public enum RoboGameState
    {
        Stopped = 0,
        DrawingCards = 1,
        ChoosingCards = 2,
        PlayingCards = 3,
        Ready = 4,
        StartGame = 5
    }

    public enum RoboPlayerState
    {
        Stopped,
        Ready,
        Thinking,
        Decided,
        Dead,
        Error,
        Finished
    }

    public enum RoboPlayerInteraction
    {
        Ignored,
        Blocked
        //Pushed
    }
}