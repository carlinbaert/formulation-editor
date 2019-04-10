using System.Collections.Generic;
using System.Linq;

namespace FormulationEditor.WPF.Data.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
    {
        protected List<T> AllItems;

        public GenericRepository()
        {
            AllItems = new List<T>();
        }

        public void Add(T model)
        {
            SetNextId(model);

            AllItems.Add(model);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return AllItems;
        }

        protected abstract void SetNextId(T model);
    }
}
