using System;

namespace Handicapper
{
    public class Player : iPlayer
    {

        private int _ID;
        private string _Name;
        private int _Category;
        private double _Actual;
        private int _Playing;
        private int _Rounds;
        private string _Notes;
        private bool _DataIsDirty;

        public Player(int ID)
        {
            _ID = ID;
            _DataIsDirty = false;
        }
        public Player()
        {
            _DataIsDirty = false;
        }
        public int PlayerID { get { return _ID; } }
        public bool DataIsDirty { get { return _DataIsDirty; } set { _DataIsDirty = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public int Category { get { return _Category; } set { _Category = value; } }
        public double Actual { get { return _Actual; } set { _Actual = value; } }
        public int Playing { get { return _Playing; } set { _Playing = value; } }
        public int RoundsPlayed { get { return _Rounds; } }
        public string Notes { get { return _Notes; } set { _Notes = value; } }
        
        public double CreateAdjustment(int Adjustment)
        {
            double newHandicap = 0.0;
            BufferCategory CurrentCategory = new BufferCategory();
            CurrentCategory = Helpers.s_Buffers.GetCategory(_Category);
            
            newHandicap = _Actual - Adjustment;
            _Notes = string.Concat("Handicap Adjustmend of ", Adjustment.ToString("#0.0"), " applied (", DateTime.Now.ToLongDateString(), ")");

            newHandicap = Math.Min(newHandicap, 28.0);
            _Playing = Convert.ToInt32(newHandicap);
            _Actual = Math.Round(newHandicap, 1);
            _Category = CheckCategory(_Actual, CurrentCategory);
            _DataIsDirty = true;
            return _Actual;
        }
        public void UpdateRoundsPlayed(int count)
        {
            _Rounds = count;
        }
        public double ReCalcHandicap(int NetScore, int thisRound)
        {
            double newHandicap = 0.0;
            BufferCategory CurrentCategory = new BufferCategory();
            CurrentCategory = Helpers.s_Buffers.GetCategory(_Category);

            if (thisRound == 3)
            {
                int tmp = NetScore;
                foreach (Round rnd in Helpers.s_Rounds.RoundsCompleted(_ID))
                    tmp += rnd.Score_Net;

                // New handicap as derived over 3 games.
                _Actual = FormatHandicap(tmp / 3);

                // Re-adjust Net Score as if using this handicap and later we 
                // adjust as if this last game was played using this handicap.
                NetScore = (NetScore - Convert.ToInt32(_Actual));
            }
                        
            if (NetScore > CurrentCategory.BufferValue)
            {
                //If Net Score > Buffer then Increase
                newHandicap = FormatHandicap(_Actual + CurrentCategory.Increase);
                _Notes = string.Concat("Handicap Increased from ", _Actual.ToString("#0.0"), " to ", newHandicap.ToString("#0.0"), " (", DateTime.Now.ToLongDateString(), ")");
            }
            else
            {                
                if (NetScore < 0)
                {
                    //Net Score < zero so reduction
                    newHandicap = FormatHandicap(_Actual - (Math.Abs(NetScore) * CurrentCategory.Reduction));
                    _Notes = string.Concat("Handicap Reduced from ", _Actual.ToString("#0.0"), " to ", newHandicap.ToString("#0.0"), " (", DateTime.Now.ToLongDateString(), ")");
                }                 
                else 
                {
                    //Net Score is within buffer so no Change to handicap.
                    newHandicap = _Actual;
                    _Notes = "Net Score within Buffer so no change";
                }
                
            }

            if (thisRound == 3)
                _Notes = string.Concat("First Handicap Initially set as ", newHandicap.ToString("#0.0"), " (", DateTime.Now.ToLongDateString(), ")");
           
            _Actual = newHandicap;
            _Playing = Convert.ToInt32(newHandicap);            
            _Category = CheckCategory(_Actual, CurrentCategory);
            _DataIsDirty = true;
            return _Actual;
        }
        private static double FormatHandicap(double newHandicap)
        {
            // Don't go below 0.1 after rounding to 1 DP.
            newHandicap = Math.Min(newHandicap, 28.0);
            newHandicap = Math.Max(newHandicap, 0.1);
            newHandicap = Math.Round(newHandicap, 1);
            return newHandicap;
        }
        private static int CheckCategory(double newHandicap, BufferCategory CurrentCategory)
        {
            if (newHandicap < CurrentCategory.Minimum)
                // If newHandicap is less than the minimum for the current Category then move down a Category
                // unless already in the minimum Category.
                return (CurrentCategory.Category == Helpers.s_Buffers.MinimumCategoryID()) ? CurrentCategory.Category : CurrentCategory.Category - 1;

            else if (newHandicap > CurrentCategory.Maximum)
                // If new is more than the maximum for the current Category then move up a Category
                // unless already in the maximum Category.
                return (CurrentCategory.Category == Helpers.s_Buffers.MaximumCategoryID()) ? CurrentCategory.Category : CurrentCategory.Category + 1;

            else
                // If still in the same Category then thats the one to return.
                return CurrentCategory.Category;
           
        }

       

    }
}
