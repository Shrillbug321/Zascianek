using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class WheatField : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 20;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new() { "Magazine" };
		products = new()
		{
			["Wheat"] = 60
		};
	}
}
