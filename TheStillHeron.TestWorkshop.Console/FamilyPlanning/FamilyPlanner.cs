using System;
using System.Collections.Generic;
using System.Linq;

namespace TheStillHeron.TestWorkshop.Console.FamilyPlanning
{
    public class FamilyPlanner
    {
        public IList<FamilyMember> FamilyMembers { get; set; }

        public string DayPlan()
        {
            // ex.2
            var plan = "";

            foreach (var member in FamilyMembers)
            {
                var chores = member.Chores
                    .Where(x => x.IsDue())
                    .Select(x => $"{member.Name}: {x.Name}");

                if (chores.Count() > 0)
                {
                    plan += chores.Aggregate((total, item) => $"{total}\n{item}");
                }
                else
                {
                    plan += $"{member.Name}: No chores today";
                }
            }

            return plan == "" ? "No chores today" : plan;
        }

        public static FamilyPlanner Basic()
        {
            return new FamilyPlanner
            {
                FamilyMembers = new List<FamilyMember>
                {
                    new FamilyMember
                    {
                        Name = "Elizabeth",
                        Chores = new List<Chore>
                        {
                            new Chore
                            {
                                Name = "Sweeping",
                                Cadence = ChoreCadence.Weekly,
                                OriginDate = new DateTime(2022, 01, 01) // Saturday
                            }
                        }
                    }
                }
            };
        }
    }
}