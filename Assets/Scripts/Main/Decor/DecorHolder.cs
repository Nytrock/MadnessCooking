using UnityEngine;

public class DecorHolder : MonoBehaviour
{
    [SerializeField] private Decor _decor;

    public Decor Decor => _decor;

    public void ChangeState(bool newValue)
    {
        gameObject.SetActive(newValue);
    }
}
