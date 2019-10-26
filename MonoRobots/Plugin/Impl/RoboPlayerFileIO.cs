using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace SeeSharpSoft.MonoRobots.Plugin.Impl
{
    public abstract class RoboPlayerFileIO : RoboPlayerPlugin
    {
        protected virtual String WorkingDirectory
        {
            get
            {
                String location = this.GetType().Assembly.Location.Replace("\\\\", "/").Replace("\\", "/").Replace("//", "/");
                location = location.Remove(location.LastIndexOf('/'));
				return location;
            }
        }
        protected abstract String LaunchFile { get; }
        protected virtual String LaunchFileArguments { get { return ""; } }
		public virtual bool UseFullPath { get { return true; } }
        public virtual bool DifficultyAsParameter { get { return true; } }

        public override void StartGame(RoboBoard board)
        {
            base.StartGame(board);
        }

        public override void StartRound(RoboPosition position, ICollection<RoboCard> cards, IEnumerable<RoboPosition> allPlayers)
        {
            SavePlayFiles(position, cards, allPlayers);

            Exception ex = LaunchProgramm();

            if (ex != null)
            {
                PlayCards(null, ex);
                return;
            }

            RoboCard[] result = RoboUtils.LoadCards(WorkingDirectory + "/ccards.txt", 5);

            PlayCards(result);
        }

        private void SavePlayFiles(RoboPosition position, IEnumerable<RoboCard> cards, IEnumerable<RoboPosition> allPlayers)
        {
            RoboBoard board = GetBlockingBoard(position, allPlayers);

            board.Save(WorkingDirectory + "/board.txt");

            RoboPosition.SavePosition(WorkingDirectory + "/bot.txt", position);
            RoboUtils.SaveCardsToFile(WorkingDirectory + "/cards.txt", cards);
        }

        protected virtual Exception LaunchProgramm()
        {
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = WorkingDirectory;
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            startInfo.FileName = (UseFullPath ? WorkingDirectory + "/" : "") + LaunchFile;
            startInfo.Arguments = LaunchFileArguments + " " + Board.Difficulty.ToString().ToLower();

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
                return ex;
            }

            return null;
        }
    }
}