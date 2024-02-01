using UnityEngine;
using UnityEngine.UI;

public class ClientWaitState : ClientState
{
    private ClientUI _clientUI;
    private Slider _waitSlider;
    private float _waitTime;
    private float _waitMultiplier;
    private float _nowTime;

    public override void EnterState(Client client)
    {
        _client = client;
        _clientUI = client.GetComponent<ClientUI>();

        _clientUI.SetUIVisible(true);
        _waitMultiplier = _client.WaitMultiplier;
        _waitTime = _client.GetWaitTime();
        _waitSlider = _clientUI.WaitSlider;
        _waitSlider.maxValue = _waitTime;

        _client.OrderActivated += DecreaseWait;
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        if (_nowTime < _waitTime) {
            _nowTime += Time.deltaTime;
            _waitSlider.value = _nowTime;
        } else {
            _client.Leave();
        }
    }

    private void DecreaseWait(Client _)
    {
        _nowTime = Mathf.Max(0, _nowTime - _waitTime * 0.1f * _waitMultiplier);
        _waitSlider.value = _nowTime;
    }
}
