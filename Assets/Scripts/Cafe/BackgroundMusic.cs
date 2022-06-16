using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource CafeMusic;
    public AudioSource KitchenMusic;
    public AudioSource FarmMusic;
    public AudioSource OfficeMusic;
    public Transform Camera;
    public Transform Menu;

    public void UpdateMusic()
    {
        if (Camera.position.x == -36.2f) {
            CafeMusic.Stop();
            OfficeMusic.Stop();
            KitchenMusic.Stop();
        } else if (Camera.position.x == -90.2f) {
            FarmMusic.Stop();
            CafeMusic.Stop();
            KitchenMusic.Stop();
        } else {
            FarmMusic.Stop();
            OfficeMusic.Stop();
        }
        Menu.position = new Vector2(Camera.position.x, Camera.position.y);
    }
}
