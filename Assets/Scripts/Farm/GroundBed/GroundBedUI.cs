using UnityEngine;

public class GroundBedUI : MonoBehaviour
{
    [SerializeField] private GameObject _UI;

    private void Start()
    {
        _UI.SetActive(false);
    }

    public void ChangeMode()
    {
        _UI.SetActive(!_UI.activeSelf);
    }
}
