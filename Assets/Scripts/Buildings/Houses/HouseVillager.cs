public class HouseVillager : AbstractHouse
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		buildingName = "HouseVillager";
		needToBuild = new()
		{
			["Wood"] = 12
		};
		maxInhabitans = 8;
		timeToNextInhabitant = 1;
		inhabitantType = "Villager";
	}
}
