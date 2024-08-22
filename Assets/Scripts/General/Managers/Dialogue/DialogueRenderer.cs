using UnityEngine;

public class DialogueRenderer : MonoBehaviour {
    [SerializeField] private DialogueContainer _dialogueContainer;
    [SerializeField] private DialogueGroup _dialogueGroup;
    [SerializeField] private Dialogue _dialogue;

    [SerializeField] private bool _isGroupedDialogues;
    [SerializeField] private bool _isStartingDialogues;

    [SerializeField] private int _selectedDialogueGroupIndex;
    [SerializeField] private int _selectedDialogueIndex;
}
