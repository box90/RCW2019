using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCW2019.DAL;
using System.Linq;

namespace RWC2019.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async System.Threading.Tasks.Task TestMatchDetailAsync()
        {
            MatchesRepository rep = new MatchesRepository();
            var match = await rep.GetMatch(1, false);
            var events = match.Events.ToList();
            Assert.IsTrue(match.Id == 1);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestResultsAsync()
        {
            MatchesRepository rep = new MatchesRepository();
            var matches = await rep.GetResults();
            Assert.IsTrue(matches.Any());
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestStartAsync()
        {
            MatchesRepository rep = new MatchesRepository();
            var match = await rep.StartMatch(1);
            Assert.IsTrue(match);
        }
    }
}
