using System.Collections.Generic;
using static GameplayControllerInitializer;

public class Bakery : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 60;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new List<string> { "Granary" };
		getItemBuildingsNames = new string[] { "Magazine" };
		needToBuild = new Dictionary<string, int>
		{
			["Wood"] = 10
		};
		products = new Dictionary<string, int>
		{
			["Bread"] = 5
		};
		needToProduction = new Dictionary<string, int>
		{
			["Flour"] = 1
		};
	}
}
