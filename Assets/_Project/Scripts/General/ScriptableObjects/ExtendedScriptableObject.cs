using UnityEngine;

namespace MadnessCooking.General {
    public abstract class ExtendedScriptableObject : ScriptableObject {
        public virtual Sprite Icon => null;
        public int ID => ScriptableObjectsDatabase.Instance.GetId(this);
    }
}
