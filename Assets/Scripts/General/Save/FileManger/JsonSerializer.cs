using System.Text;
using System;
using UnityEngine;

public class JsonSerializer
{
    public string Serialize<T>(T obj)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonUtility.ToJson(obj)));
    }

    public T Deserialize<T>(string json)
    {
        return JsonUtility.FromJson<T>(Encoding.UTF8.GetString(Convert.FromBase64String(json)));
    }
}
