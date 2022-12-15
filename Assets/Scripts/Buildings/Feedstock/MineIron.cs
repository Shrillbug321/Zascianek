using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class MineIron : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new() { "Magazine" };
		needToBuild = new()
		{
			["Wood"] = 20
		};
		products = new()
		{
			["Iron"] = 5
		};
		grounds = new string[] { "iron_dirt", "iron_grass" };
	}
}
