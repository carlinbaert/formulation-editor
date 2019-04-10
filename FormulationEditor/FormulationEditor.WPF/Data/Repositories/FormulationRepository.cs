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

        protected override void SetNextId(Formulation model)
        {
            if (AllItems.Count == 0)
                model.Id = 1;
            else
                model.Id = AllItems.Select(m => m.Id).Max() + 1;
        }
    }
}
