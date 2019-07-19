using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Handicapper
{
    public partial class frmPlayerHistory : Form
    {
        Player _CurrentPlayer;
        bool _DataChanged = false;

        public frmPlayerHistory(int PlayerID)
        {
            try {
                InitializeComponent();
                this.cmdExtract.Enabled = false;
                Prepare(PlayerID);
            }

            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }
               
        private void frmPlayerHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_DataChanged)
                this.DialogResult = DialogResult.Yes;
            else
                this.DialogResult = DialogResult.Ignore;
        }
        private void cmdAddRound_Click(object sender, EventArgs e)
        {
            try {
                using (frmAddPlayerRound ln = new frmAddPlayerRound(_CurrentPlayer))
                {
                    ln.ShowDialog();
                    if (ln.DialogResult == DialogResult.Yes)
                    {
                        _DataChanged = true;
                        Prepare(_CurrentPlayer.PlayerID);
                    }
                }
            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmdExtract_Click(object sender, EventArgs e)
        {
            string result = ExtractPlayerDetails(false);
            if (result != string.Empty)
                MessageBox.Show("File saved as " + result);
        }
        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (_CurrentPlayer.RoundsPlayed < 3)
                MessageBox.Show("Cannot apply an adjustment as still PFH", Helpers.Title);
            else
            {
                int adjustment = ValidAdjustment();
                if (adjustment > 0 && adjustment < 10)
                {
                    _CurrentPlayer.CreateAdjustment(adjustment);
                    _DataChanged = true;
                    // add this to rounds and refresh where required.                    
                    Prepare(_CurrentPlayer.PlayerID);

                    if (MessageBox.Show("Do you want to create a letter? ", Helpers.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        ExtractPlayerDetails(true);
                }
                else
                    MessageBox.Show("This is not a valid adjustment value", Helpers.Title);
            }

            this.chkAdjustment.Checked = false;

        }
        private void lnkInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInfo ln = new frmInfo();
            ln.ShowDialog();
            ln.Dispose();
        }
        private void chkAdjustment_CheckedChanged(object sender, EventArgs e)
        {
            this.txtAdjustment.Text = string.Empty;
            this.txtAdjustment.Enabled = this.chkAdjustment.Checked;
            this.cmdApply.Enabled = this.chkAdjustment.Checked;
        }


        private void Prepare(int PlayerID)
        {
            _CurrentPlayer = new Player();
            _CurrentPlayer = Helpers.s_Players.GetPlayer(PlayerID);
            FillDetail();
            FillHistory();
            FormatGrid();
        }
        private void FillDetail()
        {
            try {
                this.lblName.Text = _CurrentPlayer.Name;                
                this.lblClass.Text = _CurrentPlayer.Category.ToString();
                this.lblRounds.Text = _CurrentPlayer.RoundsPlayed.ToString();
                this.lblNotes.Text = _CurrentPlayer.Notes.ToString();

                if (_CurrentPlayer.RoundsPlayed > 2)
                {
                    this.chkAdjustment.Enabled = true;
                    this.lblPlaying.Text = _CurrentPlayer.Playing.ToString();
                    this.lblActual.Text = _CurrentPlayer.Actual.ToString("#0.0");
                }
                else
                {
                    this.chkAdjustment.Enabled = false;
                    this.lblActual.Text = "PFH";
                    this.lblPlaying.Text = "PFH";                    
                }

             }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }
        private void FillHistory()
        {
            try {
                this.dgvHistory.DataSource = null;
                if (this.dgvHistory.Rows.Count > 0)
                {
                    this.dgvHistory.Rows.Clear();
                    this.dgvHistory.Refresh();
                }

                List<Round> history = new List<Round>();
                history = Helpers.s_Rounds.RoundsCompleted(_CurrentPlayer.PlayerID);                
                this.dgvHistory.DataSource = history;
            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }
        private void FormatGrid()
        {
            try {
                if (this.dgvHistory.Rows.Count > 0)
                {
                    this.cmdExtract.Enabled = true;
                    this.dgvHistory.Columns[0].Visible = false;
                    this.dgvHistory.Columns[1].Visible = false;
                    this.dgvHistory.Columns[4].Width = 50;
                    this.dgvHistory.Columns[7].Width = 80;
                    this.dgvHistory.Columns[8].Width = 80;
                    Helpers.StretchLastColumn(this.dgvHistory);
                }
            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }        
        private int ValidAdjustment()
        {
            if (Helpers.IsNumeric(this.txtAdjustment.Text))
                return Helpers.NullToInt(this.txtAdjustment.Text);
            else
                return 0;
            }

        private string ExtractPlayerDetails(bool adjustmentLetter)
        {
            try {
                string path = Helpers.OpenFile(Helpers.FileType.ForExtracting);
                if (path != string.Empty)
                {
                    using (FileAccessor fl = new FileAccessor(path, Helpers.FileType.ForExtracting))
                    {
                        StringBuilder str = new StringBuilder();

                        str.Append("Player Name:".PadRight(20)).Append(_CurrentPlayer.Name).AppendLine();
                        str.Append("Extract Date:".PadRight(20)).Append(DateTime.Now.ToString("dd MMM yyyy")).AppendLine();
                        str.Append("Rounds Played:".PadRight(20)).Append(_CurrentPlayer.RoundsPlayed.ToString()).AppendLine();
                        str.Append("Playing Handicap:".PadRight(20)).Append(_CurrentPlayer.Playing.ToString("#0")).AppendLine();
                        str.Append("Actual Handicap:".PadRight(20)).Append(_CurrentPlayer.Actual.ToString("#0.0")).AppendLine();
                        str.Append("Category:".PadRight(20)).Append(_CurrentPlayer.Category.ToString()).AppendLine();
                        fl.WriteEntry(str.ToString());
                        str.Clear();

                        if (adjustmentLetter)
                        {
                            fl.WriteEntry("");
                            fl.WriteEntry("*".PadLeft(50, '*'));
                            fl.WriteEntry("ADJUSTMENT");
                            fl.WriteEntry("*".PadLeft(50, '*'));

                            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath + "Info\\"))
                            {
                                string info = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath + "Info\\Clause23.txt";
                                if (File.Exists(info))
                                {
                                    using (FileAccessor file = new FileAccessor(info, Helpers.FileType.ForReading))
                                    {
                                       str.Append( file.DataFile.ReadToEnd());
                                    }
                                }   
                                else
                                    str.Append("Cannot find information File");
                            }
                            else
                                str.Append("Cannot find information Folder");
                            
                            fl.WriteEntry(str.ToString());
                            str.Clear();
                            fl.WriteEntry("");
                        }
                        
                        str.Append("Date".PadRight(20));
                        str.Append("Course".PadRight(12));
                        str.Append("SSI".PadRight(5));
                        str.Append("Score".PadRight(7));
                        str.Append("Adjusted".PadRight(10));
                        str.Append("Previous".PadRight(10));
                        str.Append("Net".PadRight(12));
                        str.Append("Notes");
                        fl.WriteEntry(str.AppendLine().ToString());
                        
                        foreach (DataGridViewRow rw in this.dgvHistory.Rows)
                        {                            
                            str.Clear();
                            str.Append(rw.Cells[2].Value.ToString().PadRight(20));

                            // Chop Course to 12 characters.
                            if(rw.Cells[3].Value.ToString().Length > 12)
                                str.Append(rw.Cells[3].Value.ToString().Substring(0, 12));
                            else
                                str.Append(rw.Cells[3].Value.ToString().PadRight(12));

                            str.Append(rw.Cells[4].Value.ToString().PadRight(5));
                            str.Append(rw.Cells[5].Value.ToString().PadRight(7));
                            str.Append(rw.Cells[6].Value.ToString().PadRight(10));
                            str.Append(rw.Cells[9].Value.ToString().PadRight(10));

                            str.Append(((Convert.ToInt32(rw.Cells[6].Value) - Convert.ToInt32(rw.Cells[4].Value)).ToString()));
                            str.Append(" (").Append(rw.Cells[6].Value.ToString()).Append("-").Append(rw.Cells[4].Value.ToString()).Append(")  ");
                            
                            str.Append(rw.Cells[10].Value.ToString());

                            fl.WriteEntry(str.ToString());
                        }

                        // Extract current Players Category details.
                        BufferCategory CurrentCategory = new BufferCategory();
                        CurrentCategory = Helpers.s_Buffers.GetCategory(_CurrentPlayer.Category);

                        str.Clear();
                        fl.WriteEntry(str.AppendLine().ToString());
                        str.Append(string.Concat(Enumerable.Repeat("*", 50)));
                        fl.WriteEntry(str.ToString());

                        str.Clear();
                        str.Append("Current Category is ").Append(CurrentCategory.Category.ToString());
                        fl.WriteEntry(str.ToString());

                        str.Clear();
                        str.Append("Minimum Handicap in this Category: ").Append(CurrentCategory.Minimum.ToString());
                        fl.WriteEntry(str.ToString());

                        str.Clear();
                        str.Append("Maximum Handicap in this Category: ").Append(CurrentCategory.Maximum.ToString());
                        fl.WriteEntry(str.ToString());

                        str.Clear();
                        str.Append("Adjustment Range is ").Append(CurrentCategory.Reduction.ToString()).Append(" Reduction and ").Append(CurrentCategory.Increase.ToString()).Append(" Increase");
                        fl.WriteEntry(str.ToString());

                        str.Clear();
                        str.Append("Buffer is ").Append(CurrentCategory.BufferValue.ToString());
                        fl.WriteEntry(str.ToString());

                        str.Clear();
                        str.Append("The Buffer is used where the Net Score is below or above Handicap to decide if an Adjustment is to be made. ").AppendLine();
                        str.Append("    If Net Score is greater than buffer then Adjustment is an Increase. ").AppendLine();
                        str.Append("    If Net Score is within buffer then no Adjustment. ").AppendLine();
                        str.Append("    If Net Score is less than Handicap then Adjustment is a Reduction. ");
                        fl.WriteEntry(str.ToString());

                        str.Clear();                        
                        str.Append(string.Concat(Enumerable.Repeat("*", 50)));
                        fl.WriteEntry(str.ToString());

                        str = null;
                    }
                }

                return path;

            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }

             
            
        }

    }

}
