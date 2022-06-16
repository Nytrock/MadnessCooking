using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour
{
    public Camera Looking;
    public Transform PositionLocation;
    public Sprite ActiveSprite;
    public Sprite NoActiveSprite;
    public Image Object;
    public AudioSource Music;
    public BackgroundMusic background;
    public AudioSource Press;

    void Update()
    {
        if (Looking.transform.position.x == PositionLocation.position.x && Looking.transform.position.y == PositionLocation.position.y) {
            Object.sprite = ActiveSprite;
        } else {
            Object.sprite = NoActiveSprite;
        }
    }

    public void SetCameraPosition()
    {
        Press.Play();
        if (PositionLocation.position.x != 0 && PositionLocation.position.x != -18.1f)
            Music.Play();
        else if (Looking.transform.position.x != 0 && Looking.transform.position.x != -18.1f)
            Music.Play();
        Looking.transform.position = new Vector3(PositionLocation.position.x, PositionLocation.position.y, -10f);
        background.UpdateMusic();
    }
}
