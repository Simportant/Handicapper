using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Handicapper
{
    public class Leagues : iLeagues
    {

        private List<string> _Leagues;
        private string _CurrentLeague = string.Empty;
        private string _CurrentFile = string.Empty;

        public Leagues()
        {   
            FindLeagues();
        }

        public List<string> ListLeagues()
        {
            return _Leagues;
        }
        
        private void FindLeagues()
        {
            _CurrentLeague = string.Empty;
            _Leagues = new List<string>();
            string fl = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath + "Leagues\\";            

            if (!Directory.Exists(fl))
                Directory.CreateDirectory(fl);
            
            foreach (string ztmp in Directory.GetDirectories(fl).ToList())
            {
                string dirName = new DirectoryInfo(ztmp).Name;
                _Leagues.Add(dirName);
            }
                        
        }
        public void CreateLeagues(string newName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath + "Leagues\\" + newName + "\\";
            Directory.CreateDirectory(path);

            using (StreamWriter sw = File.CreateText(path + "file.dat"))
            {
            }
            _Leagues.Add(newName);
        }       
        private void PopulateLeague(string file)
        {
            if (file.Length > 5)
            {
                Helpers.s_Players = new Players();
                Helpers.s_Rounds = new Rounds();

                try
                {
                    using (FileAccessor sr = new FileAccessor(file, Helpers.FileType.ForReading))
                    {
                        string line;
                        int PlayerID;
                        String[] lineElements;
                        Player plr;
                        Round rnd;

                        while ((line = sr.DataFile.ReadLine()) != null)
                        {
                            lineElements = line.Split(',');

                            if (lineElements[0].ToString() == "Player")
                            {

                                PlayerID = int.Parse(lineElements[1]);
                                plr = new Player(PlayerID)
                                {
                                    Name = lineElements[2],
                                    Actual = double.Parse(lineElements[3]),
                                    Playing = int.Parse(lineElements[4]),
                                    Category = int.Parse(lineElements[5]),
                                    Notes = lineElements[6]
                                };
                                Helpers.s_Players.AddPlayer(plr);
                            }
                            else if (lineElements[0].ToString() == "Round")
                            {
                                rnd = new Round
                                {
                                    PlayerID = int.Parse(lineElements[1]),
                                    Sequence = int.Parse(lineElements[2]),
                                    Date = lineElements[3],
                                    Course = lineElements[4],
                                    SSI = int.Parse(lineElements[5]),
                                    ActualStrokes = int.Parse(lineElements[6]),
                                    AdjustedStrokes = int.Parse(lineElements[7]),
                                    Score_Gross = int.Parse(lineElements[8]),
                                    Score_Net = int.Parse(lineElements[9]),
                                    HandicapUsed = int.Parse(lineElements[10]),
                                    Notes = lineElements[11]
                                };
                                Helpers.s_Rounds.AddRound(rnd);

                            }
                        }
                    }
                                   
                    foreach (Player plr in Helpers.s_Players.PlayersList)
                        plr.UpdateRoundsPlayed(Helpers.s_Rounds.RoundsPlayed(plr.PlayerID));
               
                    if (Helpers.s_Players.PlayersList.Count > 1)
                        Helpers.s_Players.SortPlayers();
                }

                catch { throw; }
            }
        }


        public bool doesLeagueExist(string name)
        {
            return (_Leagues.Exists(n => n.ToString() == name));
        }
        public void SetCurrentLeague(string nm)
        {
            _CurrentLeague = nm;
            _CurrentFile = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath + "Leagues\\" + _CurrentLeague + "\\file.dat";
            PopulateLeague(_CurrentFile);
        }
        public string GetCurrentLeague()
        {
            return _CurrentLeague;
        }
        public string GetCurrentLeagueFile()
        {
            return _CurrentFile;
        }
       
    }
}
