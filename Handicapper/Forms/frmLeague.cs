using System;
using System.Windows.Forms;

namespace Handicapper
{
    public partial class frmLeague : Form
    {
        DialogResult result = DialogResult.Ignore;

        public frmLeague()
        {
            try { 
                InitializeComponent();
                PopulateItems();

            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }

        private void cmdCreateNew_Click(object sender, EventArgs e)
        {
            try {
                frmAddNew ln = new frmAddNew(Helpers.NewType.League);
                ln.ShowDialog();
                if (ln.DialogResult == DialogResult.Yes)
                    PopulateItems();
                ln.Dispose();

            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }     
        private void cmdClose_Click(object sender, EventArgs e)
        {
            try {
                Helpers.s_Leagues = null;
                result = DialogResult.Ignore;
                this.Close();

            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }
        private void frmLeague_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = result;
        }
        private void lstItems_DoubleClick(object sender, EventArgs e)
        {
         
            try {
                if (this.lstItems.SelectedIndex == -1) return;
                Helpers.s_Leagues.SetCurrentLeague(this.lstItems.SelectedItem.ToString());
                result = DialogResult.Yes;
                this.Close();
                
            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }


        private void PopulateItems()
        {
            try {
                this.lstItems.Items.Clear();
                Helpers.s_Leagues = new Leagues();
                this.lstItems.Items.AddRange(Helpers.s_Leagues.ListLeagues().ToArray());

            }
            catch (Exception ex)
            {
                Helpers.Log(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ErrorLogger.LogLevel.ERROR);
                throw;
            }
        }


    }

}
