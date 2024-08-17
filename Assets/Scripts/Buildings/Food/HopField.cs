using System.Collections.Generic;
using static GameplayControllerInitializer;

public class HopField : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 20;
		productionTime = MONTH_DURATION * 6;
		stockBuildingsNames = new List<string> { "Magazine" };
		products = new Dictionary<string, int>
		{
			["Hop"] = 25
		};
	}
}
