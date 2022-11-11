public class Crossbower : Warrior
{
	public override void Start()
	{
		base.Start();
		hp = 90;
		speed = 5f;
		attackSpeed = 2000;
		damageMin = 12;
		damageMax = 14;
		armor = 7;
		weaponType = WeaponType.Distance;
		//SetCircleCollider(9);
		gameObject.tag = "Warrior";
	}

	public override void Update()
	{
		base.Update();
	}

}