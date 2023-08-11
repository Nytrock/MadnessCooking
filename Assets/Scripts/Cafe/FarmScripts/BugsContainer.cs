using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugsContainer : MonoBehaviour
{
    public GroundBed bed;
    public float MaxTimeSpawn;
    public float MinTimeSpawn;
    private float TimeToCreate;
    public float CurrentTime = 0;
    public int IndexBug;
    public bool DestroyActive = false;
    public int Count = 0;
    public bool HaveUpdate;

    void Start()
    {
        Count = gameObject.transform.childCount;
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
                    for (int i = 0; i < gameObject.transform.childCount; i++){
                        if (!gameObject.transform.GetChild(i).gameObject.activeSelf) {
                            sus.Add(gameObject.transform.GetChild(i).gameObject);
                            Isus.Add(i);
                        }
                    }
                    if (sus.Count == 0) {
                        IndexBug = -1;
                    } else {
                        int Rand = Random.Range(0, sus.Count);
                        IndexBug = Isus[Rand];
                        gameObject.transform.GetChild(IndexBug).gameObject.SetActive(true);
                        CurrentTime = 0;
                        TimeToCreate = Random.Range(MinTimeSpawn, MaxTimeSpawn);
                        bed.MultiplyPlantingBugs -= 0.02f;
                    }
                }
            }
        }
    }
}
