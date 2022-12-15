using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Brewery : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new() { "Granary" };
		getItemBuildingsNames = new string[] { "Magazine" };
		needToBuild = new()
		{
			["Wood"] = 15
		};
		products = new()
		{
			["Beer"] = 1
		};
		needToProduction = new()
		{
			["Hop"] = 1
		};
	}
}
