using System;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithCount : ItemInfoRenderer {
    [SerializeField] private CountRenderer _count;

    public void SetCount(int count) {
        _count.UpdateCount(count);
    }

    public override void ResetInfo() {
        base.ResetInfo();
        _count.ResetText();
    }
}
