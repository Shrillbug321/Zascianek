using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Market : Building
{
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
		buildingName = "Market";
		needToBuild = new()
		{
			["Wood"] = 2
		};
	}

	public static void Buy(string item)
	{
		if (CanBuy(item))
		{
			gameplay.items[item]++;
			gameplay.items["Money"] -= prices[item];
		}
	}

	public static void Sell(string item)
	{
		if (CanSell(item))
		{
			gameplay.items[item]--;
			gameplay.items["Money"] += prices[item];
		}
	}

	public static bool CanBuy(string item)
	{
		return GameplayControllerInitializer.gameplay.items["Money"] >= prices[item];
	}

	public static bool CanSell(string item)
	{
		return GameplayControllerInitializer.gameplay.items[item] > 0;
	}
}
