using Domain.Entities;
using System.Collections.Generic;

namespace DAL.Repositories.Contracts
{
    public interface IConsumptionRepository
    {
        Consumption Find(int id);
        IEnumerable<Consumption> All();
        IEnumerable<Consumption> AllAvaible();
        void Create(Consumption consumption);
        void Update(int id, Consumption consumption);
        void Delete(int id);




    }
}
