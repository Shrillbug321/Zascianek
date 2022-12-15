using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class CrossbowMaker : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 60;
		productionTime = MONTH_DURATION * 2;
		stockBuildingsNames = new() { "Armory" };
		getItemBuildingsNames = new string[]{ "Magazine" };
		needToBuild = new()
		{
			["Wood"] = 10,
			["Iron"] = 2
		};
		products = new()
		{
			["Crossbow"] = 1
		};
		needToProduction = new()
		{
			["Wood"] = 2
		};
	}
}
