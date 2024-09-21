using System;
using UnityEngine;

[Serializable]
public class MessagePanelInfo {
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private string _submit;
    [SerializeField] private Vector2 _position;
    [SerializeField] private bool _isSubmitButton;

    public string Title => _title;
    public string Description => _description;
    public string Submit => _submit;
    public Vector2 Position => _position;
    public bool IsSubmitButton => _isSubmitButton;
}
