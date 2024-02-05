using UnityEngine;

public class BuyableObject : ScriptableObject
{
    public string Name;
    [TextArea] public string Description;
    public int Cost;
}
