using System;
using System.Windows.Forms;

namespace Handicapper
{
    public partial class frmAddNew : Form
    {

        DialogResult result = DialogResult.Ignore;
        Helpers.NewType _typ;

        public frmAddNew(Helpers.NewType typ)
        {
            try {
                _typ = typ;
                InitializeComponent();

                if (_typ == Helpers.NewType.League)
                {
                    this.Text = "Add New League";
                    this.lblAddNew.Text = "Add New League";
                }
                else
                {
                    this.Text = "Add New Player";
                    this.lblAddNew.Text = "Add New Player";
                }

                this.txtName.Focus();

            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }
        
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try {
                if (ValidateItem())
                {
                    if (_typ == Helpers.NewType.Player)
                    {
                        Player plr = new Player(Helpers.s_Players.NewPlayerID())
                        {
                            Name = this.txtName.Text,
                            Category = Helpers.s_Buffers.MaximumCategoryID(),
                            Actual = 0.0,
                            Notes = "New Player (PFH)",
                            Playing = 0,
                            DataIsDirty = true
                        };
                        Helpers.s_Players.AddPlayer(plr);
                    }
                    else
                        Helpers.s_Leagues.CreateLeagues(this.txtName.Text);

                    result = DialogResult.Yes;
                    this.Close();
                }
                else
                    MessageBox.Show(this.txtName.Text + " already exists in the collection");

            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateItem()
        {
            try {
                if (this.txtName.Text.Length < 1)
                    return false;
                else
                    if (_typ == Helpers.NewType.Player)
                        return !Helpers.s_Players.DoesPlayerExist(this.txtName.Text);
                    else
                        return !Helpers.s_Leagues.doesLeagueExist(this.txtName.Text);

            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }

        private void frmAddNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = result;
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdSave_Click(sender, e);
        }


    }
}
