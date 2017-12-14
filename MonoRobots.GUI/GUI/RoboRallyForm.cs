using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SeeSharpSoft.Plugin;
using System.Threading;
using SeeSharpSoft.MonoRobots.Plugin.Impl;
using SeeSharpSoft.MonoRobots.Plugin;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SeeSharpSoft.MonoRobots.GUI
{
    public partial class RoboRallyForm : Form
    {
        public RoboRallyForm()
        {
            InitializeComponent();

            Throttling = 50;
            RoboManager = new RoboManager();
            RoboManager.GameStateChange += RoboManager_GameStateChange;
            CreateNewBoard();
            SetBoardEditorVisibility(true);
            folderBrowserDialog.SelectedPath = Directory.GetCurrentDirectory();
        }

        private void RoboManagerControlBinding_Format(object sender, ConvertEventArgs e)
        {
            RoboPlayerControl control = new RoboPlayerControl();
            control.RoboPlayer = e.Value as RoboPlayer;
            e.Value = control;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode) return;

            RoboManager.LoadPlugins();

            foreach (RoboPlayerPlugin plugin in RoboManager.AvailablePlugins)
            {
                AddPluginToPlayerDropDown(plugin);
            }
        }

        ToolStripMenuItem AddPluginToPlayerDropDown(RoboPlayerPlugin plugin)
        {
            ToolStripMenuItem item = playerToolStripMenuItem.DropDownItems.Add(plugin.Name) as ToolStripMenuItem;
            item.CheckOnClick = true;
            item.CheckedChanged += new EventHandler(item_CheckedChanged);
            return item;
        }

        RoboPlayerPlugin playerActivationChanged(String playerName, Boolean activated)
        {
            RoboPlayerPlugin plugin = null;
            if (activated)
            {
                plugin = RoboManager.ActivatePlugin(playerName);
                plugin.Player.Position.PropertyChanged += Position_PropertyChanged;

                RoboPlayerControl control = new RoboPlayerControl();
                control.RoboPlayer = plugin.Player;
                playerPanel.Controls.Add(control);
            }
            else
            {
                plugin = RoboManager.ActivePlayers.FirstOrDefault(elem => elem.Name == playerName);
                if (plugin != null)
                {
                    plugin.Player.Position.PropertyChanged -= Position_PropertyChanged;
                    RoboPlayerControl playerControl = playerPanel.Controls
                        .Cast<RoboPlayerControl>()
                        .FirstOrDefault(control => control.RoboPlayer == plugin.Player);
                    if (playerControl != null)
                    {
                        playerPanel.Controls.Remove(playerControl);
                    }
                }
                RoboManager.DeactivatePlugin(playerName);
            }
            return plugin;
        }

        void item_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            playerActivationChanged(item.Text, item.Checked);
        }

        void Position_PropertyChanged(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(boardControl.Refresh));
            Thread.Sleep(Throttling);
            Application.DoEvents();
        }

        RoboManager RoboManager { set; get; }
        int Throttling { set; get; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RoboBoard Board { get { return boardControl.Board; } }

        private void loadBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog(this) != DialogResult.OK) return;

            RoboBoard board = new RoboBoard();
            board.Load(openFileDialog.FileName, Difficulty.Hard);

            SetBoard(board);

            SetBoardEditorVisibility(false);
        }

        private void startGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Board == null) return;

            RoboManager.StartGame();
            RoboManager.StartRound();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(boardControl, "Discard actual board?", "New", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) return;
            CreateNewBoard();
            SetBoardEditorVisibility(true);
        }

        private void CreateNewBoard()
        {
            SetBoard(new RoboBoard(12, 12));
        }

        private void saveBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (boardControl.Board.BoardFile == null) SaveBoardAs();
            else if (MessageBox.Show(boardControl, "Override previous board?", "Save", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) return;
            else SaveBoard(boardControl.Board.BoardFile);
        }

        private void SaveBoard(String filename)
        {
            boardControl.Board.Save(filename);
        }

        private void SaveBoardAs()
        {
            if (boardControl.Board.BoardFile != null) saveFileDialog.FileName = boardControl.Board.BoardFile;
            if (saveFileDialog.ShowDialog(this) == DialogResult.Cancel) return;

            SaveBoard(saveFileDialog.FileName);
        }

        private void saveBoardAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveBoardAs();
        }

        public void SetBoard(RoboBoard board)
        {
            RoboManager.Board = board;
            boardControl.Board = board;
            editControl.Board = board;
        }

        private void boardControl_Paint(object sender, PaintEventArgs e)
        {
            foreach(RoboPlayer player in RoboManager.ActivePlayers.Select(plugin => plugin.Player))
            {
                boardControl.DrawRobo(e, new RoboPosition(player.Position.X -1, player.Position.Y -1, player.Position.Direction, player.Position.IsDead), player.Name);
            }
        }

        private void showEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetBoardEditorVisibility(splitContainer.Panel1Collapsed);
        }

        private void SetBoardEditorVisibility(bool visible)
        {
            splitContainer.Panel1Collapsed = !visible;
            showEditorToolStripMenuItem.Checked = visible;
        }

        private String FindPluginName(String initialName)
        {
            IEnumerable<String> pluginNames = RoboManager.AvailablePlugins.Select(plugin => plugin.Name);
            String pluginName = initialName;
            int nameDifferentiater = 1;
            while (pluginNames.Contains(pluginName))
            {
                nameDifferentiater++;
                pluginName = initialName + nameDifferentiater;
            }
            return pluginName;
        }

        private void loadKIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openKIFileDialog.ShowDialog(this) != DialogResult.OK || openKIFileDialog.FileName.IsNullOrEmpty()) return;

            String fileNameOnly = openKIFileDialog.SafeFileName;
            RoboPlayerFileIOWrapper plugin = new RoboPlayerFileIOWrapper();
            plugin.Name = FindPluginName(fileNameOnly.Substring(0, fileNameOnly.IndexOf('.')));
            plugin.ExecutablePath = openKIFileDialog.FileName;
            RoboManager.AvailablePlugins.Add(plugin);
            ToolStripMenuItem item = AddPluginToPlayerDropDown(plugin);
            item.PerformClick();
        }

        private void RoboManager_GameStateChange(RoboManager sender, EventArgs<RoboGameState> eventArgs)
        {
            switch(eventArgs.Value)
            {
                case RoboGameState.Stopped:
                    GameEnded();
                    break;
            }
        }

        private void GameEnded()
        {
            StringBuilder builder = new StringBuilder("All players reached the destination or died:\r\n\r\n");
            foreach (RoboPlayer player in RoboManager.ActivePlayers.Select(plugin => plugin.Player).OrderBy(elem => elem.TotalPlayedCards))
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
            MessageBox.Show(builder.ToString());
        }

        private void gameSpeedPlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Throttling > 0)
            {
                Throttling-=10;
            }
        }

        private void gameSpeedMinusToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Throttling < 3000)
            {
                Throttling+=10;
            }
        }

        private void changeAssetDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) != DialogResult.OK || folderBrowserDialog.SelectedPath.IsNullOrEmpty()) return;

            boardControl.LoadImages(folderBrowserDialog.SelectedPath);
        }

        private void playerPanelToggleButton_Click(object sender, EventArgs e)
        {
            boardPlayerSplitContainer.Panel2Collapsed = !boardPlayerSplitContainer.Panel2Collapsed;
        }
    }
}