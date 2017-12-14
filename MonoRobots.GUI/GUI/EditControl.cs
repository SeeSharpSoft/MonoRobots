using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SeeSharpSoft.MonoRobots.GUI
{
    public partial class EditControl : UserControl
    {
        public EditControl()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RoboBoard Board { set; get; }

        private void btnSizeOK_Click(object sender, EventArgs e)
        {
            if (Board == null) return;

            int x, y;

            if (!Int32.TryParse(tbSizeX.Text, out x) || !Int32.TryParse(tbSizeY.Text, out y))
            {
                MessageBox.Show(this, "Error on parsing size dimensions - only integers are allowed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Board.SetSize(x, y);

            this.ParentForm.Refresh();
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            if (Board == null) return;

            Board.RotateFields();

            this.ParentForm.Refresh();
        }

        private void btnMirrorVert_Click(object sender, EventArgs e)
        {
            if (Board == null) return;

            Board.MirrorFieldsVertical();

            this.ParentForm.Refresh();
        }

        private void btnMirrorHoriz_Click(object sender, EventArgs e)
        {
            if (Board == null) return;

            Board.MirrorFieldsHorizontal();

            this.ParentForm.Refresh();
        }

        private void btnFlip1_Click(object sender, EventArgs e)
        {
            if (Board == null) return;

            Board.MirrorFieldsVertical();
            Board.RotateFields();

            this.ParentForm.Refresh();
        }

        private void btnFlip2_Click(object sender, EventArgs e)
        {
            if (Board == null) return;

            Board.MirrorFieldsHorizontal();
            Board.RotateFields();

            this.ParentForm.Refresh();
        }

        private void btnRotateCCW_Click(object sender, EventArgs e)
        {
            if (Board == null) return;

            Board.RotateFields();
            Board.RotateFields();
            Board.RotateFields();

            this.ParentForm.Refresh();
        }        
    }
}