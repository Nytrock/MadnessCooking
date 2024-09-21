using UnityEngine;

public class GameSaveManager : SaveManager<GameData> {
    protected override string _fileName => "save/save1";

    [ContextMenu("Save")]
    private void SaveGame() {
        if (!Application.isPlaying)
            return;

        Save();
    }
}
