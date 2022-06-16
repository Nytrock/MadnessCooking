using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ContinueButton : MonoBehaviour
{
    public Button This;

    void Update()
    {
        if (File.Exists(Application.dataPath + "/save/MadnessCookingSave.txt"))
            This.interactable = true;
        else
            This.interactable = false;
    }
}
