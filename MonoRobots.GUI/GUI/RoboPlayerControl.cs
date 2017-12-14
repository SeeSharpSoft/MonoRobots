using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeeSharpSoft.MonoRobots.GUI.Properties;

namespace SeeSharpSoft.MonoRobots.GUI
{
    public partial class RoboPlayerControl : UserControl
    {
        public RoboPlayerControl()
        {
            InitializeComponent();

            roboPlayerBindingSource.CurrentItemChanged += RoboPlayerBindingSource_CurrentItemChanged;
        }

        private void UpdateUI(RoboPlayer roboPlayer)
        {
            if (dataLabelState.InvokeRequired)
            {
                Action<RoboPlayer> action = UpdateUI;
                this.Invoke(action, roboPlayer);
            }
            else
            {
                dataLabelState.Text = roboPlayer.PlayerState.ToString();
                dataLabelTime.Text = roboPlayer.TotalTimeElapsed.TotalMilliseconds + " ms";
                if (roboPlayer.PlayerState == RoboPlayerState.Decided && roboPlayer.Cards != null && roboPlayer.Cards.Length == 5)
                {
                    cardBox1.Image = (Image)Resources.ResourceManager.GetObject(RoboCard.EncodeCard(roboPlayer.Cards[0]).Replace(" ", ""), Resources.Culture);
                    cardBox2.Image = (Image)Resources.ResourceManager.GetObject(RoboCard.EncodeCard(roboPlayer.Cards[1]).Replace(" ", ""), Resources.Culture);
                    cardBox3.Image = (Image)Resources.ResourceManager.GetObject(RoboCard.EncodeCard(roboPlayer.Cards[2]).Replace(" ", ""), Resources.Culture);
                    cardBox4.Image = (Image)Resources.ResourceManager.GetObject(RoboCard.EncodeCard(roboPlayer.Cards[3]).Replace(" ", ""), Resources.Culture);
                    cardBox5.Image = (Image)Resources.ResourceManager.GetObject(RoboCard.EncodeCard(roboPlayer.Cards[4]).Replace(" ", ""), Resources.Culture);
                }
            }
        }

        private void RoboPlayerBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            UpdateUI(RoboPlayer);
        }

        private RoboPlayer _roboPlayer;
        public RoboPlayer RoboPlayer
        {
            set
            {
                if (_roboPlayer == value)
                {
                    return;
                }
                _roboPlayer = value;
                roboPlayerBindingSource.DataSource = _roboPlayer;
            }
            get
            {
                return _roboPlayer;
            }
        }

        private void DataLabelTime_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value == null)
            {
                e.Value = "<unknown>";
            }
            else
            {
                e.Value = e.Value + " ms";
            }
        }

        private void DataLabelState_Format(object sender, ConvertEventArgs e)
        {
            if (!(e.Value is RoboPlayerState))
            {
                e.Value = "<unknown>";
            } else
            {
                e.Value = e.Value.ToString();
            }
        }
    }
}
