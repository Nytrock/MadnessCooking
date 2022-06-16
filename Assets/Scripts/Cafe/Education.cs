using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Education : MonoBehaviour
{
    public List<GameObject> Images;
    public int Index;
    public AudioSource Press;
    public SpawnClients spawn;
    public GroundBed groundBed;
    public Chickens chickens;
    public CarTimer carTimer;
    public Fridge fridge;
    public ForOrders forOrders;
    public Save save;

    public void Start()
    {
        if (NewOrContinue.Education)
            Images[0].SetActive(true);
        else {
            for (int i = 0; i < Images.Count; i++)
                Destroy(Images[i].gameObject);
            Images.Clear();
        }
    }

    public void AddIndex(bool Pressed)
    {
        if (Index + 1 < Images.Count && NewOrContinue.Education) {
            Images[Index].SetActive(false);
            Index += 1;
            Images[Index].SetActive(true);
            if (Pressed)
                Press.Play();
            if (Index == 7)
                spawn.Spawn();
            if (Index == 36)
                groundBed.PanelActive();
            if (Index == 44)
                chickens.ActivePanel();
            if (Index == 48)
                fridge.ActivePanel();
            if (Index == 60)
                carTimer.NowTime = 0;
            if (Index == 68)
                forOrders.ActivePanel();
        } else {
            NewOrContinue.Education = false;
            for (int i = 0; i < Images.Count; i++)
                Destroy(Images[i].gameObject);
            save.SaveAll();
        }
    }
}
