using UnityEngine;

public class InternetActivator : UIActivator
{
    [SerializeField] private InternetManager _internet;

    protected override void Press()
    {
        _internet.ChangeInternetState(true);
    }
}
