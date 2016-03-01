using System;
using DAL.Repositories.Contracts;
using Domain.Entities;
using DAL;

namespace DAL.Repositories.EntitiyFramework
{
    public class ConsumptionRepository : IConsumptionRepository
    {
        private StulanContext _ctx = new StulanContext();
        public Consumption Find(int id)
        {
            return _ctx.Consumptions.Find(id);
        }
    }
}
