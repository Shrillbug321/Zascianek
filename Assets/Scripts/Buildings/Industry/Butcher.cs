using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Butcher : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new() { "Granary" };
		getItemBuildingsNames = new string[]{"Magazine", "Granary"};
		products = new()
		{
			["Sausage"] = 2
		};
		needToProduction = new()
		{
			["Meat"] = 2,
			["Leather"] = 1
		};
	}
}
