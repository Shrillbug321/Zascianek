public class HouseVillager : AbstractHouse
{
	public override void Start()
	{
		base.Start();
		maxDp = 50;
		dp = 40;
		buildingName = "HouseVillager";
		needToBuild = new()
		{
			["Wood"] = 12
		};
		maxInhabitans = 8;
		timeToAddInhabitant = 1;
		timeToRemoveInhabitant = 10;
		inhabitantType = "Villager";
	}
}
