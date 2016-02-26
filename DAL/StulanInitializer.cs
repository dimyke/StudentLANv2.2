using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Domain.Entities;

namespace DAL
{
    public class StulanInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<StulanContext>
    {
        protected override void Seed(StulanContext context)
        {
            var consumptions = new List<Consumption>
            {
                new Consumption {ConsumptionId=1, Name="Frieten", Price=2 },
                new Consumption {ConsumptionId=2, Name="Crocskes", Price=2 },
                new Consumption {ConsumptionId=3, Name="Kevin zijn moeder", Price=0 },
                new Consumption {ConsumptionId=4, Name="Bitterballen", Price=2 },
                new Consumption {ConsumptionId=5, Name="Pizza", Price=2 }
            };

            consumptions.ForEach(s => context.Consumptions.Add(s));
            context.SaveChanges();

        }

    }
}
