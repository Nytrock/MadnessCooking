using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectsDatabase : Singleton<ScriptableObjectsDatabase> {
    [SerializeField] private List<ExtendedScriptableObject> _scriptableObjects;
    [SerializeField] private string _objectsFolder;

    protected void Start() {
        foreach (var scriptableObject in _scriptableObjects)
            if (scriptableObject != null && scriptableObject is BuyableItem item)
                item.Initialize();
    }

#if UNITY_EDITOR
    [ContextMenu("AddMissingObjects")]
    private void AddMissingObjects() {
        List<ExtendedScriptableObject> assets = AssetsUtility.GetAssets<ExtendedScriptableObject>(_objectsFolder);
        foreach (var asset in assets)
            if (!_scriptableObjects.Contains(asset))
                _scriptableObjects.Add(asset);
    }
#endif

    public int GetId(ExtendedScriptableObject scriptableObject) {
        return _scriptableObjects.IndexOf(scriptableObject);
    }

    public ExtendedScriptableObject GetObject(int id) {
        return _scriptableObjects[id];
    }
}
