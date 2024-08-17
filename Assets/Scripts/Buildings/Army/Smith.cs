using System.Collections.Generic;
using static GameplayControllerInitializer;

public class Smith : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 20;
		productionTime = MONTH_DURATION * 2;
		stockBuildingsNames = new List<string> { "Armory" };
		getItemBuildingsNames = new string[]{ "Magazine" };
		needToBuild = new Dictionary<string, int>
		{
			["Wood"] = 10,
			["Iron"] = 5
		};
		products = new Dictionary<string, int>
		{
			["Sable"] = 1
		};
		needToProduction = new Dictionary<string, int>
		{
			["Iron"] = 2
		};
	}
}
