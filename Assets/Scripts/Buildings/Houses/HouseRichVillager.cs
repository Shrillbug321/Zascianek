public class HouseRichVillager : AbstractHouse
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 60;
		buildingName = "HouseRichVillager";
		needToBuild = new()
		{
			["Wood"] = 15,
			//["Clay"] = 5
		};
		maxInhabitans = 4;
		timeToNextInhabitant = 1;
		inhabitantType = "RichVillager";
	}
}
