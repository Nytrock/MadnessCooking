using UnityEngine;

public class CafeSeat : MonoBehaviour {
    public Direction GetSeatDirection() {
        return Mathf.Sign(transform.localScale.x).ToDirection();
    }
}
