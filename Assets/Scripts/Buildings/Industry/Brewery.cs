using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Brewery : ProductionBuilding
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
