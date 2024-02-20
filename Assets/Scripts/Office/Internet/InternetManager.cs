using UnityEngine;

public class InternetManager : MonoBehaviour
{
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private Animator _internetAnimator;


    public void ChangeInternetState(bool newState)
    {
        _cameraAnimator.SetBool("isInternet", newState);
        _internetAnimator.SetBool("isInternet", newState);
    }
}
