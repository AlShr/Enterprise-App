using System.Reflection;
using Enterprise.CoreData.Domain;
using ProjectBase.Utils;
using ProjectBase.Data;

namespace Enterprise.NUnitTests.Data.Domain
{
    public class EntityIdSetter<TId>
    {
        public void SetIdOf(Entity<TId> domainObject, TId id)
        {
            PropertyInfo idProperty = domainObject.GetType().GetProperty(NAMEOFIDMEMBER,
                BindingFlags.Public | BindingFlags.Instance);
            Check.Ensure(idProperty != null, "idProperty could not be found");

            idProperty.SetValue(domainObject, id, null);
        }

        private const string NAMEOFIDMEMBER = "ID";
    }
}
