using System.Collections.Generic;
using static GameplayControllerInitializer;

public class Brewery : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new List<string> { "Granary" };
		getItemBuildingsNames = new string[] { "Magazine" };
		needToBuild = new Dictionary<string, int>
		{
			["Wood"] = 15
		};
		products = new Dictionary<string, int>
		{
			["Beer"] = 1
		};
		needToProduction = new Dictionary<string, int>
		{
			["Hop"] = 1
		};
	}
}
