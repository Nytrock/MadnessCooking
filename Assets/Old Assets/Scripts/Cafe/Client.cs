using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    public Cafe cafe;
    public Transform TargetPosition;
    public Transform TargetDestroy;
    private Animator anim;
    public GameObject ImageFood;
    public int NumberIn;
    public float speed;
    public float speedTime;
    public Food NeedFood;
    public int MaxWait;
    public int MinWait;
    public Slider WaitClient;
    public ButtonsYesNo yesNo;
    public GameObject Table;
    public List<SpriteRenderer> AllChapterClient;
    public List<Sprite> Heads;
    public List<Sprite> HairStyles;
    public List<Sprite> Eyes;
    public List<Sprite> Brows;
    public List<Sprite> Bodyes;
    public List<Sprite> Hands1;
    public List<Sprite> Hands2;
    public List<Sprite> Legs1;
    public List<Sprite> Legs2;
    public List<Sprite> Foots;
    private bool Layers = true;
    public bool Eating;
    public SpriteRenderer Eat;
    public List<AudioSource> StepVol;
    public AudioSource ClickNo;
    public AudioSource ClickYes;
    public AudioSource EatingVolume;
    public AudioSource SitVolume;

    void Update() {
        if (transform.position != TargetPosition.position) {
            anim.SetBool("IsWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition.position, Time.deltaTime * speed);
            ImageFood.SetActive(false);
        } else {
            anim.SetBool("IsWalking", false);
            ImageFood.SetActive(true);
            WaitClient.gameObject.SetActive(true);
            if (TargetPosition.localScale.x > 0 && transform.localScale.x > 0 || TargetPosition.localScale.x < 0 && transform.localScale.x < 0) {
                transform.localScale *= new Vector2(-1, 1);
                WaitClient.transform.localScale *= new Vector2(-1, 1);
                ImageFood.transform.localScale *= new Vector2(-1, 1);
                Table.SetActive(true);
            }
            if (Layers) {
                foreach (SpriteRenderer rend in AllChapterClient)
                    rend.sortingLayerName = "Default";
                Layers = false;
                if (cafe.Camera.position.x == 0f){
                    SitVolume.Play();
                } else {
                    SitVolume.Stop();
                }
            }
            if (WaitClient.value < WaitClient.maxValue && !NewOrContinue.Education) {
                if (!Eating)
                    WaitClient.value += Time.deltaTime * speedTime;
                else { 
                    WaitClient.value += Time.deltaTime;
                    if (cafe.Camera.position.x == 0 && !EatingVolume.isPlaying)
                        EatingVolume.Play();
                    else if (EatingVolume.isPlaying && cafe.Camera.position.x != 0f)
                        EatingVolume.Stop();
                }
            } else if (!NewOrContinue.Education) {
                EatingVolume.Stop();
                yesNo.Click();
            }
        }

        if (transform.position == TargetDestroy.position)
            Destroy(this.gameObject);
    }

    public void ActivateClient(Cafe coffe, SpawnClients spawn)
    {
        int count = Eyes.Count;
        AllChapterClient[0].sprite = Eyes[Random.Range(0, count)]; 
        AllChapterClient[1].sprite = HairStyles[Random.Range(0, count)]; 
        AllChapterClient[2].sprite = Heads[Random.Range(0, count)]; 
        AllChapterClient[3].sprite = Brows[Random.Range(0, count)];
        int BodyCount = Random.Range(0, count);
        AllChapterClient[4].sprite = Bodyes[BodyCount];
        AllChapterClient[5].sprite = Hands1[BodyCount];
        AllChapterClient[8].sprite = Hands1[BodyCount];
        AllChapterClient[6].sprite = Hands2[BodyCount];
        AllChapterClient[7].sprite = Hands2[BodyCount];
        BodyCount = Random.Range(0, count);
        AllChapterClient[9].sprite = Legs1[BodyCount];
        AllChapterClient[12].sprite = Legs1[BodyCount];
        AllChapterClient[10].sprite = Legs2[BodyCount];
        AllChapterClient[13].sprite = Legs2[BodyCount];
        BodyCount = Random.Range(0, count);
        AllChapterClient[11].sprite = Foots[BodyCount];
        AllChapterClient[14].sprite = Foots[BodyCount];
        cafe = coffe;
        foreach (SpriteRenderer rend in AllChapterClient)
            rend.sortingLayerName = "Weapon";
        List<int> Pos = new List<int>();
        for (int i = 0; i < cafe.BusyPositions.Count; i++) {
            if (!cafe.BusyPositions[i]) {
                Pos.Add(i);
            }
        }
        int Rand = Pos[Random.Range(0, Pos.Count)];
        if (NewOrContinue.Education)
            Rand = 0;
        TargetPosition = cafe.PositionsClients[Rand];
        cafe.BusyPositions[Rand] = true;
        NumberIn = Rand;
        WaitClient.value = 0;
        float Multi = cafe.PositionsClients.Count * 0.5f * 0.25f;
        if (Multi < 1f)
            Multi = 1f;
        WaitClient.maxValue = Random.Range(MinWait * Multi, MaxWait * Multi);
        anim = GetComponent<Animator>();
        TargetDestroy = spawn.DestroyT;
        cafe.AvailableClients[NumberIn] = this;
    }

    public void GoToDestroy() {
        if (cafe.Camera.position.x == 0)
            SitVolume.Play();
        TargetPosition = TargetDestroy;
        cafe.BusyPositions[NumberIn] = false;
    }

    public void Step() {
        if (cafe.Camera.position.x == 0) { 
            int i = Random.Range(0, StepVol.Count);
            StepVol[i].pitch = Random.Range(0.7f, 1.05f);
            StepVol[i].Play();
        }
    }
}
