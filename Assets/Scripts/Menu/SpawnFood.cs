using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnFood : MonoBehaviour
{
    public float TimeSpawn;
    public Transform Container;
    public Image FoodMenu;
    public float MinTime;
    public float MaxTime;
    public float MinForce;
    public float MaxForce;
    public float MinForceLeft;
    public float MaxForceLeft;
    public List<int> Numbers;
    public List<Sprite> Sprites;

    void Start()
    {
        TimeSpawn = Random.Range(MinTime, MaxTime);
    }

    void Update() {
        if (TimeSpawn < 0) {
            var food = Instantiate(FoodMenu, Container);
            food.gameObject.transform.position = new Vector2(Random.Range(0f, 1920f), -90f);
            food.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(MinForce, MaxForce));
            food.GetComponent<Rigidbody2D>().AddForce(new Vector2(Numbers[Random.Range(0, 2)], 0) * Random.Range(MinForceLeft, MaxForceLeft));
            food.sprite = Sprites[Random.Range(0, Sprites.Count)];
            TimeSpawn = Random.Range(MinTime, MaxTime);
        } else {
            TimeSpawn -= Time.deltaTime;
        }
    }
}
