using Newtonsoft.Json;
using System;
using System.Text;

public class JsonConverter {
    public string Serialize<T>(T obj) {
        JsonSerializerSettings settings = new() {
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
        };
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
        return json;
    }

    public string SerializeCoded<T>(T obj) {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(Serialize(obj)));
    }

    public T Deserialize<T>(string json) {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public T DeserializeCoded<T>(string json) {
        return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(Convert.FromBase64String(json)));
    }
}
