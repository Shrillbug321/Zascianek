public class Settler : Warrior
{
	public override void Start()
	{
		base.Start();
		hp = 150;
		speed = 3f;
		attackSpeed = 1000;
		damageMin = 35;
		damageMax = 35;
		armor = 10;
		weaponType = WeaponType.Cold;
		SetCircleCollider(5);
		gameObject.tag = "PlayerWarrior";
	}

	public override void Update()
	{
		base.Update();
	}

}