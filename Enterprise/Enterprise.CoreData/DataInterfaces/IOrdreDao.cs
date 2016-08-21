using System;
using ProjectBase.Data;
using Enterprise.CoreData.Domain;

namespace Enterprise.CoreData.DataInterfaces
{
    public interface IOrdreDao : IDao<Order, long> 
    {
        Order MakeOrder(ReadingCart cart);
    }
}
