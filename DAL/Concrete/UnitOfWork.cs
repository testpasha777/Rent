using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope transaction;

        public void CommitTransaction()
        {
            transaction.Complete();
        }

        public void Dispose()
        {
            if(transaction != null)
            {
                transaction.Dispose();
            }
        }

        public void StartTransaction()
        {
            transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
