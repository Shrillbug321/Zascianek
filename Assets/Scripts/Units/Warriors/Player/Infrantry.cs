public class Infrantry : Warrior
{
	public override void Start()
	{
		base.Start();
		hp = 100;
		speed = 4f;
		attackSpeed = 2000;
		damageMin = 18;
		damageMax = 20;
		armor = 8;
		weaponType = WeaponType.Cold;
		SetCircleCollider(5);
		gameObject.tag = "PlayerWarrior";
	}

	public override void Update()
	{
		base.Update();
	}

}