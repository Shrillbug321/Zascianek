using System.Collections.Generic;
using static GameplayControllerInitializer;

public class Market : Building
{
	private static Dictionary<string, int> prices = new()
	{
		["Meat"] = 8,
		["Leather"] = 5,
		["Apple"] = 3,
		["Bread"] = 5,
		["Beer"] = 10,
		["Sausage"] = 12,
		["Iron"] = 20,
		["Clay"] = 20,
		["Wood"] = 10,
		["Gold"] = 40,
		["Wheat"] = 5,
		["Flour"] = 7,
		["Hop"] = 5,
		["Sable"] = 12,
		["Armor"] = 12,
		["Crossbow"] = 12
	};
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		buildingName = "Market";
		needToBuild = new Dictionary<string, int>
		{
			["Wood"] = 5,
			["Clay"] = 5,
			["Money"] = 10,
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
		return gameplay.items["Money"] >= prices[item];
	}

	public static bool CanSell(string item)
	{
		return gameplay.items[item] > 0;
	}
}
