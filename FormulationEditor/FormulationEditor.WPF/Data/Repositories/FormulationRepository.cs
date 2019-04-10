using FormulationEditor.Model;
using System.Collections.Generic;
using System.Linq;

namespace FormulationEditor.WPF.Data.Repositories
{
    public class FormulationRepository : GenericRepository<Formulation>, IFormulationRepository
    {
        public FormulationRepository()
        {
            AllItems = new List<Formulation>();
        }

        public Formulation GetById(int formulationId)
        {
            return AllItems.FirstOrDefault(f => f.Id == formulationId);
        }

        protected override int GetNextId()
        {
            if (AllItems.Count == 0)
                return 1;

            var maxId = AllItems.Select(m => m.Id).Max();

            return maxId + 1;
        }
    }
}
