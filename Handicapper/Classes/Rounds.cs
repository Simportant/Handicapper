using System.Collections.Generic;
using System.Linq;

namespace Handicapper
{
    public class Rounds
    {
        public Rounds()
        {
            AllRoundsList = new List<Round>();
        }

        public List<Round> RoundsCompleted(int PlayerID)
        {
            return AllRoundsList.FindAll(x => x.PlayerID == PlayerID).OrderByDescending(t => t.Sequence).ToList();
        }

        public List<Round> AllRoundsList { get; }

        public void AddRound(Round rnd)
        {
            AllRoundsList.Add(rnd);
        }

        public int RoundsPlayed(int PlayerID)
        {
            int cnt = 0;
            foreach (Round rnd in AllRoundsList)
            {
                if (rnd.PlayerID == PlayerID)
                    cnt += 1;
            }
            return cnt;
        }


    }
}
