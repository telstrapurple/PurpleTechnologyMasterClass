using NUnit.Framework;
using Moq;
using TheStillHeron.TestWorkshop.Console.FamilyPlanning;

namespace TheStillHeron.TestWorkshop.Console.Test
{
    public class FamilyPlannerTest
    {
        [Test]
        public void When_Chore_Is_Due_It_Is_Listed_In_The_Plan()
        {
            // ex.2
            // Arrange
            var familyPlanner = FamilyPlanner.Basic();

            // Act
            var dayPlan = familyPlanner.DayPlan();

            // Assert
            // Hint: FamilyPlanner.Basic() makes it so that
            // Elizabeth sweeps on Tuesdays
            Assert.That(dayPlan.Contains("Elizabeth: Sweeping"));
        }
    }
}