using System.Collections.Generic;
using System.Linq;

namespace FormulationEditor.WPF.Data.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
    {
        protected List<T> AllItems;

        public void Add(T model)
        {
            
            AllItems.Add(model);
        }

        public IEnumerable<T> GetAll()
        {
            return AllItems;
        }

        protected abstract int GetNextId();
    }
}
