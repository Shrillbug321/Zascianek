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
		dp = maxDp = 20;
		productionTime = MONTH * 1;
		stockBuildingsNames = new() { "Granary" };
		getItemBuildingsNames = new string[]{"Magazine", "Granary"};
		products = new()
		{
			["Sausage"] = 2
		};
		needToProduction = new()
		{
			["Meat"] = 15,
			["Leather"] = 2
		};
	}
}
