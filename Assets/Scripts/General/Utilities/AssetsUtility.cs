using UnityEditor;
using UnityEngine;

public static class AssetsUtility {
    public static TAsset CreateAsset<TAsset>(string path, string assetName) where TAsset : ScriptableObject {
        TAsset asset = LoadAsset<TAsset>(path, assetName);
        if (asset != null)
            return asset;

        asset = ScriptableObject.CreateInstance<TAsset>();
        AssetDatabase.CreateAsset(asset, $"{path}/{assetName}.asset");
        return asset;
    }

    public static TAsset LoadAsset<TAsset>(string path, string assetName) where TAsset : ScriptableObject {
        return AssetDatabase.LoadAssetAtPath<TAsset>($"{path}/{assetName}.asset");
    }

    public static void Save(this Object asset) {
        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void RemoveAsset(string path, string asset) {
        AssetDatabase.DeleteAsset($"{path}/{asset}.asset");
    }
}
