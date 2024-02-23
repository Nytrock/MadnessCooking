using UnityEngine;

public class TechicUIActivator : UIActivator
{
    [SerializeField] private TechnicHolderUI _technicUI;
    private TechnicHolder _holder;

    private void Start()
    {
        _holder = transform.GetComponent<TechnicHolder>();
    }

    protected override void Press()
    {
        _technicUI.OpenTechnic(_holder);
    }
}
