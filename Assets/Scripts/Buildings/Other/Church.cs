using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Church : Building
{
	public static int priests;
	public override void Start()
	{
		base.Start();
		dp = maxDp = 70;
		buildingName = "Church";
		needToBuild = new()
		{
			["Wood"] = 30,
			["Money"] = 100,
		};
	}
	public void GetTax()
	{
		gameplay.items["Wheat"] -= (int)Math.Round(gameplay.items["Wheat"] * 0.1,0);
		gameplay.items["Money"] -= 10;
	}
}
