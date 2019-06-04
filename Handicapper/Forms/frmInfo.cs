using System;
using System.IO;
using System.Windows.Forms;

namespace Handicapper
{
    public partial class frmInfo : Form
    {
        public frmInfo()
        {
            InitializeComponent();
            FillText();
        }

        private void FillText()
        {
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath + "Info\\"))
            {
                string fl = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath + "Info\\Clause23.txt";
                if (File.Exists(fl))
                    this.txtInfo.LoadFile(fl, RichTextBoxStreamType.PlainText);
                else
                    this.txtInfo.Text = "Cannot find information File";
            }
            else
                this.txtInfo.Text = "Cannot find information Folder";            
        }


        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
