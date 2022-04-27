using NUnit.Framework;
using TheStillHeron.TestWorkshop.SoccerApi.Commands;
using TheStillHeron.TestWorkshop.SoccerApi.DataStorage;
using TheStillHeron.TestWorkshop.SoccerApi.Domain;
using TheStillHeron.TestWorkshop.SoccerApi.Repository;

namespace TheStillHeron.TestWorkshop.SoccerApi.Test
{
    public class Tests
    {
        [Test]
        public void Can_Create_A_Season()
        {
            // Arrange
            var dataStore = new DataStore();
            var repo = new SeasonRepository(dataStore);
            var seasonName = "Home and Away";
            var year = "2022";

            // Act
            var createCommand = new CreateSeason(repo);
            createCommand.Execute(seasonName, year);

            // Assert
            var getSeasonCommand = new GetSeason(repo);
            var fetchedSeason = getSeasonCommand.Execute(year);

            Assert.AreEqual(seasonName, fetchedSeason.Name);
        }

        [Test]
        public void Can_Add_Match_To_Season()
        {
            // Arrange
            var dataStore = new DataStore();
            var repo = new SeasonRepository(dataStore);
            var seasonName = "Home and Away";
            var year = "2022";
            var roundNumber = 1;
            var homeTeam = "Manchester United";
            var awayTeam = "Tottenham";
            var matchNumber = 1;

            // Act
            var createCommand = new CreateSeason(repo);
            createCommand.Execute(seasonName, year);

            var addRoundCommand = new AddRound(repo);
            addRoundCommand.Execute(year, roundNumber);

            var addMatchCommand = new AddMatch(repo);
            addMatchCommand.Execute(
                seasonYear: year,
                roundNumber,
                homeTeam,
                awayTeam,
                matchTime: new System.DateTime(2022, 5, 3),
                matchNumber
            );

            // ex.3
            // How can we fetch the match back?
            // At the moment we'd need to construct the controller
            // and invoke a method on it - that's frustrating!
            Match match = null;

            Assert.IsNotNull(match);
            Assert.AreEqual(homeTeam, match.HomeTeam);
            Assert.AreEqual(awayTeam, match.AwayTeam);
            Assert.IsNull(match.Winner);
            Assert.AreEqual(matchNumber, match.MatchNumber);
        }
    }
}