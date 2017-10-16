using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyEntityIdReference
{
    public class LazyEntityIdReference<T> where T : class
    { 
        private EntityIdRegistry<T> EntityIdRegistry;
        private int Id;
        private T Entity;

        public LazyEntityIdReference(EntityIdRegistry<T> entityIdRegistry, int id)
        {
            EntityIdRegistry = entityIdRegistry;
            Id = id;
        }

        public T Get()
        {
            if (Entity == null)
            {
                Console.WriteLine("Resolving entity ID");
                Entity = EntityIdRegistry.FromId(Id);
            }

            return Entity;
        }

        public static implicit operator T(LazyEntityIdReference<T> entity)
        {
            return entity.Get();
        }
    }
}
