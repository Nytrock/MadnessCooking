using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CloseAndOpen : MonoBehaviour
{
    public bool Close;
    public TextMeshProUGUI TextClose;
    public Cafe cafe;
    public AudioSource Change;

    public void Pressed(){
        Change.Play();
        if (Close) {
            TextClose.text = LocalizationManager.GetTranslate("Открыто");
        } else {
            TextClose.text = LocalizationManager.GetTranslate("Закрыто");
            cafe.Closed();
        }
        Close = !Close;
    }
}
