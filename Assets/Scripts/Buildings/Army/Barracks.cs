using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Barracks : Building
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 10;
		buildingName = "Barracks";
		productionTime = MONTH * 3;
		needToBuild = new()
		{
			["Wood"] = 2
		};
	}
}
