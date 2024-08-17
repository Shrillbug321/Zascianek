using static GameplayControllerInitializer;

public class HouseSettler : AbstractHouse
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 210;
		buildingName = "HouseSettler";
		inhabitantType = "Settler";
		color = "Green";
		//gameObject.layer = LayerMask.NameToLayer("Buildings");
		//hud.HideGameOverScreen();
	}

	private void OnDestroy()
	{
		gameplay.GameOver();
	}
}
