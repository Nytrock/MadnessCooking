using UnityEngine;

[RequireComponent(typeof(TechnicHolder))]
public class TechicUIActivator : UIActivator
{
    [SerializeField] private TechnicHolderUI _technicUI;
    private TechnicHolder _holder;

    private void Awake()
    {
        _holder = GetComponent<TechnicHolder>();
    }

    protected override void Press()
    {
        _technicUI.OpenTechnic(_holder);
    }
}
