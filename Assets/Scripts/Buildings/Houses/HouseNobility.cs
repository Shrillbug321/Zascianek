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
			//["Clay"] = 30
			//["Money"] = 10
		};
		maxInhabitans = 2;
		timeToNextInhabitant = 1;
		inhabitantType = "Nobility";
	}
}
