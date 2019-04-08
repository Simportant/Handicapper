using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicapper
{
    class History : iHistory
    {
        private List<Player> _Players;

        public History()
        {
            // Open History Database
            // Add each Player into the _Players List


        }

        public void AddNewPlayer(int ID)
        {
            throw new NotImplementedException();
        }

        public Player GetPlayer(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
