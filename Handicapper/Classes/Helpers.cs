using System;
using System.IO;

namespace Handicapper
{
    public static class Helpers
    {

        public static Players s_Players;
        public static Rounds s_Rounds;
        public static BufferCategories s_Buffers;
        public static Leagues s_Leagues;

        public static string Title { get { return "Handicapper"; } }

        public static string OpenFile(FileType tp)
        {
            System.Windows.Forms.FileDialog dlg;

            if (tp == FileType.ForReading)
                dlg = new System.Windows.Forms.OpenFileDialog();
            else
                dlg = new System.Windows.Forms.SaveFileDialog();

            dlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return dlg.FileName;
            else
                return string.Empty;
        }

        public enum FileType { ForReading, ForWriting, ForLogging, ForExtracting }
        public enum NewType { Player, League, }


        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static void Log(string Message, string Source, ErrorLogger.LogLevel Level)
        {
            ErrorLogger log = new ErrorLogger(Message, Source, Level);
        }

        public static void StretchLastColumn(this System.Windows.Forms.DataGridView dataGridView)
        {
            var lastColIndex = dataGridView.Columns.Count - 1;
            var lastCol = dataGridView.Columns[lastColIndex];
            lastCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        }

        public static string NullToString(this object value)
        {
            return (value ?? string.Empty).ToString();
        }
        public static int NullToInt(this object value)
        {
            return (value == null || value == DBNull.Value)  ? 0 : Convert.ToInt32(value);
        }
        public static double NullToDouble(this object value)
        {
            return (value == null || value == DBNull.Value) ? 0 : Convert.ToDouble(value);
        }

        public static bool IsNumeric(string anyString)
        {
           
            if ( (anyString != null) && (anyString != string.Empty) )
            {
                // Can now declare this in-line.
                //double dummyOut = new double();
                System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-US", true);
                return Double.TryParse(anyString, System.Globalization.NumberStyles.Any, cultureInfo.NumberFormat, out double dummyOut);
            }
            else
            {
                return false;
            }
        }

       
    }
}
