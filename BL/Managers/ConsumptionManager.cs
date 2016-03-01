
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

        public void Create(Consumption consumption)
        {
            _ConsumptionRepository.Create(consumption);
        }

        public void Update(int id, Consumption consumption)
        {
            _ConsumptionRepository.Update(id, consumption);
        }

        public void Delete(int id)
        {
            _ConsumptionRepository.Delete(id);
        }
    }
}
