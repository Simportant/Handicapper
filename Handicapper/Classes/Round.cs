

namespace Handicapper
{
    public class Round : iRound
    {
        public Round()
        { }

        public int PlayerID { get; set; }
        public int Sequence { get; set; }
        public string Date { get; set; }
        public string Course { get; set; }
        public int SSI { get; set; }
        public int ActualStrokes { get; set; }
        public int AdjustedStrokes { get; set; }
        public int Score_Gross { get; set; }
        public int Score_Net { get; set; }
        public int HandicapUsed { get; set; }
        public string Notes { get; set; }
    }
}
