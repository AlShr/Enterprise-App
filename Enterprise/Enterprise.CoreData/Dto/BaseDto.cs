using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.CoreData.Dto
{
    [Serializable]
    public abstract class BaseDto<TId>
    {
        public virtual TId ID
        {
            get { return id; }
            set { id = value; }
        }
        private TId id = default(TId);
    }
}
