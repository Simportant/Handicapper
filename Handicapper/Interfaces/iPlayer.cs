using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicapper
{
    interface iPlayer
    {
        Double ReCalcHandicap(int NetScore, int thisRound);
        double CreateAdjustment(int Adjustment);
        void UpdateRoundsPlayed(int count);
        int PlayerID { get; }
        string Name { get; set; }
        int Category { get; set; }
        double Actual { get; set; }
        int Playing { get; set; }
        string Notes { get; set; }
        int RoundsPlayed { get ; }
        bool DataIsDirty { get; set ; }
    }
}
