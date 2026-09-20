using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    public class UnityObjectJsonConverter<TObject> : JsonConverter<TObject> {
        public override TObject ReadJson(JsonReader reader, Type objectType, TObject existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer) {
            JObject obj = JObject.Load(reader);
            string json = obj.ToString(Formatting.None);
            json = json.Replace("_", string.Empty);
            return JsonUtility.FromJson<TObject>(json);
        }

        public override void WriteJson(JsonWriter writer, TObject value, Newtonsoft.Json.JsonSerializer serializer) {
            string json = JsonUtility.ToJson(value);
            JObject obj = JObject.Parse(json);
            obj.WriteTo(writer);
        }
    }
}
