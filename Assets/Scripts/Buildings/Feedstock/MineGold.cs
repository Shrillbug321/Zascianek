using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class MineGold : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		productionTime = MONTH * 2;
		stockBuildingsNames = new() { "Magazine" };
		needToBuild = new()
		{
			["Wood"] = 30
		};
		products = new()
		{
			["Gold"] = 2
		};
		grounds = new string[] { "gold_dirt", "gold_stone" , "gold_stone_small" };
	}
}
