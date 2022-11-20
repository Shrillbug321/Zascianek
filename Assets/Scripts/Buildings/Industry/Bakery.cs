using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bakery : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 20;
		productionTime = MONTH * 1;
		stockBuildingsNames = new() { "Granary" };
		getItemBuildingsNames = new string[] { "Magazine" };
		needToBuild = new()
		{
			["Wood"] = 10
		};
		products = new()
		{
			["Bread"] = 5
		};
		needToProduction = new()
		{
			["Flour"] = 1
		};
	}
}
