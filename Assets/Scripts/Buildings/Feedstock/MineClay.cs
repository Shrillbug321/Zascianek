using System.Collections.Generic;
using static GameplayControllerInitializer;

public class MineClay : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new List<string> { "Magazine" };
		needToBuild = new Dictionary<string, int>
		{
			["Wood"] = 10
		};
		products = new Dictionary<string, int>
		{
			["Clay"] = 5
		};
		grounds = new string[] { "clay_dirt", "clay_grass" };
	}
}
