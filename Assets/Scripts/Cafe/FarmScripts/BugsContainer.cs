using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugsContainer : MonoBehaviour
{
    public GroundBed bed;
    public float TimeToCreate;
    private float MaxTimeSpawn;
    private float MinTimeSpawn;
    public float CurrentTime = 0;
    public int IndexBug;
    public bool DestroyActive = false;
    public int Count = 0;
    public bool HaveUpdate;

    void Start()
    {
        Count = this.gameObject.transform.childCount;
        MaxTimeSpawn = TimeToCreate + 45;
        MinTimeSpawn = TimeToCreate + 5;
        TimeToCreate = Random.Range(MinTimeSpawn, MaxTimeSpawn);
    }

    void Update()
    {
        if (!DestroyActive && !HaveUpdate) {
            if (CurrentTime < TimeToCreate){
                CurrentTime += Time.deltaTime;
            } else {
                if (IndexBug != -1) {
                    List<GameObject> sus = new List<GameObject>();
                    List<int> Isus = new List<int>();
                    for (int i = 0; i < this.gameObject.transform.childCount; i++){
                        if (!this.gameObject.transform.GetChild(i).gameObject.activeSelf) {
                            sus.Add(this.gameObject.transform.GetChild(i).gameObject);
                            Isus.Add(i);
                        }
                    }
                    if (sus.Count == 0) {
                        IndexBug = -1;
                    } else {
                        int Rand = Random.Range(0, sus.Count);
                        IndexBug = Isus[Rand];
                        this.gameObject.transform.GetChild(IndexBug).gameObject.SetActive(true);
                        CurrentTime = 0;
                        TimeToCreate = Random.Range(MinTimeSpawn, MaxTimeSpawn);
                        bed.MultiplyPlantingBugs -= 0.025f;
                    }
                }
            }
        }
    }
}
