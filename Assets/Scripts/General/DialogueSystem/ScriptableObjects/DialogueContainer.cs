using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : ScriptableObject {
    [SerializeField] private string _fileName;
    [SerializeField] private SerializableDictionary<DialogueGroup, List<Dialogue>> _groups;
    [SerializeField] private List<Dialogue> _ungroupedDialogues;

    public void Initialize(string fileName) {
        _fileName = fileName;
        _groups = new();
        _ungroupedDialogues = new();
    }
}
