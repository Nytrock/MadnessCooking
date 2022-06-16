using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MenuButtons : MonoBehaviour
{
    public Animator PanelConfirm;
    public Animator PanelEducation;
    public Animator PanelSettings;
    public AudioSource ButtonPress;
    public SaveSound saveSound;

    public void Start()
    {
        if (File.Exists(Application.dataPath + "/save/MadnessCookingSaveSound.txt"))
            saveSound.LoadSound();
    }

    public void ExitGame()
    {
        ButtonPress.Play();
        saveSound.SaveAllSound();
        Application.Quit();
    }

    public void ContinueGame()
    {
        ButtonPress.Play();
        NewOrContinue.Continue = true;
        NewOrContinue.Education = true;
        SceneManager.LoadScene(1);
    }

    public void StartGame(bool Confirm)
    {
        ButtonPress.Play();
        if (!File.Exists(Application.dataPath + "/save/MadnessCookingSave.txt") || Confirm) {
            NewOrContinue.Continue = false;
            CloseConfirm();
            PanelEducation.SetBool("Active", true);
        } else {
            PanelConfirm.SetBool("Active", true);
        }
    }

    public void CloseConfirm()
    {
        ButtonPress.Play();
        PanelConfirm.SetBool("Active", false);
    }

    public void EducationConfirm(bool choise)
    {
        ButtonPress.Play();
        NewOrContinue.Education = choise;
        SceneManager.LoadScene(1);
    }

    public void ActivePanelSettings()
    {
        ButtonPress.Play();
        PanelSettings.SetBool("Active", !PanelSettings.GetBool("Active"));
        saveSound.SaveAllSound();
    }
}
