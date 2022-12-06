using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Pigsty : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 20;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new() { "Granary", "Magazine" };
		getItemBuildingsNames = new string[]{"Granary"};
		products = new()
		{
			["Meat"] = 15,
			["Leather"] = 2
		};
		needToProduction = new()
		{
			["Apple"] = 5
		};
	}
}
