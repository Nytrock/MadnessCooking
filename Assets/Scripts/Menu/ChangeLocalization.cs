using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeLocalization : MonoBehaviour
{
    private int nowLang = 0;
    private int langCount;
    private LocalizationManager manager;

    private void Start() {
        langCount = LocalizationManager.numLanguages;
        manager = LocalizationManager.localizationManager;
    }

    public void PreviousLanguage()
    {
        if (nowLang - 1 >= 0)
            nowLang--;
        else
            nowLang = langCount - 1;

        manager.SetLanguage(nowLang);
    }

    public void NextLanguage()
    {
        if (nowLang + 1 < langCount)
            nowLang++;
        else
            nowLang = 0;

        manager.SetLanguage(nowLang);
    }
}
