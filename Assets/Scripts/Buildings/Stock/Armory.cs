using System.Collections.Generic;

public class Armory : StockBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 70;
		needToBuild = new Dictionary<string, int>
		{
			["Clay"] = 20
		};
		stockedItems = new Dictionary<string, int>
		{
			["Sable"] = 0,
			["Crossbow"] = 0,
			["Armor"] = 0
		};
	}
}
