using System;
using UnityEngine;

[Serializable]
public class DialogueChoiceData {
    [SerializeField] private string _text;
    [SerializeField] private Dialogue _nextDialogue;
}
