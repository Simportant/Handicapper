using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

namespace Handicapper
{
    public class Players
    {
        public Players()
        {
            PlayersList = new BindingList<Player>(); 
        }

        public BindingList<Player> PlayersList { get; private set; }

        public void SortPlayers()
        {
            // To Sort(Player.Name) we first OrderBy into a new List<T>, then use this List to create a new BindingList<T>
            // because the Enumberable.OrderBy method returns an IOrderedEnumberable<T>, so need to create a new BindingList<T> from that return value.
            List<Player> sortedList = Helpers.s_Players.PlayersList.OrderBy(x => x.Name).ToList();
            PlayersList = new BindingList<Player>(sortedList);
        }

        public void AddPlayer(Player plr)
        {
            PlayersList.Add(plr);
        }

        public Player GetPlayer(int ID)
        {
            return PlayersList.Single(Q => Q.PlayerID == ID);
        }

        public int NewPlayerID()
        {
            if (PlayersList.Count == 0)
                return 1;
            else
                return PlayersList.Max(t => t.PlayerID) + 1;
        }

        public bool DoesPlayerExist(string name)
        {
            // This is a BindingList<T> so the method Exists() or Lambda Contains<> is not available to us.
            foreach (Player plr in PlayersList)
            {
                if (plr.Name == name)
                    return true;
            }
            return false;
        }

        public bool PlayerDataChanged()
        {
            foreach (Player plr in PlayersList)
            {
                if (plr.DataIsDirty)
                    return true;
            }
            return false;
        }


    }
}
