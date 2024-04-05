using UnityEngine.UI;

public abstract class ChoiceSimpleButton<T> : ChoiceButton<T>
{
    public virtual void Setup(T item, int index, ChoiceSimpleUI<T> ui)
    {
        _button = GetComponent<Button>();
        gameObject.SetActive(true);
        Item = item;
    }
}
