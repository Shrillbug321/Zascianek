public class Crossbower : Warrior
{
	public override void Start()
	{
		base.Start();
		HP = 90;
		Speed = 5f;
		AttackSpeed = 2000;
		DamageMin = 12;
		DamageMax = 14;
		Armor = 7;
		WeaponType = WeaponType.Distance;
		SetCircleCollider(9);
		gameObject.tag = "PlayerWarrior";
	}

	public override void Update()
	{
		base.Update();
	}

}