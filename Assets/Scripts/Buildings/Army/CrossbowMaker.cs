using System.Collections.Generic;
using static GameplayControllerInitializer;

public class CrossbowMaker : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 60;
		productionTime = MONTH_DURATION * 2;
		stockBuildingsNames = new List<string> { "Armory" };
		getItemBuildingsNames = new string[]{ "Magazine" };
		needToBuild = new Dictionary<string, int>
		{
			["Wood"] = 10,
			["Iron"] = 2
		};
		products = new Dictionary<string, int>
		{
			["Crossbow"] = 1
		};
		needToProduction = new Dictionary<string, int>
		{
			["Wood"] = 2
		};
	}
}
