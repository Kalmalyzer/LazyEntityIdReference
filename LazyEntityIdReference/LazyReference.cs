using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyEntityIdReference
{
    public class LazyReference<T>
    {
        public delegate T DereferenceDelegate();

        private DereferenceDelegate Dereference;
        private T Obj;

        public LazyReference(DereferenceDelegate dereference)
        {
            Dereference = dereference;
        }

        public T Get()
        {
            if (Obj == null)
                Obj = Dereference();

            return Obj;
        }
    }
}
