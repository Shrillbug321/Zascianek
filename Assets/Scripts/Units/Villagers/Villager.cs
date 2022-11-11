using Assets.Scripts;

public class Villager: AbstractVillager
{
	public override void Start()
	{
		base.Start();
		hp = 100;
		speed = 4f;
		armor = 8;
		color = "Green";
		gameObject.tag = "Villager";
	}
	public override void Update()
	{
		base.Update();
	}
}
