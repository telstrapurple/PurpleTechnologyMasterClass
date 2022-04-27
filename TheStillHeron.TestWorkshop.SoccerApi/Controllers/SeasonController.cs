using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheStillHeron.TestWorkshop.SoccerApi.Commands;
using TheStillHeron.TestWorkshop.SoccerApi.DataStorage;
using TheStillHeron.TestWorkshop.SoccerApi.Domain;
using TheStillHeron.TestWorkshop.SoccerApi.Repository;

namespace TheStillHeron.TestWorkshop.SoccerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeasonController : ControllerBase
    {
        private IDataStore _storage;
        private SeasonRepository _seasonRepository;

        public SeasonController(IDataStore storage)
        {
            _storage = storage;
        }

        private readonly ILogger<SeasonController> _logger;

        public SeasonController(ILogger<SeasonController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<Season> Get()
        {
            var currentSeason = Environment.GetEnvironmentVariable("CURRENT_SEASON");
            var command = new GetSeason(_seasonRepository);
            return command.Execute(currentSeason);
        }

        [HttpGet("/match")]
        public ActionResult<Match> GetMatch(int roundNumber, int matchNumber)
        {
            // ex.3
            var currentSeason = _seasonRepository.GetCurrentSeason();
            var match = currentSeason
                .Rounds.Where(x => x.RoundNumber == roundNumber)
                .FirstOrDefault()
                ?.Matches.Where(x => x.MatchNumber == matchNumber)
                .FirstOrDefault();

            if (match == null)
            {
                return new NotFoundResult();
            }

            return match;
        }

        [HttpPost]
        public void Post(PostSeason postBody)
        {
            var command = new CreateSeason(_seasonRepository);
            command.Execute(postBody.Name, postBody.Year);
        }

        [HttpPost("/round")]
        public void PostRound(PostRound postBody)
        {
            var currentSeason = Environment.GetEnvironmentVariable("CURRENT_SEASON");
            var command = new AddRound(_seasonRepository);
            command.Execute(currentSeason, postBody.RoundNumber);
        }

        [HttpPost("/match")]
        public void PostMatch(PostMatch postBody)
        {
            var currentSeason = Environment.GetEnvironmentVariable("CURRENT_SEASON");
            var command = new AddMatch(_seasonRepository);
            command.Execute(
                seasonYear: currentSeason,
                roundNumber: postBody.RoundNumber,
                homeTeam: postBody.HomeTeam,
                awayTeam: postBody.AwayTeam,
                matchTime: postBody.MatchTime,
                matchNumber: postBody.MatchNumber
            );
        }
    }
}
