using UnityEngine;
using UnityEngine.UI;

public class SpotEditorChooseButtons : MonoBehaviour
{
    [SerializeField] private Button[] chooseButtons;

    public void ChangeButtonNumber(int freeSpace)
    {
        for (int i = 0; i < chooseButtons.Length; i++)
            chooseButtons[i].interactable = i < freeSpace;
    }
}
