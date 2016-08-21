using System;
using ProjectBase.Utils;
using NHibernate.Search.Attributes;

namespace ProjectBase.Data
{
    public abstract class Entity<TId>
    {
        public virtual TId ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// must be provided two propertly compare two objects 
        /// </summary>
        /// <returns></returns>
        public abstract override int GetHashCode();


        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TId>);
        }

        private static bool IsTransient(Entity<TId> obj)
        {
            return obj != null &&
                Equals(obj.ID, default(TId));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(Entity<TId> other)
        {
            if (other == null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (!IsTransient(this) && !IsTransient(other) && Equals(id, other.ID))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                    otherType.IsAssignableFrom(thisType);
            }
            return false;
        }
        private TId id = default(TId);
    }
}
