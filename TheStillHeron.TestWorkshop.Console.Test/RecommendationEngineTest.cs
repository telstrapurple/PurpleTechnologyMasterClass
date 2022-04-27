using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TheStillHeron.TestWorkshop.Console.Weather;

namespace TheStillHeron.TestWorkshop.Console.Test
{
    public class RecommendationEngineTest
    {
        [Test]
        public async Task When_Rain_Is_Predicted_Umbrella_Is_Recommended()
        {
            // ex.1
            // Arrange
            var engine = new RecommendationEngine(null);

            // Act
            var recommendation = await engine.GetRecommendation();

            // Assert
            Assert.AreEqual("Don't forget your umbrella!", recommendation);
        }
    }
}