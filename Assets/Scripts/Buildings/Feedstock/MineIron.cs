using System.Collections.Generic;
using static GameplayControllerInitializer;

public class MineIron : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new List<string> { "Magazine" };
		needToBuild = new Dictionary<string, int>
		{
			["Wood"] = 20
		};
		products = new Dictionary<string, int>
		{
			["Iron"] = 5
		};
		grounds = new string[] { "iron_dirt", "iron_grass" };
	}
}
