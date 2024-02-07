using UnityEngine;

public class BuyableObject : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    [TextArea] public string Description;
    public int Cost;
}
