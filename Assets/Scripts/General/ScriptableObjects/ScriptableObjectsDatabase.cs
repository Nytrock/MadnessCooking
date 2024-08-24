using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectsDatabase : Singleton<ScriptableObjectsDatabase> {
    [SerializeField] private List<ExtendedScriptableObject> _scriptableObjects;

    public int GetId(ExtendedScriptableObject scriptableObject) {
        return _scriptableObjects.IndexOf(scriptableObject);
    }

    public ExtendedScriptableObject GetObject(int id) {
        return _scriptableObjects[id];
    }
}
