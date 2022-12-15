using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class AppleField : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 20;
		buildingName = "AppleField";
		productionTime = MONTH_DURATION * 3;
		stockBuildingsNames = new() { "Granary" };
		products = new()
		{
			["Apple"] = 10
		};
	}
}
