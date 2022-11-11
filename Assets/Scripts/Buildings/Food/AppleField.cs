﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AppleField : FoodBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 10;
		productionTime = MONTH * 3;
		needToBuild["Wood"] = 2;
		stockBuildingName = "Granary";
		product = new()
		{
			["Apple"] = 2
		};
	}

	public override string CanBuild()
	{
		return base.CanBuild();
	}
}
