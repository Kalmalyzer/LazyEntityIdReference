using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyEntityIdReference
{
    public class EntityIdRegistry<T> where T : class
    { 
        private Dictionary<int, T> IdToEntity = new Dictionary<int, T>();
        private Dictionary<T, int> EntityToId = new Dictionary<T, int>();
        private int Counter;

        public int Add(T item)
        {
            IdToEntity[Counter] = item;
            EntityToId[item] = Counter;
            return Counter++;
        }

        public T FromId(int id)
        {
            if (id != 0)
                return IdToEntity[id];
            else
                return null;
        }

        public int ToId(T item)
        {
            if (item != null)
                return EntityToId[item];
            else
                return 0;
        }
    }

}
