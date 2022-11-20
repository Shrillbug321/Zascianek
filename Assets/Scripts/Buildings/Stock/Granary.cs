using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Granary : StockBuilding
{
	/*public Dictionary<string, int> stocked = new()
	{
		["apple"] = 0,
		["beer"] = 0,
		["meat"] = 0,
		["bread"] = 0,
		["wurst"] = 0
	};*/
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		stockedItems = new()
		{
			["Apple"] = 0,
			["Meat"] = 0,
			["Bread"] = 0,
			["Beer"] = 0,
			["Sausage"] = 0,
		};
	}
}
