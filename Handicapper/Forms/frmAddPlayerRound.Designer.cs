namespace Handicapper
{
    partial class frmAddPlayerRound
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtCourse = new System.Windows.Forms.TextBox();
            this.txtSSI = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtDatePlayed = new System.Windows.Forms.DateTimePicker();
            this.txtActual = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAdjusted = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Course";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCourse
            // 
            this.txtCourse.Location = new System.Drawing.Point(156, 65);
            this.txtCourse.MaxLength = 50;
            this.txtCourse.Name = "txtCourse";
            this.txtCourse.Size = new System.Drawing.Size(200, 22);
            this.txtCourse.TabIndex = 2;
            // 
            // txtSSI
            // 
            this.txtSSI.Location = new System.Drawing.Point(156, 108);
            this.txtSSI.MaxLength = 3;
            this.txtSSI.Name = "txtSSI";
            this.txtSSI.Size = new System.Drawing.Size(75, 22);
            this.txtSSI.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "SSI";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Date";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dtDatePlayed
            // 
            this.dtDatePlayed.CustomFormat = "\"dd/MM/yyyy\"";
            this.dtDatePlayed.Location = new System.Drawing.Point(156, 22);
            this.dtDatePlayed.Name = "dtDatePlayed";
            this.dtDatePlayed.Size = new System.Drawing.Size(200, 22);
            this.dtDatePlayed.TabIndex = 1;
            // 
            // txtActual
            // 
            this.txtActual.Location = new System.Drawing.Point(156, 151);
            this.txtActual.MaxLength = 3;
            this.txtActual.Name = "txtActual";
            this.txtActual.Size = new System.Drawing.Size(75, 22);
            this.txtActual.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Actual Strokes";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAdjusted
            // 
            this.txtAdjusted.Location = new System.Drawing.Point(156, 194);
            this.txtAdjusted.MaxLength = 3;
            this.txtAdjusted.Name = "txtAdjusted";
            this.txtAdjusted.Size = new System.Drawing.Size(75, 22);
            this.txtAdjusted.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Adjusted Strokes";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtNotes
            // 
            this.txtNotes.Enabled = false;
            this.txtNotes.Location = new System.Drawing.Point(156, 237);
            this.txtNotes.MaxLength = 200;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(370, 51);
            this.txtNotes.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(98, 237);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Notes";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(156, 326);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 31);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(451, 326);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 31);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmAddPlayerRound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 429);
            this.ControlBox = false;
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAdjusted);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtActual);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtDatePlayed);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSSI);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCourse);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmAddPlayerRound";
            this.Text = "Add Player Round";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddPlayerRound_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCourse;
        private System.Windows.Forms.TextBox txtSSI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtDatePlayed;
        private System.Windows.Forms.TextBox txtActual;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAdjusted;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdClose;
    }
}