using System.Collections.Generic;
using static GameplayControllerInitializer;

public class AppleField : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 20;
		buildingName = "AppleField";
		productionTime = MONTH_DURATION * 3;
		stockBuildingsNames = new List<string> { "Granary" };
		products = new Dictionary<string, int>
		{
			["Apple"] = 10
		};
	}
}
