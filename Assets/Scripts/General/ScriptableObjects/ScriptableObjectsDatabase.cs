using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectsDatabase : Singleton<ScriptableObjectsDatabase> {
    [SerializeField] private List<ExtendedScriptableObject> _scriptableObjects;
    [SerializeField] private string _objectsFolder;

    [ContextMenu("CheckObjectsLocalization")]
    private void CheckObjectsLocalization() {
        if (!Application.isPlaying)
            return;

        foreach (var scriptableObject in _scriptableObjects) {
            if (scriptableObject as BuyableItem != null) {
                BuyableItem item = scriptableObject as BuyableItem;
                if (!LocalizationManager.Instance.CheckLocalizationExists(item.Name))
                    Debug.LogError($"No translation found for {item.Name}");
                if (!LocalizationManager.Instance.CheckLocalizationExists(item.Description))
                    Debug.LogError($"No translation found for {item.Description}");
                break;
            }
        }
    }

    [ContextMenu("AddMissingObjects")]
    private void AddMissingObjects() {
        List<ExtendedScriptableObject> assets = AssetsUtility.GetAssets<ExtendedScriptableObject>(_objectsFolder);
        foreach (var asset in assets)
            if (!_scriptableObjects.Contains(asset))
                _scriptableObjects.Add(asset);
    }

    public int GetId(ExtendedScriptableObject scriptableObject) {
        return _scriptableObjects.IndexOf(scriptableObject);
    }

    public ExtendedScriptableObject GetObject(int id) {
        return _scriptableObjects[id];
    }
}
