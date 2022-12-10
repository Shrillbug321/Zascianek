using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Well : Building
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		buildingName = "Well";
		needToBuild = new()
		{
			["Clay"] = 5
		};
	}
}
