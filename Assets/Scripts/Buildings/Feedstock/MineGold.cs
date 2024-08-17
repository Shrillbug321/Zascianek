using System.Collections.Generic;
using static GameplayControllerInitializer;

public class MineGold : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		productionTime = MONTH_DURATION * 2;
		stockBuildingsNames = new List<string> { "Magazine" };
		needToBuild = new Dictionary<string, int>
		{
			["Wood"] = 30
		};
		products = new Dictionary<string, int>
		{
			["Gold"] = 2
		};
		grounds = new string[] { "gold_dirt", "gold_stone" , "gold_stone_small" };
	}
}
