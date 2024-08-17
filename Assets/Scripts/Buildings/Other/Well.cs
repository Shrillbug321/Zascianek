using System.Collections.Generic;

public class Well : Building
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		buildingName = "Well";
		needToBuild = new Dictionary<string, int>
		{
			["Clay"] = 5
		};
	}
}
