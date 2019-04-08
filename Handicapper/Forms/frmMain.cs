using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Handicapper
{
    public partial class frmMain : Form
    {
        
        bool _DataHasChanged = false;

        public frmMain()
        {
            InitializeComponent();
            this.Text = string.Concat(this.Text, " (Version ", System.Reflection.Assembly.GetEntryAssembly().GetName().Version, ")");
            if (Debugger.IsAttached)
                this.mnuTesting.Visible = true;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
        private void mnuOpenLeague_Click(object sender, EventArgs e)
        {
           
            try {
                FileUpdate();

                frmLeague lg = new frmLeague();
                lg.ShowDialog();

                if (lg.DialogResult == DialogResult.Yes)
                    PopulateDataGrid();

                lg.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Helpers.Title);
            }
        }
        private void mnuAddPlayer_Click(object sender, EventArgs e)
        {
            try {

                frmAddNew ln = new frmAddNew(Helpers.NewType.Player);
                ln.ShowDialog();
                if (ln.DialogResult == DialogResult.Yes)
                    PopulateDataGrid();
                ln.Dispose();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Helpers.Title);
            }
        }
        private void mnuTesting_Click(object sender, EventArgs e)
        {
            bool numeric = true;
            string val = "078";

            numeric = Helpers.IsNumeric(val);

            if (numeric)
                MessageBox.Show($"{val} is a number and CASTS as {Convert.ToDouble(val)} ");
            else
                MessageBox.Show("Not Numeric");

        }
        private void dgvPlayers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
            try {
                // If something is selected then try and open it
                if (e.RowIndex == -1) { return; }

                int ID = (int)this.dgvPlayers.Rows[e.RowIndex].Cells[0].Value;

                frmPlayerHistory ln = new frmPlayerHistory(ID);
                ln.ShowDialog();                                
                if (ln.DialogResult == DialogResult.Yes)
                {
                    _DataHasChanged = true;
                    PopulateDataGrid();
                }

                ln.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Helpers.Title);
            }
        }
        private void dgvPlayers_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

            // Do not automatically paint the focus rectangle.
            e.PaintParts &= ~DataGridViewPaintParts.Focus;

            // Determine whether the cell should be painted
            // with the custom selection background.
            if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
            {
                // Calculate the bounds of the row.
                Rectangle rowBounds = new Rectangle(
                    this.dgvPlayers.RowHeadersWidth, 
                    e.RowBounds.Top,
                    this.dgvPlayers.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) - this.dgvPlayers.HorizontalScrollingOffset + 1,
                    e.RowBounds.Height);

                // Paint the custom selection background.
                using (Brush backbrush =
                    new System.Drawing.Drawing2D.LinearGradientBrush(rowBounds,
                        this.dgvPlayers.DefaultCellStyle.SelectionBackColor,
                        e.InheritedRowStyle.ForeColor,
                        System.Drawing.Drawing2D.LinearGradientMode.Horizontal))

                {
                    e.Graphics.FillRectangle(backbrush, rowBounds);
                }

            }

        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileUpdate();
        }
        
        private void PopulateDataGrid()
        {
            try {
                this.dgvPlayers.DataSource = null;
                if (this.dgvPlayers.Rows.Count > 0)
                {
                    this.dgvPlayers.Rows.Clear();
                    this.dgvPlayers.Refresh();
                }

                this.dgvPlayers.DataSource = Helpers.s_Players.PlayersList;
                this.dgvPlayers.Columns[0].Visible = false;
                this.dgvPlayers.Columns[1].Visible = false;
                Helpers.StretchLastColumn(this.dgvPlayers);
                this.mnuAddPlayer.Enabled = true;

                foreach (DataGridViewRow rw in this.dgvPlayers.Rows)
                {
                    if (Convert.ToInt32(rw.Cells[6].Value) < 3)
                        rw.DefaultCellStyle.ForeColor = Color.Red;
                    else
                        rw.DefaultCellStyle.ForeColor = Color.Black;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Helpers.Title);
            }
        }
        private void FileUpdate()
        {
            try {
                if (_DataHasChanged || (Helpers.s_Players != null && Helpers.s_Players.PlayerDataChanged()))
                {
                    // re-write the whole file.
                    FileAccessor sr = new FileAccessor(Helpers.s_Leagues.GetCurrentLeagueFile(), Helpers.FileType.ForWriting);
                    sr.Dispose();
                    _DataHasChanged = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Helpers.Title);
            }
        }

       
    }

}
