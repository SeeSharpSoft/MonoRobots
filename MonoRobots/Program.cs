using SeeSharpSoft.MonoRobots.Plugin.Impl;
using SeeSharpSoft.Plugin;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using SeeSharpSoft.MonoRobots.Plugin;
using System.Collections.Generic;

namespace SeeSharpSoft.MonoRobots
{
    static class Program
    {
        static AutoResetEvent autoEvent = new AutoResetEvent(false);

        enum CommandLineAction
        {
            DEFAULT,
            CREATE_DECK,
            PLAY_GAME,
            STATS
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
                case CommandLineAction.STATS:
                    Statistics(args[1], args[2], args[3], args.Length > 4 ? (Difficulty)Enum.Parse(typeof(Difficulty), args[4]) : Difficulty.Hard);
                    break;
                case CommandLineAction.DEFAULT:
                    PlayGame(args[0], args[1], args.Length > 2 ? (Difficulty)Enum.Parse(typeof(Difficulty), args[2]) : Difficulty.Hard);
                    break;
            }
        }

        private static CommandLineAction GetCommand(String firstArgument)
        {
            switch (firstArgument)
            {
                case "--deck":
                    return CommandLineAction.CREATE_DECK;
                case "--play":
                    return CommandLineAction.PLAY_GAME;
                case "--stats":
                    return CommandLineAction.STATS;
            }
            return CommandLineAction.DEFAULT;
        }

        // MonoRobots.exe --play <relative_ki> <relative_board> <difficulty?>
        private static void PlayGame(String kiPath, String boardPath, Difficulty difficulty)
        {
            RoboManager roboManager = SetupRoboManager();
            RoboPlayerPlugin roboPlugin = SetupRoboPlayer(roboManager, kiPath);
            RoboBoard board = RoboUtils.LoadBoard(boardPath, difficulty);
 
            Console.WriteLine("Start...");

            ExecuteGame(roboManager, board, RoboUtils.CreateCardPile());

            Console.Write(GetResults(roboManager));
            Console.WriteLine("End...");
        }

        // MonoRobots.exe --stats <relative_ki> <relative_boards> <relative_decks>
        private static void Statistics(String kiPath, String boardsPath, String decksPath, Difficulty difficulty)
        {
            RoboManager roboManager = SetupRoboManager();
            RoboPlayerPlugin roboPlugin = SetupRoboPlayer(roboManager, kiPath);

            Dictionary<String, List<RoboPlayerResult>> results = new Dictionary<string, List<RoboPlayerResult>>();

            int roundsPlayed = 0;

            foreach (String boardFileName in getFileEnumerable(boardsPath))
            {
                List<RoboPlayerResult> boardStats = new List<RoboPlayerResult>();
                results.Add(boardFileName, boardStats);

                RoboBoard board = RoboUtils.LoadBoard(boardFileName, difficulty);
                foreach (String deckFileName in getFileEnumerable(decksPath))
                {
                    RoboCard[] pile = RoboUtils.LoadCardDeck(deckFileName);
                    ExecuteGame(roboManager, board, pile);
                    roundsPlayed++;

                    RoboPlayerResult singleStat = new RoboPlayerResult(roboPlugin.Player);
                    boardStats.Add(singleStat);
                }
            }

            Console.WriteLine("RoundsPlayed: " + roundsPlayed);
            Console.Write(GetFormattedStatistics(results));
        }

        private static IEnumerable<String> getFileEnumerable(String pathPattern)
        {
            String fileNamePattern = Path.GetFileName(pathPattern);
            String directory = "./" + Path.GetDirectoryName(pathPattern);
            Console.WriteLine(directory);
            Console.WriteLine(fileNamePattern);
            return Directory.EnumerateFiles(directory, fileNamePattern, SearchOption.AllDirectories);
        }

        private static RoboManager SetupRoboManager()
        {
            Console.WriteLine("Init RoboManager");
            RoboManager roboManager = new RoboManager();
            roboManager.GameStateChange += RoboManager_GameStateChange;

            return roboManager;
        }

        private static RoboPlayerPlugin SetupRoboPlayer(RoboManager roboManager, String kiExecutable)
        {
            Console.WriteLine("Initialize KI: " + Directory.GetCurrentDirectory() + "/" + kiExecutable);
            RoboPlayerPlugin roboPlugin = RoboUtils.RegisterPlugin(roboManager, kiExecutable);
            roboManager.ActivatePlugin(roboPlugin);
            return roboPlugin;
        }

        private static void ExecuteGame(RoboManager roboManager, RoboBoard board, RoboCard[] deck)
        {
            roboManager.StartGame(board, deck);
            roboManager.StartRound();
            autoEvent.WaitOne();
        }

