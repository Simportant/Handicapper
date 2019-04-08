using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicapper
{
    interface iRound
    {
        int PlayerID { get; set; }
        int Sequence { get; set; }
        string Date { get; set; }
        string Course { get; set; }
        int SSI { get; set; }
        int ActualStrokes { get; set; }
        int AdjustedStrokes { get; set; }
        int Score_Gross { get; set; }
        int Score_Net { get; set; }
        int HandicapUsed { get; set; }
        string Notes { get; set; }
        

    }
}
