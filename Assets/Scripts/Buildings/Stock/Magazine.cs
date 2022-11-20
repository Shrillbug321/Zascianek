using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Magazine : StockBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
	stockedItems = new()
	{
		["Wood"] = 0,
		["Wheat"] = 0,
		["Flour"] = 0,
		["Hop"] = 0,
		["Leather"] = 0,
		["Iron"] = 0,
		["Gold"] = 0,
		["Clay"] = 0,
	};
}
}
