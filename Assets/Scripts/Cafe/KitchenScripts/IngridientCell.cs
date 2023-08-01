using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngridientCell : MonoBehaviour
{
    public Image IngridientImage;
    public TextMeshProUGUI Count;
    public TextIngridient Description;
    public bool OnMouse;

    void Update()
    {
        Description.gameObject.SetActive(OnMouse);

        if (OnMouse) {
            var v3 = Input.mousePosition;
            v3.z = 10f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            Description.transform.position = new Vector2(v3.x + 1.5f, v3.y - 1);
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
