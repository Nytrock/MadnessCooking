using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngridientCell : MonoBehaviour
{
    public Image IngridientImage;
    public Text Count;
    public TextIngridient Description;
    public bool OnMouse;

    void Update()
    {
        if (OnMouse) {
            Description.gameObject.SetActive(true);
            Description.transform.position = new Vector2(Input.mousePosition.x + 150f, Input.mousePosition.y - 115f);
        } else {
            Description.gameObject.SetActive(false);
        }
    } 

    public void OnMouseEnter()
    {
        OnMouse = true;
    }

    public void OnMouseExit()
    {
        OnMouse = false;
    }
}
