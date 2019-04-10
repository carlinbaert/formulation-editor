using System.Collections.Generic;

namespace FormulationEditor.WPF.Data.Repositories
{
    public interface IGenericRepository<T>
    {
        void Add(T model);

        IEnumerable<T> GetAll();
    }
}
