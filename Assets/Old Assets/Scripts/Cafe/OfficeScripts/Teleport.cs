using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Animator PanelOrders;

    public void ActivePanel()
    {
        PanelOrders.SetBool("Active", !PanelOrders.GetBool("Active"));
    }
}
