﻿public class HouseRichVillager : AbstractHouse
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 60;
		buildingName = "HouseRichVillager";
		needToBuild = new()
		{
			["Wood"] = 15,
			["Clay"] = 5
		};
		maxInhabitans = 4;
		timeToAddInhabitant = 1;
		timeToRemoveInhabitant = 10;
		inhabitantType = "RichVillager";
	}
}
