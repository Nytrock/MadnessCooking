using UnityEngine.UI;

public abstract class ChoiceSimpleButton<TItem> : ChoiceButton<TItem> where TItem: BuyableObject
{
    public virtual void Setup(TItem item, int index, ChoiceSimpleUI<TItem> ui)
    {
        _button = GetComponent<Button>();
        gameObject.SetActive(true);
        Item = item;
    }
}
