namespace SeeSharpSoft.MonoRobots.GUI
{
    partial class RoboPlayerControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cardBox1 = new System.Windows.Forms.PictureBox();
            this.cardBox2 = new System.Windows.Forms.PictureBox();
            this.cardBox3 = new System.Windows.Forms.PictureBox();
            this.cardBox4 = new System.Windows.Forms.PictureBox();
            this.cardBox5 = new System.Windows.Forms.PictureBox();
            this.dataLabelName = new System.Windows.Forms.Label();
            this.roboPlayerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataLabelState = new System.Windows.Forms.Label();
            this.dataLabelTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cardBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roboPlayerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cardBox1
            // 
            this.cardBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cardBox1.Location = new System.Drawing.Point(3, 41);
            this.cardBox1.Name = "cardBox1";
            this.cardBox1.Size = new System.Drawing.Size(54, 64);
            this.cardBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cardBox1.TabIndex = 0;
            this.cardBox1.TabStop = false;
            // 
            // cardBox2
            // 
            this.cardBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cardBox2.Location = new System.Drawing.Point(63, 41);
            this.cardBox2.Name = "cardBox2";
            this.cardBox2.Size = new System.Drawing.Size(54, 64);
            this.cardBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cardBox2.TabIndex = 1;
            this.cardBox2.TabStop = false;
            // 
            // cardBox3
            // 
            this.cardBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cardBox3.Location = new System.Drawing.Point(123, 41);
            this.cardBox3.Name = "cardBox3";
            this.cardBox3.Size = new System.Drawing.Size(54, 64);
            this.cardBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cardBox3.TabIndex = 2;
            this.cardBox3.TabStop = false;
            // 
            // cardBox4
            // 
            this.cardBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cardBox4.Location = new System.Drawing.Point(183, 41);
            this.cardBox4.Name = "cardBox4";
            this.cardBox4.Size = new System.Drawing.Size(54, 64);
            this.cardBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cardBox4.TabIndex = 3;
            this.cardBox4.TabStop = false;
            // 
            // cardBox5
            // 
            this.cardBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cardBox5.Location = new System.Drawing.Point(243, 41);
            this.cardBox5.Name = "cardBox5";
            this.cardBox5.Size = new System.Drawing.Size(54, 64);
            this.cardBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cardBox5.TabIndex = 4;
            this.cardBox5.TabStop = false;
            // 
            // dataLabelName
            // 
            this.dataLabelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataLabelName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.roboPlayerBindingSource, "Name", true));
            this.dataLabelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataLabelName.Location = new System.Drawing.Point(6, 2);
            this.dataLabelName.Name = "dataLabelName";
            this.dataLabelName.Size = new System.Drawing.Size(291, 20);
            this.dataLabelName.TabIndex = 5;
            this.dataLabelName.Text = "<unknown>";
            this.dataLabelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // roboPlayerBindingSource
            // 
            this.roboPlayerBindingSource.AllowNew = false;
            this.roboPlayerBindingSource.DataSource = typeof(SeeSharpSoft.MonoRobots.RoboPlayer);
            // 
            // dataLabelState
            // 
            this.dataLabelState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataLabelState.Location = new System.Drawing.Point(183, 20);
            this.dataLabelState.Name = "dataLabelState";
            this.dataLabelState.Size = new System.Drawing.Size(114, 17);
            this.dataLabelState.TabIndex = 9;
            this.dataLabelState.Text = "Thinking";
            this.dataLabelState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataLabelTime
            // 
            this.dataLabelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataLabelTime.Location = new System.Drawing.Point(84, 20);
            this.dataLabelTime.Name = "dataLabelTime";
            this.dataLabelTime.Size = new System.Drawing.Size(93, 17);
            this.dataLabelTime.TabIndex = 10;
            this.dataLabelTime.Text = "5954 ms";
            this.dataLabelTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Playtime (total):";
            // 
            // RoboPlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataLabelTime);
            this.Controls.Add(this.dataLabelState);
            this.Controls.Add(this.dataLabelName);
            this.Controls.Add(this.cardBox5);
            this.Controls.Add(this.cardBox4);
            this.Controls.Add(this.cardBox3);
            this.Controls.Add(this.cardBox2);
            this.Controls.Add(this.cardBox1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "RoboPlayerControl";
            this.Size = new System.Drawing.Size(300, 110);
            ((System.ComponentModel.ISupportInitialize)(this.cardBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roboPlayerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox cardBox1;
        private System.Windows.Forms.PictureBox cardBox2;
        private System.Windows.Forms.PictureBox cardBox3;
        private System.Windows.Forms.PictureBox cardBox4;
        private System.Windows.Forms.PictureBox cardBox5;
        private System.Windows.Forms.Label dataLabelName;
        private System.Windows.Forms.Label dataLabelState;
        private System.Windows.Forms.Label dataLabelTime;
        private System.Windows.Forms.BindingSource roboPlayerBindingSource;
        private System.Windows.Forms.Label label1;
    }
}
