using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class CriticUI : MonoBehaviour {
        [SerializeField] private MessagePanel _messagePanel;
        [SerializeField] private MessagePanelInfo _startMessageInfo;
        [SerializeField] private MessagePanelInfo _successMessageInfo;
        [SerializeField] private MessagePanelInfo _failureMessageInfo;

        public void SetMessage(CriticMessageType messageType) {
            switch (messageType) {
                case CriticMessageType.Start:
                    _messagePanel.SetInfo(_startMessageInfo);
                    break;
                case CriticMessageType.Success:
                    _messagePanel.SetInfo(_successMessageInfo);
                    break;
                case CriticMessageType.Failure:
                    _messagePanel.SetInfo(_failureMessageInfo);
                    break;
            }
        }
    }
}
