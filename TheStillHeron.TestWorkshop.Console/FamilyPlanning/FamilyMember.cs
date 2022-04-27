using System;
using System.Collections.Generic;

namespace TheStillHeron.TestWorkshop.Console.FamilyPlanning
{
    public class FamilyMember
    {
        public string Name { get; set; }

        public IEnumerable<Chore> Chores { get; set; }
    }
}