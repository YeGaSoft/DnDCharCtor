
internal interface IDatabaseService
{
    Task CreateTableAsync();
    Task<T?> GetItemAsync<T>(string primaryKey);
    Task<int> SaveItemAsync<T>(string primaryKey, T item);
    Task<int> DeleteItemAsync(string primaryKey);
}