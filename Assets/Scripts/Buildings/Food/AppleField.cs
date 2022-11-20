using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AppleField : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 10;
		buildingName = "AppleField";
		productionTime = MONTH * 3;
		needToBuild = new()
		{
			["Wood"] = 2
		};
		stockBuildingsNames = new() { "Granary" };
		products = new()
		{
			["Apple"] = 2
		};
	}
}
