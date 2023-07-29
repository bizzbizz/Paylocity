using Api.Models;

namespace Api.DAL.Mock;

public class EmployeeMockTable : ITable<Employee>
{
    public async ValueTask<Employee?> FindByIdOrDefaultAsync(int id)
        => await ValueTask.FromResult(MockDataStore.Employees.TryGetValue(id, out var employee) ? employee : null);

    public async ValueTask<IEnumerable<Employee>> GetAllAsync()
        => await ValueTask.FromResult(MockDataStore.Employees.Values);
}
