using UnityEngine;

public class BaseObject : ScriptableObject
{
    public string Name;
    [TextArea] public string Description;
    public int Cost;
}
