using System;
using UnityEngine;

public class CafeSeat : MonoBehaviour {
    [SerializeField] private Direction _seatDirection;

    public Direction SeatDirection => _seatDirection;

    public event Action<bool> StateChanged;

    public void ChangeSeatState(bool newState) {
        StateChanged?.Invoke(newState);
    }
}
