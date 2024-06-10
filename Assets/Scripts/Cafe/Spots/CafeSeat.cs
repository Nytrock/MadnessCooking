using UnityEngine;

public class CafeSeat : MonoBehaviour {
    [SerializeField] private Direction _seatDirection;

    public Direction SeatDirection => _seatDirection;
}
