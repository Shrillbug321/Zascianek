using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class MineClay : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		productionTime = MONTH * 1;
		stockBuildingsNames = new() { "Magazine" };
		needToBuild = new()
		{
			["Wood"] = 10
		};
		products = new()
		{
			["Clay"] = 5
		};
		grounds = new string[] { "clay_dirt", "clay_grass" };
	}
}
