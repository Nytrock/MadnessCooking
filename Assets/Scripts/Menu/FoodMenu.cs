using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMenu : MonoBehaviour
{
    void Update()
    {
        if (this.gameObject.transform.position.y < -150)
            Destroy(this.gameObject);
    }
}
