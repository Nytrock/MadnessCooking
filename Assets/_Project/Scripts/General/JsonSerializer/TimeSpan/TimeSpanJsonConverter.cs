using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public class TimeSpanJsonConverter : JsonConverter<TimeSpan> {
    public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer) {
        JObject obj = JObject.Load(reader);
        string json = obj.ToString(Formatting.None);
        SerializableTimeSpan serializableTimeSpan = JsonUtility.FromJson<SerializableTimeSpan>(json);
        return serializableTimeSpan.ToTimeSpan();
    }

    public override void WriteJson(JsonWriter writer, TimeSpan value, Newtonsoft.Json.JsonSerializer serializer) {
        SerializableTimeSpan serializableTimeSpan = new(value);
        string json = JsonUtility.ToJson(serializableTimeSpan);
        JObject obj = JObject.Parse(json);
        obj.WriteTo(writer);
    }
}
