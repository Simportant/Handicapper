using System.Collections.Generic;

namespace Handicapper
{
    public interface iLeagues
    {
        void CreateLeagues(string newName);
        bool doesLeagueExist(string name);
        string GetCurrentLeague();
        string GetCurrentLeagueFile();
        List<string> ListLeagues();
        void SetCurrentLeague(string nm);
    }
}