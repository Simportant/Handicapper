using System;
using System.IO;
using System.Text;

namespace Handicapper
{
    public class ErrorLogger
    {
        private static FileStream _Stream;
        private static StreamWriter _Writer;
        static readonly string _outFile;

        public enum LogLevel { ALL, DEBUG, INFO, WARN, ERROR, FATAL }

        static ErrorLogger()
        {
            try
            {
                string outPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Logs\\";
                if (!System.IO.Directory.Exists(outPath))
                    System.IO.Directory.CreateDirectory(outPath);

                _outFile = outPath + "Handicapper.log";
            }
            catch (Exception) { throw; }
        }


        public ErrorLogger(string Message, string Source, LogLevel Level)
        {
            try
            {
                _Stream = new FileStream(_outFile, FileMode.Append);
                _Writer = new StreamWriter(_Stream);

                StringBuilder str = new StringBuilder();
                str.Append(DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToString("dd/MM/yyyy") + " ");
                str.Append(Level.ToString());
                str.Append(": [" + Source + "] - ");
                str.Append(Message);
                _Writer.WriteLine(str.ToString());
                str = null;
                _Writer.Close();
                _Stream.Close();
            }
            catch (Exception) { throw; }
        }

    }
}
