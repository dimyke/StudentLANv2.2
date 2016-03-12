
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

        //haalt een comsumptie op aan de hand van id
        public Consumption Find(int id)
        {
            return _ConsumptionRepository.Find(id);
        }

        //geeft alle consumpties terug
        public IEnumerable<Consumption> All()
        {
            return _ConsumptionRepository.All();
        }

        //geeft alle beschikbare consumpties op het huidige evnement terug.
        public IEnumerable<Consumption> AllAvaible()
        {
            return _ConsumptionRepository.AllAvaible();
        }

        //maakt een consumptie aan
        public void Create(Consumption consumption)
        {
            _ConsumptionRepository.Create(consumption);
        }

        //Een update van een consumptie
        public void Update(int id, Consumption consumption)
        {
            _ConsumptionRepository.Update(id, consumption);
        }

        //het verwijderen van een consumptie
        public void Delete(int id)
        {
            _ConsumptionRepository.Delete(id);
        }
    }
}
