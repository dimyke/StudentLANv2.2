
using DAL.Repositories.Contracts;
using DAL.Repositories.EntitiyFramework;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BL.Managers
{
    public class ConsumptionManager
    {
        private readonly IConsumptionRepository _ConsumptionRepository = new ConsumptionRepository();

        public Consumption Find(int id)
        {
            return _ConsumptionRepository.Find(id);
        }

        public IEnumerable<Consumption> All()
        {
            return _ConsumptionRepository.All();
        }
    }
}
