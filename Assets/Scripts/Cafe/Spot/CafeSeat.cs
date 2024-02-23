using UnityEngine;

public class CafeSeat : MonoBehaviour
{
    public float GetSeatRotation()
    {
        return Mathf.Sign(transform.localScale.x);
    }
}
