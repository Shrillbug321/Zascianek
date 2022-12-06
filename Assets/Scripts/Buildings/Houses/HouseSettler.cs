using UnityEngine;
using static GameplayControllerInitializer;

public class HouseSettler : AbstractHouse
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 1400;
		buildingName = "HouseSettler";
		inhabitantType = "Settler";
		color = "Green";
		gameObject.layer = LayerMask.NameToLayer("Buildings");
	}

	private void OnDestroy()
	{
		gameplay.GameOver();
	}
}
