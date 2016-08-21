using System;
using ProjectBase.Data;
using Enterprise.CoreData.Domain;

namespace Enterprise.CoreData.DataInterfaces
{
    public interface IReaderCartSelectionDao : IDao<ReaderCartSelection, long>
    {
        ReaderCartSelection AddSelectionToReaderCart(Book bookDescription, ReadingCart cart);
    }
}
