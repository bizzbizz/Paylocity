using Api.Models;

namespace Api.DAL.Mock;

public class DependentMockTable : ITable<Dependent>
{
    public async ValueTask<Dependent?> FindByIdOrDefaultAsync(int id)
        => await ValueTask.FromResult(MockDataStore.Dependents.TryGetValue(id, out var dependent) ? dependent : null);

    public async ValueTask<IEnumerable<Dependent>> GetAllAsync()
        => await ValueTask.FromResult(MockDataStore.Dependents.Values);
}
