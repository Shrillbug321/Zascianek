public class HeavyInfrantry : Warrior
{
	public override void Start()
	{
		base.Start();
		hp = 120;
		speed = 3f;
		attackSpeed = 2500;
		damageMin = 25;
		damageMax = 28;
		armor = 15;
		weaponType = WeaponType.Cold;
		SetCircleCollider(5);
		gameObject.tag = "PlayerWarrior";
	}

	public override void Update()
	{
		base.Update();
	}

}