        private static void RoboManager_GameStateChange(RoboManager sender, EventArgs<RoboGameState> e)
        {
            switch (e.Value)
            {
                case RoboGameState.Stopped:
                    autoEvent.Set();
                    break;
            }
        }

        // MonoRobots.exe --deck <relative_directory> <no_of_decks?>
        private static void CreateDeck(String relativePath, int amount)
        {
            String finalPath = Directory.GetCurrentDirectory() + "/" + relativePath;
            if (!Directory.Exists(finalPath))
            {
                Directory.CreateDirectory(finalPath);
            }
            for (int i = 0; i < amount; ++i)
            {
                RoboCard[] cards = RoboUtils.CreateCardPile();
                RoboUtils.SaveCardsToFile(finalPath + "/cards" + (i + 1 + "").PadLeft((amount + "").Length, '0') + ".deck", cards);
            }
        }

        private static String GetFormattedStatistics(Dictionary<String, List<RoboPlayerResult>> results)
        {
            StringBuilder builder = new StringBuilder("board; #win; #rounds; #cards; #time; øwin; ørounds; øcards; øtime");
            builder.AppendLine();
            Dictionary<String, RoboPlayerStatistic> stats = new Dictionary<String, RoboPlayerStatistic>();
            foreach (KeyValuePair<String, List<RoboPlayerResult>> entry in results)
            {
                stats.Add(entry.Key, RoboPlayerStatistic.CalcStatistics(entry.Value));
            }
            stats.Add("Total", RoboPlayerStatistic.CalcStatistics(stats.Values));

            foreach (KeyValuePair<String, RoboPlayerStatistic> entry in stats)
            {
                RoboPlayerStatistic stat = entry.Value;
                builder.Append(entry.Key);
                builder.Append("; ");
                builder.Append(stat.Finished);
                builder.Append("; ");
                builder.Append(stat.RoundsPlayed);
                builder.Append("; ");
                builder.Append(stat.CardsPlayed);
                builder.Append("; ");
                builder.Append(stat.Time);
                builder.Append("; ");
                builder.Append(stat.Finished + stat.Died + stat.Error > 0 ? (double)stat.Finished / (stat.Finished + stat.Died + stat.Error) : 0.0);
                builder.Append("; ");
                builder.Append(stat.Finished > 0 ? stat.RoundsPlayed / stat.Finished : 0.0);
                builder.Append("; ");
                builder.Append(stat.Finished > 0 ? stat.CardsPlayed / stat.Finished : 0.0);
                builder.Append("; ");
                builder.Append(stat.Finished > 0 ? stat.Time / stat.Finished : 0.0);
                builder.AppendLine();
            }

            return builder.ToString();
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

    public class RoboPlayerResult
    {
        public RoboPlayerResult() { }

        public RoboPlayerResult(RoboPlayer player)
        {
            State = player.PlayerState;
            RoundsPlayed = player.Round;
            CardsPlayed = player.TotalPlayedCards;
            Time = player.TotalTimeElapsed;
        }

        public RoboPlayerState State { set; get; }
        public int RoundsPlayed { set; get; }
        public int CardsPlayed { set; get; }
        public TimeSpan Time { set; get; }
    }

    public class RoboPlayerStatistic
    {
        public static RoboPlayerStatistic CalcStatistics(IEnumerable<RoboPlayerStatistic> singleStats)
        {
            RoboPlayerStatistic stats = new RoboPlayerStatistic();
            foreach (RoboPlayerStatistic singleStat in singleStats)
            {
                stats.Finished += singleStat.Finished;
                stats.Died += singleStat.Died;
                stats.Finished += singleStat.Error;
                stats.RoundsPlayed += singleStat.RoundsPlayed;
                stats.CardsPlayed += singleStat.CardsPlayed;
                stats.Time += singleStat.Time;
            }
            return stats;
        }

        public static RoboPlayerStatistic CalcStatistics(IEnumerable<RoboPlayerResult> results)
        {
            RoboPlayerStatistic stats = new RoboPlayerStatistic();
            foreach(RoboPlayerResult result in results)
            {
                switch(result.State)
                {
                    case RoboPlayerState.Dead:
                        stats.Died++;
                        break;
                    case RoboPlayerState.Finished:
                        stats.RoundsPlayed += result.RoundsPlayed;
                        stats.CardsPlayed += result.CardsPlayed;
                        stats.Time += result.Time.Milliseconds;
                        stats.Finished++;
                        break;
                    default:
                        stats.Error++;
                        break;
                }    
            }
            return stats;
        }

        public int Finished { set; get; }
        public int Died { set; get; }
        public int Error { set; get; }
        public double RoundsPlayed { set; get; }
        public double CardsPlayed { set; get; }
        public int Time { set; get; }
    }
}