﻿using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Common.Services;
using SQLite;
using System.IO;
using System.Threading.Tasks;

internal class DatabaseService : IDisposable, IDatabaseService
{
    private const string DbName = "dnd_char_ctor.db";

    private readonly IJsonSerializerService _serializer;

    private readonly SQLiteAsyncConnection _database;

    public DatabaseService(IJsonSerializerService serializer)
    {
        _serializer = serializer;

        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DbName);
        _database = new SQLiteAsyncConnection(dbPath);
    }

    public async Task CreateTableAsync()
    {
        await _database.CreateTableAsync<Entity>().ConfigureAwait(false);
    }

    public async Task<T?> GetItemAsync<T>(string primaryKey)
    {
        await CreateTableAsync().ConfigureAwait(false);

        var entity = await _database.FindAsync<Entity>(primaryKey).ConfigureAwait(false);
        if (entity is null) return default;
        return _serializer.Deserialize<T>(entity.JsonValue);
    }

    public async Task<int> SaveItemAsync<T>(string primaryKey, T item)
    {
        await CreateTableAsync().ConfigureAwait(false);

        var jsonValue = _serializer.Serialize(item);
        var entity = new Entity() { Key = primaryKey, JsonValue = jsonValue };
        return await _database.InsertOrReplaceAsync(entity).ConfigureAwait(false);
    }

    public async Task<int> DeleteItemAsync(string primaryKey)
    {
        await CreateTableAsync().ConfigureAwait(false);

        var entity = await _database.FindAsync<Entity>(primaryKey).ConfigureAwait(false);
        if (entity is null) return 0;
        return await _database.DeleteAsync(primaryKey).ConfigureAwait(false);
    }



    ~DatabaseService() => Dispose();

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _database.CloseAsync().ConfigureAwait(false).SafeFireAndForget(null);
    }
}

internal class Entity
{
    [PrimaryKey]
    public string Key { get; set; } = string.Empty;

    // Yes, it is stupid to store Json in a Database.
    // But our interface (the user of this service) only has keys to access data and it wants the deserialized object.
    // The consumer does not want to do db-operations and thus we have a similar usage like `LocalStorage`, `SecureStorage` or `Preferences`
    public string JsonValue { get; set; } = string.Empty;
}