using System.Collections.Generic;

public class Granary : StockBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 60;
		needToBuild = new Dictionary<string, int>
		{
			["Wood"] = 10
		};
		stockedItems = new Dictionary<string, int>
		{
			["Apple"] = 0,
			["Meat"] = 0,
			["Bread"] = 0,
			["Beer"] = 0,
			["Sausage"] = 0,
		};
	}
}
