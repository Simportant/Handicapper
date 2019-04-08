using System;
using System.Text;
using System.IO;


namespace Handicapper
{
   
    public class FileAccessor : ifile, IDisposable
    {        
        
        private FileStream _Stream;
        private StreamWriter _Writer;
        private FileStatus _FileStatus;
        private readonly Helpers.FileType _FileType;
        private bool _disposed;
       
        private enum FileStatus { Open, Closed, Writing, Error }
       
        public FileAccessor(string fileName, Helpers.FileType FileType)
        {
            _FileStatus = FileStatus.Closed;
            _FileType = FileType;           
            OpenFile(fileName);
        }

        public StreamReader DataFile { get; private set; }
        private void OpenFile(string FileName)
        {
            if (_FileStatus == FileStatus.Closed)
            {
                try
                {
                    if (_FileType == Helpers.FileType.ForReading)
                    {
                        _Stream = new FileStream(FileName, FileMode.Open);
                        DataFile = new StreamReader(_Stream);
                        _FileStatus = FileStatus.Open;
                    }
                    else if (_FileType == Helpers.FileType.ForWriting)
                    {
                        BackupOldFile(FileName);
                        _Stream = new FileStream(FileName, FileMode.Append);
                        _Writer = new StreamWriter(_Stream);
                        _FileStatus = FileStatus.Open;
                        UpdateDataFile();
                        CloseFile();
                    }
                    else if (_FileType == Helpers.FileType.ForLogging)
                    {
                        _Stream = new FileStream(FileName, FileMode.Append);
                        _Writer = new StreamWriter(_Stream);
                        _FileStatus = FileStatus.Open;                     
                    }
                    else if (_FileType == Helpers.FileType.ForExtracting)
                    {
                        _Stream = new FileStream(FileName, FileMode.Create);
                        _Writer = new StreamWriter(_Stream);
                        _FileStatus = FileStatus.Open;
                    }
                }
                catch(Exception ex)
                {
                    CloseFile();                    
                    throw new Exception("Could not open file: " + ex.Message);
                }
            }
        }
        private void CloseFile()
        {
            try
            {
                if (_FileStatus != FileStatus.Closed)
                {
                    if (_FileType == Helpers.FileType.ForReading)
                        DataFile.Dispose();
                    else
                        _Writer.Dispose();

                    _Stream.Dispose();
                    _FileStatus = FileStatus.Closed;
                }
            }
            catch { throw; }
        }       
        private void BackupOldFile(string FileName)
        {
            int cnt = 1;
            string newName = FileName + "_" + cnt.ToString(); 
            
            while (File.Exists(newName))
            {
                cnt += 1;
                newName = FileName + "_" + cnt.ToString();

                if (cnt > 99)
                    throw new System.ArgumentException("Too many backups in", FileName);
            }

            File.Move(FileName, newName);

        }
        public void WriteEntry(string Entry)
        {
            _Writer.WriteLine(Entry);
        }        
        private void UpdateDataFile()
        {
            try { 
                //Write Entry to new file
                char delim = ',';
                StringBuilder str = new StringBuilder();
            
                foreach (Player plr in Helpers.s_Players.PlayersList)
                {
                    str.Clear();
                    str.Append("Player").Append(delim);
                    str.Append(plr.PlayerID.ToString()).Append(delim);
                    str.Append(plr.Name.ToString()).Append(delim);
                    str.Append(plr.Actual.ToString()).Append(delim);
                    str.Append(plr.Playing.ToString()).Append(delim);
                    str.Append(plr.Category.ToString()).Append(delim);
                    str.Append(plr.Notes.ToString());
                    WriteEntry(str.ToString());
                }
                _Writer.Flush();

                foreach (Round rnd in Helpers.s_Rounds.AllRoundsList)
                {
                    str.Clear();
                    str.Append("Round").Append(delim);
                    str.Append(rnd.PlayerID.ToString()).Append(delim);
                    str.Append(rnd.Sequence.ToString()).Append(delim);
                    str.Append(rnd.Date.ToString()).Append(delim);
                    str.Append(rnd.Course.ToString()).Append(delim);
                    str.Append(rnd.SSI.ToString()).Append(delim);
                    str.Append(rnd.ActualStrokes.ToString()).Append(delim);
                    str.Append(rnd.AdjustedStrokes.ToString()).Append(delim);
                    str.Append(rnd.Score_Gross.ToString()).Append(delim);
                    str.Append(rnd.Score_Net.ToString()).Append(delim);
                    str.Append(rnd.HandicapUsed.ToString()).Append(delim);
                    str.Append(rnd.Notes.ToString()).Append(delim);
                    WriteEntry(str.ToString());
                }
                _Writer.Flush();
                str = null;
            }

            catch(Exception)
            {
                CloseFile();
                throw;
            }
        }
                
            

        ~FileAccessor()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here.
                }

                // Dispose unmanaged resources here.
                CloseFile();
            }

            _disposed = true;
        }



    }
}
