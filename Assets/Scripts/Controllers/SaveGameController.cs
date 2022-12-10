using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

	public class SaveGameController:MonoBehaviour
	{
	protected SaveLoadUtility slu;

	public void Start()
	{
		slu = gameObject.AddComponent<SaveLoadUtility>();
	}

	public async void Save()
	{
		slu.SaveGame("n");
	}

	public async void Load()
	{
		slu.LoadGame("n");
		gameplay.Start();
		//hud.Start();
		//Instantiate(hud);
		//Instantiate(gameplay.mainCamera);
	}
}