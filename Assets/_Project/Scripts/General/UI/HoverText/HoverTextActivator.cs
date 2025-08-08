public class HoverTextActivator : HoverObjectStateChanger {
    protected HoverTextPanel _hoverPanel;
    protected string _textToShow;

    protected override void ActivateHoverObject() {
        base.ActivateHoverObject();
        ShowText();
    }

    protected override void DisableHoverObject() {
        base.DisableHoverObject();
        HideText();
    }

    protected virtual void ShowText() {
        _hoverPanel.ShowText(_textToShow);
    }

    protected virtual void HideText() {
        _hoverPanel.ChangeState(false);
    }

    public void SetHoverPanel(HoverTextPanel hoverText) {
        _hoverPanel = hoverText;
    }
}
