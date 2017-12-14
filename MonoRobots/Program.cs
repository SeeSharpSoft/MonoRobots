using SeeSharpSoft.MonoRobots.Plugin.Impl;
using SeeSharpSoft.Plugin;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace SeeSharpSoft.MonoRobots
{
    static class Program
    {
        static AutoResetEvent autoEvent = new AutoResetEvent(false);

        enum CommandLineAction
        {
            DEFAULT,
            CREATE_DECK,
            PLAY_GAME
        }

        public static void Main(string[] args)
        { 
            switch(GetCommand(args[0]))
            {
                case CommandLineAction.CREATE_DECK:
                    CreateDeck(args[1], args.Length > 2 ? int.Parse(args[2]) : 1);
                    break;
                case CommandLineAction.PLAY_GAME:
                    PlayGame(args[1], args[2], args.Length > 3 ? (Difficulty)Enum.Parse(typeof(Difficulty), args[3]) : Difficulty.Hard);
                    break;
                case CommandLineAction.DEFAULT:
                    PlayGame(args[0], args[1], args.Length > 2 ? (Difficulty)Enum.Parse(typeof(Difficulty), args[2]) : Difficulty.Hard);
                    break;
            }
        }

        private static void PlayGame(String kiPath, String boardPath, Difficulty difficulty)
        {
            Console.WriteLine("Init RoboManager");
            RoboManager roboManager = new RoboManager();
            RoboPlayerFileIOWrapper roboPlugin = new RoboPlayerFileIOWrapper();
            roboManager.AvailablePlugins.Add(roboPlugin);
            roboManager.GameStateChange += RoboManager_GameStateChange;

            Console.WriteLine("Initialize KI: " + Directory.GetCurrentDirectory() + "/" + kiPath);
            roboManager.ActivatePlugin(roboPlugin);
            roboPlugin.ExecutablePath = Directory.GetCurrentDirectory() + "/" + kiPath;

            Console.WriteLine("Initialize Board: " + Directory.GetCurrentDirectory() + "/" + boardPath);
            RoboBoard board = new RoboBoard();
            board.Load(Directory.GetCurrentDirectory() + "/" + boardPath, difficulty);
            roboManager.Board = board;

            Console.WriteLine("Start...");
            roboManager.StartGame();
            roboManager.StartRound();

            autoEvent.WaitOne();

            Console.Write(GetResults(roboManager));

            Console.WriteLine("End...");
        }

        private static void RoboManager_GameStateChange(RoboManager sender, EventArgs<RoboGameState> e)
        {
            switch(e.Value)
            {
                case RoboGameState.Stopped:
                    autoEvent.Set();
                    break;
            }
        }

        private static CommandLineAction GetCommand(String firstArgument)
        {
            switch(firstArgument)
            {
                case "--deck":
                    return CommandLineAction.CREATE_DECK;
                case "--play":
                    return CommandLineAction.PLAY_GAME;
            }
            return CommandLineAction.DEFAULT;
        }

        private static void CreateDeck(String relativePath, int amount)
        {
            String finalPath = Directory.GetCurrentDirectory() + "/" + relativePath;
            if (!Directory.Exists(finalPath))
            {
                Directory.CreateDirectory(finalPath);
            }
            for(int i = 0; i < amount; ++i)
            {
                RoboCard[] cards = RoboUtils.CreateCardPile();
                RoboUtils.SaveCardsToFile(finalPath + "/cards" + (i + 1 + "").PadLeft((amount + "").Length, '0') + ".deck", cards);
            }
        }

        private static String GetResults(RoboManager roboManager)
        {
            StringBuilder builder = new StringBuilder("All players reached the destination or died:\r\n\r\n");
            foreach (RoboPlayer player in roboManager.ActivePlayers.Select(plugin => plugin.Player).OrderBy(elem => elem.TotalPlayedCards))
            {
                player.EndGame();
                builder.Append(player.Name.ToUpper());
                switch (player.PlayerState)
                {
                    case RoboPlayerState.Dead:
                        builder.AppendLine(" died painfully after " + player.TotalTimeElapsed.Milliseconds + " milliseconds online! (RIP)");
                        builder.AppendLine("(Elapsed rounds: " + player.Round + "; Played cards: " + player.TotalPlayedCards + ")");
                        break;
                    case RoboPlayerState.Error:
                        builder.AppendLine(" had a system error after " + player.TotalTimeElapsed.Milliseconds + " milliseconds online! (BEEEeeep)");
                        builder.AppendLine("(Elapsed rounds: " + player.Round + "; Played cards: " + player.TotalPlayedCards + ")");
                        break;
                    case RoboPlayerState.Finished:
                        builder.AppendLine(" reached the destination after " + player.TotalTimeElapsed.Milliseconds + " milliseconds online! (Yeah)");
                        builder.AppendLine("(Elapsed rounds: " + player.Round + "; Played cards: " + player.TotalPlayedCards + ")");
                        break;
                    case RoboPlayerState.Ready:
                        builder.AppendLine(" is still ready??? Someone is cheating!!!");
                        break;
                    case RoboPlayerState.Stopped:
                        builder.AppendLine(" is out of energy - a late joiner?!");
                        break;
                    case RoboPlayerState.Thinking:
                        builder.AppendLine(" is still thinking?! Hello?! Game is over?!");
                        break;
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}