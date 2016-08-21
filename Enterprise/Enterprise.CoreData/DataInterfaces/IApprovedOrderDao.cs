using ProjectBase.Data;
using Enterprise.CoreData.Domain;
using System;

namespace Enterprise.CoreData.DataInterfaces
{
    public interface IApprovedOrderDao : IDao<ApprovedOrder, long>
    {
        ApprovedOrder MakeApprovedOrder(Order order);
        void RecoveringApprovedOrderItem(Book book, ApprovedOrder order, DateTime date);
    }
}
