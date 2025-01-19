using System;

public class InternetSearchPage : InternetPage {
    public event Action PageLoaded;

    public void UpdateName(string name) {
        _pageName = name;
    }

    public override void ChangeState(bool newValue) {
        base.ChangeState(newValue);
        if (newValue)
            PageLoaded?.Invoke();
    }
}
