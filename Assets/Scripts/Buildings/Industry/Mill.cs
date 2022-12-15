using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Mill : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 60;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new() { "Magazine" };
		getItemBuildingsNames = new string[] { "Magazine" };
		needToBuild = new()
		{
			["Wood"] = 15
		};
		products = new()
		{
			["Flour"] = 2
		};
		needToProduction = new()
		{
			["Wheat"] = 1
		};
	}
}
