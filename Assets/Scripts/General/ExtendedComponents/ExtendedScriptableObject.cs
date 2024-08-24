using UnityEngine;

public class ExtendedScriptableObject : ScriptableObject {
    public virtual Sprite Icon => null;
    public int ID => ScriptableObjectsDatabase.Instance.GetId(this);
}
