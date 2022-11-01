using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AppleField : Building
{
	public override void Start()
	{
		base.Start();
		productionTime = MONTH * 3;
		needToBuilding = new Dictionary<string, int>()
		{
			["iron"] = 0,
			["clay"] = 0,
			["wood"] = 2,
			["gold"] = 0,
			["money"] = 0
		};
	}

	public void Update()
	{
		time += Time.deltaTime;
		print(time);
		if (time > productionTime)
		{
			time = 0;
			GameplayControllerInitializer.gameplay.items["apple"]++;
			GameplayControllerInitializer.gameplay.items["wood"]++;
			print("aaa");
		}
	}

	/*public override async Task Production()
	{
		//while (true)
		{
			//await base.Production();
			Task.Delay(productionTime);
			GameplayControllerInitializer.gameplay.items["apple"]++;
			print(GameplayControllerInitializer.gameplay.items);
		}
	}*/
}
