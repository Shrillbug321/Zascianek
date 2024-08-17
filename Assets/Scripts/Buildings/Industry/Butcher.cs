using System.Collections.Generic;
using static GameplayControllerInitializer;

public class Butcher : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new List<string> { "Granary" };
		getItemBuildingsNames = new string[]{"Magazine", "Granary"};
		products = new Dictionary<string, int>
		{
			["Sausage"] = 2
		};
		needToProduction = new Dictionary<string, int>
		{
			["Meat"] = 2,
			["Leather"] = 1
		};
	}
}
