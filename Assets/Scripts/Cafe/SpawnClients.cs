using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClients : MonoBehaviour
{
    public Cafe cafe;
    public float ClientMaxTime;
    public float ClientMinTime;
    public float CurrentTime;
    public float NeedTime;
    public float SpeedTime = 1f;
    public float SpeedClient;
    public bool CoroutineWork = false;
    public Transform ClientsContainer;
    public Client client;
    public CloseAndOpen Close;
    public float TimeMultiply;
    public float FoodMultiply = 1f;
    public Transform DestroyT;

    void Start() {
        NeedTime = Random.Range(ClientMinTime, ClientMaxTime);
        CurrentTime = NeedTime;
    }

    void Update(){
        if (cafe.BusyPositions.Contains(false) && !Close.Close && !NewOrContinue.Education) {
            if (CurrentTime <= 0f) {
                NeedTime = Random.Range(ClientMinTime, ClientMaxTime);
                Spawn();
            } else {
                CurrentTime -= Time.deltaTime * SpeedTime * TimeMultiply / FoodMultiply;
            }
        }
    }

    public void UpdateSpeed()
    {
        for (int i = 1; i < ClientsContainer.childCount; i++) {
            ClientsContainer.GetChild(i).GetComponent<Client>().speed = SpeedClient;
            ClientsContainer.GetChild(i).GetComponent<Client>().speedTime = SpeedTime;
        }
    }

    public void Spawn()
    {
        var NewClient = Instantiate(client, ClientsContainer);
        NewClient.ActivateClient(cafe, this);
        NewClient.speed = SpeedClient;
        NewClient.speedTime = SpeedTime;
        CurrentTime = NeedTime;
    }
}
