using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ClockAnimator : MonoBehaviour {
	
	private const float
		hoursToDegrees = 360f / 12f,
		minutesToDegrees = 360f / 60f,
		secondsToDegrees = 360f / 60f;
	
	public Transform hours, minutes;
	public TimeSpan timespan = new TimeSpan(9, 0, 0);
	public int MultiplyTime;
	public SpawnClients spawnClient;
	public bool Morning;
	public bool Day;
	public bool Evening;
	public bool Night;
	public Sprite MorningSprite;
	public Sprite DaySprite;
	public Sprite EveningSprite;
	public Sprite NightSprite;
	public Sprite NightGround;
	public Sprite MorningGround;
	public Sprite Ground;
	public List<Image> Skyes;
	public List<Image> Grounds;
	public bool TimeSlow;
	public AudioSource SlowVolume;
	public AudioSource FastVolume;

	void Start()
    {
		SkyChange();
		TimeSlow = true;
	}

	private void Update()
	{
		if (TimeSlow) {
			timespan = timespan.Add(new TimeSpan(0, 0, 1 * MultiplyTime));
			hours.localRotation = Quaternion.Euler(
					0f, 0f, (float)timespan.TotalHours * -hoursToDegrees);
			minutes.localRotation = Quaternion.Euler(
				0f, 0f, (float)timespan.TotalMinutes * -minutesToDegrees);
			if ((int)timespan.TotalHours % 24 == 17 && Day){
				for (int i = 0; i < Skyes.Count; i++)
					Skyes[i].sprite = EveningSprite;
				for (int i = 0; i < Grounds.Count; i++)
					Grounds[i].sprite = Ground;
				Day = false;
				Evening = true;
				spawnClient.TimeMultiply = 0.8f;
			}else if ((int)timespan.TotalHours % 24 == 22 && Evening){
				for (int i = 0; i < Skyes.Count; i++)
					Skyes[i].sprite = NightSprite;
				for (int i = 0; i < Grounds.Count; i++)
					Grounds[i].sprite = NightGround;
				Evening = false;
				Night = true;
				spawnClient.TimeMultiply = 0.1f;
			}else if ((int)timespan.TotalHours % 24 == 4 && Night){
				for (int i = 0; i < Skyes.Count; i++)
					Skyes[i].sprite = MorningSprite;
				for (int i = 0; i < Grounds.Count; i++)
					Grounds[i].sprite = MorningGround;
				Night = false;
				Morning = true;
				spawnClient.TimeMultiply = 0.8f;
			}else if ((int)timespan.TotalHours % 24 == 10 && Morning){
				for (int i = 0; i < Skyes.Count; i++)
					Skyes[i].sprite = DaySprite;
				for (int i = 0; i < Grounds.Count; i++)
					Grounds[i].sprite = Ground;
				Morning = false;
				Day = true;
				spawnClient.TimeMultiply = 1f;
			}
			TimeSlow = false;
		} else {
			TimeSlow = true;
        }
	}

	public void ChangeSppeed(int ThisSpeed)
    {
		if (MultiplyTime != 1 * ThisSpeed) {
			MultiplyTime = 1 * ThisSpeed;
			spawnClient.SpeedTime = 1 * ThisSpeed;
			spawnClient.SpeedClient = 3 * ThisSpeed;
			FastVolume.Play();
		} else {
			MultiplyTime = 1;
			spawnClient.SpeedTime = 1;
			spawnClient.SpeedClient = 3;
			SlowVolume.Play();
		}
		spawnClient.UpdateSpeed();
    }

	public void SkyChange()
    {
		if ((int)timespan.TotalHours % 24 >= 17 && (int)timespan.TotalHours % 24 < 22) {
			for (int i = 0; i < Skyes.Count; i++)
				Skyes[i].sprite = EveningSprite;
			for (int i = 0; i < Grounds.Count; i++)
				Grounds[i].sprite = Ground;
			Night = false;
			Evening = true;
			spawnClient.TimeMultiply = 0.8f;
		} else if ((int)timespan.TotalHours % 24 >= 4 && (int)timespan.TotalHours % 24 < 10) {
			for (int i = 0; i < Skyes.Count; i++)
				Skyes[i].sprite = MorningSprite;
			for (int i = 0; i < Grounds.Count; i++)
				Grounds[i].sprite = MorningGround;
			Night = false;
			Morning = true;
			spawnClient.TimeMultiply = 0.8f;
		} else if ((int)timespan.TotalHours % 24 >= 10 && (int)timespan.TotalHours % 24 < 17) {
			for (int i = 0; i < Skyes.Count; i++)
				Skyes[i].sprite = DaySprite;
			for (int i = 0; i < Grounds.Count; i++)
				Grounds[i].sprite = Ground;
			Night = false;
			Day = true;
			spawnClient.TimeMultiply = 1f;
		} else if ((int)timespan.TotalHours % 24 >= 22 || (int)timespan.TotalHours % 24 < 4) {
			for (int i = 0; i < Skyes.Count; i++)
				Skyes[i].sprite = NightSprite;
			for (int i = 0; i < Grounds.Count; i++)
				Grounds[i].sprite = NightGround;
			spawnClient.TimeMultiply = 0.1f;
		}
	}
}