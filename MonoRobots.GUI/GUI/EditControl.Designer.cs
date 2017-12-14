namespace SeeSharpSoft.MonoRobots.GUI
{
    partial class EditControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSizeOK = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.btnFlip2 = new System.Windows.Forms.Button();
            this.btnFlip1 = new System.Windows.Forms.Button();
            this.btnRotateCCW = new System.Windows.Forms.Button();
            this.btnMirrorHoriz = new System.Windows.Forms.Button();
            this.btnMirrorVert = new System.Windows.Forms.Button();
            this.btnRotate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSizeY = new System.Windows.Forms.TextBox();
            this.tbSizeX = new System.Windows.Forms.TextBox();
            this.fieldToolbox = new SeeSharpSoft.MonoRobots.GUI.FieldToolbox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSizeOK
            // 
            this.btnSizeOK.Location = new System.Drawing.Point(228, 21);
            this.btnSizeOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSizeOK.Name = "btnSizeOK";
            this.btnSizeOK.Size = new System.Drawing.Size(100, 28);
            this.btnSizeOK.TabIndex = 1;
            this.btnSizeOK.Text = "Apply";
            this.btnSizeOK.UseVisualStyleBackColor = true;
            this.btnSizeOK.Click += new System.EventHandler(this.btnSizeOK_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.btnFlip2);
            this.groupBox.Controls.Add(this.btnFlip1);
            this.groupBox.Controls.Add(this.btnRotateCCW);
            this.groupBox.Controls.Add(this.btnMirrorHoriz);
            this.groupBox.Controls.Add(this.btnMirrorVert);
            this.groupBox.Controls.Add(this.btnRotate);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Controls.Add(this.tbSizeY);
            this.groupBox.Controls.Add(this.tbSizeX);
            this.groupBox.Controls.Add(this.btnSizeOK);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox.Location = new System.Drawing.Point(0, 322);
            this.groupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox.Name = "groupBox";
            this.groupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox.Size = new System.Drawing.Size(340, 129);
            this.groupBox.TabIndex = 2;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Modify board";
            // 
            // btnFlip2
            // 
            this.btnFlip2.Location = new System.Drawing.Point(228, 91);
            this.btnFlip2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFlip2.Name = "btnFlip2";
            this.btnFlip2.Size = new System.Drawing.Size(100, 28);
            this.btnFlip2.TabIndex = 11;
            this.btnFlip2.Text = "Flip 2";
            this.btnFlip2.UseVisualStyleBackColor = true;
            this.btnFlip2.Click += new System.EventHandler(this.btnFlip2_Click);
            // 
            // btnFlip1
            // 
            this.btnFlip1.Location = new System.Drawing.Point(228, 55);
            this.btnFlip1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFlip1.Name = "btnFlip1";
            this.btnFlip1.Size = new System.Drawing.Size(100, 28);
            this.btnFlip1.TabIndex = 10;
            this.btnFlip1.Text = "Flip 1";
            this.btnFlip1.UseVisualStyleBackColor = true;
            this.btnFlip1.Click += new System.EventHandler(this.btnFlip1_Click);
            // 
            // btnRotateCCW
            // 
            this.btnRotateCCW.Location = new System.Drawing.Point(12, 91);
            this.btnRotateCCW.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRotateCCW.Name = "btnRotateCCW";
            this.btnRotateCCW.Size = new System.Drawing.Size(100, 28);
            this.btnRotateCCW.TabIndex = 9;
            this.btnRotateCCW.Text = "Rotate -90°";
            this.btnRotateCCW.UseVisualStyleBackColor = true;
            this.btnRotateCCW.Click += new System.EventHandler(this.btnRotateCCW_Click);
            // 
            // btnMirrorHoriz
            // 
            this.btnMirrorHoriz.Location = new System.Drawing.Point(120, 91);
            this.btnMirrorHoriz.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMirrorHoriz.Name = "btnMirrorHoriz";
            this.btnMirrorHoriz.Size = new System.Drawing.Size(100, 28);
            this.btnMirrorHoriz.TabIndex = 8;
            this.btnMirrorHoriz.Text = "Mirror horiz.";
            this.btnMirrorHoriz.UseVisualStyleBackColor = true;
            this.btnMirrorHoriz.Click += new System.EventHandler(this.btnMirrorHoriz_Click);
            // 
            // btnMirrorVert
            // 
            this.btnMirrorVert.Location = new System.Drawing.Point(120, 55);
            this.btnMirrorVert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMirrorVert.Name = "btnMirrorVert";
            this.btnMirrorVert.Size = new System.Drawing.Size(100, 28);
            this.btnMirrorVert.TabIndex = 7;
            this.btnMirrorVert.Text = "Mirror vert.";
            this.btnMirrorVert.UseVisualStyleBackColor = true;
            this.btnMirrorVert.Click += new System.EventHandler(this.btnMirrorVert_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.Location = new System.Drawing.Point(12, 55);
            this.btnRotate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(100, 28);
            this.btnRotate.TabIndex = 6;
            this.btnRotate.Text = "Rotate 90°";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "x";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Size:";
            // 
            // tbSizeY
            // 
            this.tbSizeY.Location = new System.Drawing.Point(144, 23);
            this.tbSizeY.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbSizeY.Name = "tbSizeY";
            this.tbSizeY.Size = new System.Drawing.Size(53, 22);
            this.tbSizeY.TabIndex = 3;
            // 
            // tbSizeX
            // 
            this.tbSizeX.Location = new System.Drawing.Point(57, 23);
            this.tbSizeX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbSizeX.Name = "tbSizeX";
            this.tbSizeX.Size = new System.Drawing.Size(53, 22);
            this.tbSizeX.TabIndex = 2;
            // 
            // fieldToolbox
            // 
            this.fieldToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldToolbox.Location = new System.Drawing.Point(0, 0);
            this.fieldToolbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fieldToolbox.Name = "fieldToolbox";
            this.fieldToolbox.Size = new System.Drawing.Size(340, 451);
            this.fieldToolbox.TabIndex = 0;
            this.fieldToolbox.Text = "fieldToolbox1";
            // 
            // EditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.fieldToolbox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "EditControl";
            this.Size = new System.Drawing.Size(340, 451);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SeeSharpSoft.MonoRobots.GUI.FieldToolbox fieldToolbox;
        private System.Windows.Forms.Button btnSizeOK;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSizeY;
        private System.Windows.Forms.TextBox tbSizeX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMirrorHoriz;
        private System.Windows.Forms.Button btnMirrorVert;
        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.Button btnRotateCCW;
        private System.Windows.Forms.Button btnFlip2;
        private System.Windows.Forms.Button btnFlip1;
    }
}