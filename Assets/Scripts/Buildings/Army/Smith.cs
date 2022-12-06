using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Smith : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 20;
		productionTime = MONTH_DURATION * 2;
		stockBuildingsNames = new() { "Armory" };
		getItemBuildingsNames = new string[]{ "Magazine" };
		products = new()
		{
			["Sable"] = 1
		};
		needToProduction = new()
		{
			["Iron"] = 1
		};
	}
}
