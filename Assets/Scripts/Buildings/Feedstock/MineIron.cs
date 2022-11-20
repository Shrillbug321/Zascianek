using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MineIron : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		productionTime = MONTH * 1;
		stockBuildingsNames = new() { "Magazine" };
		needToBuild = new()
		{
			["Wood"] = 20
		};
		products = new()
		{
			["Iron"] = 10
		};
		grounds = new string[] { "iron_dirt", "iron_grass" };
	}
}
