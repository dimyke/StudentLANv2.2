using System;
using DAL.Repositories.Contracts;
using Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using DAL;
using System.Linq;

namespace DAL.Repositories.EntitiyFramework
{
    public class ConsumptionRepository : IConsumptionRepository
    {
        private readonly StulanContext _ctx = new StulanContext();
        public Consumption Find(int id)
        {
            return _ctx.Consumptions.Find(id);
        }

        public IEnumerable<Consumption> All()
        {
            return _ctx.Consumptions.AsEnumerable();
        }

        public void Create(Consumption consumption)
        {
            _ctx.Consumptions.Add(consumption);
            _ctx.SaveChanges();
        }

        public void Update(int id, Consumption consumption)
        {
            _ctx.Entry(_ctx.Consumptions.Find(id)).CurrentValues.SetValues(consumption);
            _ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            _ctx.Consumptions.Remove(_ctx.Consumptions.Find(id));
            _ctx.SaveChanges();
        }



    }
}
