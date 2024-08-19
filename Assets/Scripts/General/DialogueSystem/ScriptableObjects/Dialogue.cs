using System.Collections.Generic;
using UnityEngine;

public class Dialogue : ScriptableObject {
    [SerializeField] private string _name;
    [SerializeField, TextArea] private string _text;
    [SerializeField] private List<DialogueChoiceData> _choices;
    [SerializeField] private DialogueType _type;
    [SerializeField] private bool _isStartingDialogue;

    public void Initialize(string name, string text, List<DialogueChoiceData> choices, DialogueType type, bool isStartingDialogue) {
        _name = name;
        _text = text;
        _choices = choices;
        _type = type;
        _isStartingDialogue = isStartingDialogue;
    }
}
