using Assignment2APIs.Manager.Repository;
using Assignment2Models.Models;

namespace Assignment2APIs.Manager;

public class TransactionManager : IDataRepository<Transaction, int>
{
    public TransactionManager()
    {
        
    }
    public IEnumerable<Transaction> GetAll()
    {
        throw new NotImplementedException();
    }

    public Transaction Get(int id)
    {
        throw new NotImplementedException();
    }

    public int Add(Transaction item)
    {
        throw new NotImplementedException();
    }

    public int Update(int id, Transaction item)
    {
        throw new NotImplementedException();
    }

    public int Delete(int id)
    {
        throw new NotImplementedException();
    }
}