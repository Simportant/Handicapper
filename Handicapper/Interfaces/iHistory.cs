using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicapper
{
    interface iHistory
    {

        Player GetPlayer(int ID);

        void AddNewPlayer(int ID);

    }
}
