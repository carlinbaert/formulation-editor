using FormulationEditor.Model;

namespace FormulationEditor.WPF.Data.Repositories
{
    public interface IFormulationRepository : IGenericRepository<Formulation>
    {
        Formulation GetById(int formulationId);
    }
}