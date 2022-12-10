using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;
using static GameplayControllerInitializer;

public class MusicController : MonoBehaviour
{
	public AudioSource musicPlayer;
	private string type = "Ambient";
	private Random random = new();
	private Dictionary<string, List<string>> music = new()
	{
		["Ambient"] = new()
		{
			"Nadchodzi Husaria",
			"Chudy i Gruby",
			"Hobbit karmi świnie",
			"Chór",
			"Niski gwizdek",
			"Pani jeziora",
			"Dziewica"
		},
		["Battle"] = new()
		{
			"Nadchodzi zwycięstwo",
			"Przeciwnik nadchodzi",
			"Walka smoków"
		},
		["Other"] = new()
		{
			"GameOver"
		}
	};
	private string lastState = "Ambient";

	public void Start()
	{
		musicPlayer = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
		if (PlayerPrefs.GetString("music") == "false")
			musicPlayer.mute = true;
		else
			ChangeClipForType();
	}

	public void Update()
	{
		if (!musicPlayer.isPlaying)
			ChangeClipForType();
	}

	public void ChangeClip(string type, string name)
	{
		musicPlayer.Stop();
		musicPlayer.clip = Resources.Load<AudioClip>("Music/" + type + '/' + music[type].First(m => m == name));
		musicPlayer.Play();
	}

	public void ChangeClipForType()
	{
		musicPlayer.Stop();
		musicPlayer.clip = Resources.Load<AudioClip>("Music/" + type + '/' + music[type][random.Next(music[type].Count - 1)]);
		musicPlayer.Play();
	}

	public void ChangeType(string type)
	{
		this.type = type;
		ChangeClipForType();
	}

	public void ChangeState(string state)
	{
		if (state == lastState) return;
		lastState = state;
		switch (state)
		{
			case "Ambient":
				ChangeType("Ambient");
				break;
			case "Battle":
				ChangeType("Battle");
				break;
			case "GameOver":
				ChangeClip("Other", "GameOver");
				break;
		}
	}
}
