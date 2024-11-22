using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Common.Services;

public class JsonSerializerService : IJsonSerializerService
{
    public T Deserialize<T>(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<T>(json) ?? throw new ArgumentException($"Could not Deseialize Type `{typeof(T).Name}` from JSON `{json}`");
    }

    public string Serialize<T>(T obj)
    {
        return System.Text.Json.JsonSerializer.Serialize(obj) ?? throw new ArgumentException($"Could not Serialize Type `{typeof(T).Name}` from Object `{obj}`");
    }
}
