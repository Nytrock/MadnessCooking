using UnityEngine;

public class GameSaveManager : SaveManager<GameData> {
    protected override string _fileName => "save/save1";

    public override void Save() {
        base.Save();
    }

    [ContextMenu("Save")]
    private void SaveByEditor() {
        if (!Application.isPlaying)
            return;

        Save();
    }

    [ContextMenu("Delete")]
    private void DeleteByEditor() {
        _dataService = new(_fileName);
        _dataService.Delete();
    }
}
