namespace Api.DAL;

/// <summary>
/// We can use this abstraction to have a mock database (which I'm using only).
/// Everything is async because I assumed SQL queries will be mainly async.
/// </summary>
/// <typeparam name="T">T is the type of model</typeparam>
public interface ITable<T>
{
    ValueTask<T?> FindByIdOrDefaultAsync(int id);
    ValueTask<IEnumerable<T>> GetAllAsync();
}
