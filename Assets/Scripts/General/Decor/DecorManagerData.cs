using System;
using System.Collections.Generic;

[Serializable]
public class DecorManagerData {
    private List<Decor> _availableDecor = new();

    public void AddDecor(Decor decor) {
        if (_availableDecor.Contains(decor))
            return;

        _availableDecor.Add(decor);
    }

    public IEnumerable<Decor> AvailableDecor => _availableDecor;
}
