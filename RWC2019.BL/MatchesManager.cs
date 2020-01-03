using RCW2019.DAL;
using RWC2019.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWC2019.BL
{
    public static class MatchesManager
    {
        private static MatchesRepository repo = new MatchesRepository();
        public static async Task<List<Match>> GetMatchesAsync()
        {
            return (await repo.GetResults()).ToList();
        }

        public static async Task<MatchDetail> GetMatchDetailAsync(int id)
        {
            return await repo.GetMatch(id, false);
        }

        public static async Task<bool> StartMatchAsync(int id)
        {
            return await repo.StartMatch(id);
        }
    }
}
