using UnityEditor;

public static class EditorFoldersUtility {
    public static void CreateFolder(string path, string folderName) {
        if (AssetDatabase.IsValidFolder($"{path}/{folderName}"))
            return;

        AssetDatabase.CreateFolder(path, folderName);
    }

    public static void RemoveFolder(string path) {
        FileUtil.DeleteFileOrDirectory($"{path}.meta");
        FileUtil.DeleteFileOrDirectory($"{path}/");
    }
}
