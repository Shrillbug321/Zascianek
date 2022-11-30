using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Church : Building
{
	public static int priests;
	private static Dictionary<string, int> prices = new()
	{
		["Meat"] = 10,
		["Leather"] = 10,
		["Apple"] = 5,
		["Bread"] = 10,
		["Beer"] = 10,
		["Sausage"] = 10,
		["Iron"] = 10,
		["Clay"] = 10,
		["Wood"] = 40,
		["Gold"] = 10,
		["Money"] = 10,
		["Wheat"] = 10,
		["Flour"] = 10,
		["Hop"] = 10,
		["Sable"] = 10,
		["Armor"] = 10,
		["Crossbow"] = 10
	};
	public override void Start()
	{
		base.Start();
		dp = maxDp = 10;
		buildingName = "Church";
		needToBuild = new()
		{
			["Wood"] = 2
		};
	}
}
