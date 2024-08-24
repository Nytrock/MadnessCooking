using Newtonsoft.Json;

public class JsonSerializer {
    public string Serialize<T>(T obj) {
        JsonSerializerSettings settings = new() {
            NullValueHandling = NullValueHandling.Ignore,
        };
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
        return json;
        // return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
    }

    public T Deserialize<T>(string json) {
        return JsonConvert.DeserializeObject<T>(json);
        // Encoding.UTF8.GetString(Convert.FromBase64String(json));
    }
}
