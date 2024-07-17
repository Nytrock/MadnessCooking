using UnityEngine;

public class OrderUIBaseState : MonoBehaviour {
    [SerializeField] protected OrderUIState _state;

    public virtual void UpdateState(OrderUIState newState) {
        gameObject.SetActive(newState == _state);
    }
}
