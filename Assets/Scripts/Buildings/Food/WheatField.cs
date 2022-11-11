using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WheatField : FoodBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 20;
		productionTime = MONTH * 12;
		stockBuildingName = "Magazine";
		product = new()
		{
			["Wheat"] = 60
		};
	}
	public override string CanBuild()
	{
		return base.CanBuild();
	}
}
