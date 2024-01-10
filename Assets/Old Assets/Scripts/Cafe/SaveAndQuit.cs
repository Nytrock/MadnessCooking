using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveAndQuit : MonoBehaviour
{
    public CloseAndOpen Close;
    public Button This;
    public Save save;
    public AudioSource Press;

    void Update()
    {
        if (Close.Close)
            This.interactable = true;
        else
            This.interactable = false;
    }

    public void Click()
    {
        Press.Play();
        save.SaveAll();
        SceneManager.LoadScene(0);
    }
}
