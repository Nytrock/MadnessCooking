using UnityEngine;
using UnityEngine.UI;

public class ClientWaitState : ClientState
{
    private Slider _waitSlider;
    private float _waitTime;
    private float _waitMultiplier;
    private float _nowTime;

    public override void EnterState(Client client)
    {
        var clientUI = client.GetComponent<ClientUI>();

        clientUI.ChangeFoodChoiceState(true);
        clientUI.ChangeSliderState(true);
        _waitMultiplier = client.WaitMultiplier;
        _waitTime = client.GetWaitTime();
        _nowTime = 0;
        _waitSlider = clientUI.WaitSlider;
        _waitSlider.maxValue = _waitTime;

        client.OrderActivated += DecreaseWait;
    }

    public override void ExitState(Client client)
    {

    }

    public override void UpdateState(Client client)
    {
        if (_nowTime < _waitTime) {
            _nowTime += Time.deltaTime * TimeManager.instance.TimeSpeed;
            _waitSlider.value = _nowTime;
        } else {
            client.Leave();
        }
    }

    private void DecreaseWait(Client _)
    {
        _nowTime = Mathf.Max(0, _nowTime - _waitTime * 0.1f * _waitMultiplier);
        _waitSlider.value = _nowTime;
    }
}
