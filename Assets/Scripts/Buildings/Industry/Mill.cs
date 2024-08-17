using System.Collections.Generic;
using static GameplayControllerInitializer;

public class Mill : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 60;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new List<string> { "Magazine" };
		getItemBuildingsNames = new string[] { "Magazine" };
		needToBuild = new Dictionary<string, int>
		{
			["Wood"] = 15
		};
		products = new Dictionary<string, int>
		{
			["Flour"] = 2
		};
		needToProduction = new Dictionary<string, int>
		{
			["Wheat"] = 1
		};
	}
}
