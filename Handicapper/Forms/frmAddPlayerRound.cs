using System;
using System.Windows.Forms;

namespace Handicapper
{
    public partial class frmAddPlayerRound : Form
    {

        Player _CurrentPlayer;
        Round _CurrentRound = new Round();
        DialogResult result = DialogResult.Ignore;

        public frmAddPlayerRound(Player plr)
        {
            try { 
                _CurrentPlayer = plr;
                this.Text = string.Concat(this.Text, " (", _CurrentPlayer.Name, ")");                
                InitializeComponent();

            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }

        private void frmAddPlayerRound_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = result;           
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try {
                if (ValidateData())
                {
                    double newActualHandicap = 0.0;
                    BuildCurrentRound();

                    // If >= 3 rounds played then calculate the Handicap.
                    if (_CurrentRound.Sequence < 3)
                        // do nothing
                        _CurrentPlayer.Notes = "Not yet played 3 Rounds so Handicap not Calculated.";
                    else
                        // Recalcualate.
                        newActualHandicap = _CurrentPlayer.ReCalcHandicap(_CurrentRound.Score_Net, _CurrentRound.Sequence);

                    this.txtNotes.Text = _CurrentPlayer.Notes;
                    SaveRound();                   
                    result = DialogResult.Yes;
                    this.Close();
                }
            else
                MessageBox.Show("Cannot add this Round as the data is incomplete");

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

        private bool ValidateData()
        {
            try {
                if (this.txtActual.Text.Length == 0 || !Helpers.IsNumeric(this.txtActual.Text))
                    return false;
                if (this.txtAdjusted.Text.Length == 0 || !Helpers.IsNumeric(this.txtAdjusted.Text))
                    return false;
                if (this.txtCourse.Text.Length == 0)
                    return false;
                if (this.txtSSI.Text.Length == 0 || !Helpers.IsNumeric(this.txtSSI.Text))
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }
        private void SaveRound()
        {
            try {
                _CurrentRound.Notes = this.txtNotes.Text;
                Helpers.s_Rounds.AddRound(_CurrentRound);
                _CurrentPlayer.UpdateRoundsPlayed(_CurrentRound.Sequence);
            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }
        private void BuildCurrentRound()
        {
            try {
                _CurrentRound.ActualStrokes = Helpers.NullToInt(this.txtActual.Text);
                _CurrentRound.AdjustedStrokes = Helpers.NullToInt(this.txtAdjusted.Text);
                _CurrentRound.Course = this.txtCourse.Text;
                _CurrentRound.Date = this.dtDatePlayed.Text;
                _CurrentRound.HandicapUsed = Helpers.NullToInt(_CurrentPlayer.Playing);
                _CurrentRound.PlayerID = _CurrentPlayer.PlayerID;
                _CurrentRound.Sequence = Helpers.NullToInt(_CurrentPlayer.RoundsPlayed) + 1;
                _CurrentRound.SSI = Helpers.NullToInt(this.txtSSI.Text);
                _CurrentRound.Score_Gross = _CurrentRound.AdjustedStrokes - _CurrentRound.SSI;
                _CurrentRound.Score_Net = _CurrentRound.Score_Gross - _CurrentRound.HandicapUsed;
            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }
      

    }
}
