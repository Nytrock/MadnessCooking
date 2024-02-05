using UnityEngine;

public class TechnicPanel : MonoBehaviour
{
    protected TechnicHolder _nowTechnic;

    public virtual void UpdatePanel()
    {

    }

    public virtual void UpdateInfo() { }

    public void SetNewTechnic(TechnicHolder technic)
    {
        _nowTechnic = technic;
        enabled = true;
    }

    public void ChangeState()
    {
        enabled = !enabled;
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ChangeState(bool newState)
    {
        enabled = newState;
        gameObject.SetActive(newState);
    }
}
