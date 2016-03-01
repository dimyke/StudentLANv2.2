using Domain.Entities;

namespace DAL.Repositories.Contracts
{
    public interface IConsumptionRepository
    {
        Consumption Find(int id);
       
    }
}
