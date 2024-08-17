using System.Collections.Generic;
using static GameplayControllerInitializer;

public class WheatField : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 20;
		productionTime = MONTH_DURATION * 12;
		stockBuildingsNames = new List<string> { "Magazine" };
		products = new Dictionary<string, int>
		{
			["Wheat"] = 60
		};
	}
}
