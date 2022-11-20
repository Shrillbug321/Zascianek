using Assets.Scripts;

public class Nobile: AbstractVillager
{
	public override void Start()
	{
		base.Start();
		hp = 80;
		speed = 3f;
		color = "Green";
	}
	public override void Update()
	{
		base.Update();
	}
}
