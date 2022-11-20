using Assets.Scripts;

public class Villager: AbstractVillager
{
	public override void Start()
	{
		base.Start();
		hp = 50;
		speed = 4f;
		color = "Green";
	}
	public override void Update()
	{
		base.Update();
	}
}
