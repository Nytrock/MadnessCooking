using Newtonsoft.Json;
using System;

public class ScriptableObjectConverter : JsonConverter<ExtendedScriptableObject> {
    public override ExtendedScriptableObject ReadJson(JsonReader reader, Type objectType, ExtendedScriptableObject existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer) {
        int id = Convert.ToInt32(reader.Value);
        return ScriptableObjectsDatabase.Instance.GetObject(id);
    }

    public override void WriteJson(JsonWriter writer, ExtendedScriptableObject scriptableObject, Newtonsoft.Json.JsonSerializer serializer) {
        int id = scriptableObject.ID;
        if (id == -1)
            throw new NullReferenceException($"No {scriptableObject.name} in global Scriptable Objects database.");

        writer.WriteValue(id);
    }
}
