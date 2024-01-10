using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDestroyBug : MonoBehaviour
{
    public GameObject NeedBug;
    public GameObject OriginalContainer;

    public void DestroyNeedBug()
    {
        OriginalContainer.GetComponent<BugsContainer>().bed.BugsDestroyVol.Play();
        NeedBug.SetActive(false);
        OriginalContainer.GetComponent<BugsContainer>().bed.MultiplyPlantingBugs += 0.025f;
        this.gameObject.SetActive(false);
    }

    public void NoActiveDestroy() {
        OriginalContainer.GetComponent<BugsContainer>().DestroyActive = false;
        OriginalContainer.GetComponent<BugsContainer>().IndexBug = -2;
        OriginalContainer.GetComponent<BugsContainer>().CurrentTime = 0;
    }
}
