using System;

namespace Enterprise.Model
{
    [Serializable]
    public class CompositeKey
    {
        public CompositeKey()
        { }

        public CompositeKey(long key1, long key2)
        {
            class1Key = key1;
            class2Key = key2;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(CompositeKey))
            {
                return false;
            }
            else 
            {
                CompositeKey ck = (CompositeKey)obj;
                if (this.class1Key == ck.class1Key && this.class2Key == ck.class2Key)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return class1Key.GetHashCode() + class2Key.GetHashCode();
        }

        public long class1Key;
        public long class2Key;
    }
}
