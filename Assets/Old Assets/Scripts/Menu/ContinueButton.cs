using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private string savePath;

    private void Start()
    {
        GetComponent<Button>().interactable = File.Exists(Application.dataPath + savePath);
    }
}
