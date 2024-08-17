using System.Collections.Generic;

public class Magazine : StockBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		needToBuild = new Dictionary<string, int>
		{
			["Wood"] = 5
		};
		stockedItems = new Dictionary<string, int>
		{
			["Wood"] = 0,
			["Wheat"] = 0,
			["Flour"] = 0,
			["Hop"] = 0,
			["Leather"] = 0,
			["Iron"] = 0,
			["Gold"] = 0,
			["Clay"] = 0,
		};
	}
}