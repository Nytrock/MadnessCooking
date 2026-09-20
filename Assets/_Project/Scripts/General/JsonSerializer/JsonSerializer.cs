using Newtonsoft.Json;
using System;
using System.Text;
using UnityEngine;

namespace MadnessCooking.General {
    public class JsonSerializer {
        private readonly JsonConverter[] _converters = {
            new UnityObjectJsonConverter<Color>(),
            new UnityObjectJsonConverter<Vector2>(),
            new UnityObjectJsonConverter<Vector3>(),
            new UnityObjectJsonConverter<Quaternion>(),
            new TimeSpanJsonConverter()
        };

        public string Serialize<T>(T obj) {
            JsonSerializerSettings settings = new() {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Converters = _converters
            };

            string json = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
            return json;
        }

        public string SerializeCoded<T>(T obj) {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Serialize(obj)));
        }

        public T Deserialize<T>(string json) {
            return JsonConvert.DeserializeObject<T>(json, _converters);
        }

        public T DeserializeCoded<T>(string json) {
            return Deserialize<T>(Encoding.UTF8.GetString(Convert.FromBase64String(json)));
        }
    }
}
