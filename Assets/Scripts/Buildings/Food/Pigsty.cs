using System.Collections.Generic;
using static GameplayControllerInitializer;

public class Pigsty : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 20;
		productionTime = MONTH_DURATION * 9;
		stockBuildingsNames = new List<string> { "Granary", "Magazine" };
		getItemBuildingsNames = new string[]{"Granary"};
		products = new Dictionary<string, int>
		{
			["Meat"] = 15,
			["Leather"] = 2
		};
		needToProduction = new Dictionary<string, int>
		{
			["Apple"] = 5
		};
		initStatus = BuildingStatus.workerGoForItem;
	}
}
