﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Armory : StockBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		stockedItems = new()
		{
			["Sable"] = 0,
			["Crossbow"] = 0,
			["Armor"] = 0
		};
	}
}
