using UnityEngine;

public abstract class TechnicPanel : MonoBehaviour
{
    protected TechnicHolder _nowTechnic;

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


    public abstract void UpdateInfo();
    public abstract void UpdatePanel();
}
