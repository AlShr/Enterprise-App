using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Data
{
    public interface IHasAssignedId<TId>
    {
        void SetAssignedIdTo(TId assignedId);
    }
}
