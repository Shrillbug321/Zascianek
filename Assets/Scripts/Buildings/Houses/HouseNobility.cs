public class HouseNobility : AbstractHouse
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 70;
		buildingName = "HouseNobility";
		needToBuild = new()
		{
			["Wood"] = 10,
			["Clay"] = 30,
			["Money"] = 10
		};
		maxInhabitans = 2;
		timeToAddInhabitant = 1;
		timeToRemoveInhabitant = 10;
		inhabitantType = "Nobility";
	}
}
