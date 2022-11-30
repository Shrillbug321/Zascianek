using UnityEngine;
using static GameplayControllerInitializer;

public class HouseSettler : AbstractHouse
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 70;
		buildingName = "HouseSettler";
		inhabitantType = "Settler";
		gameObject.layer = LayerMask.NameToLayer("Buildings");
	}
}
