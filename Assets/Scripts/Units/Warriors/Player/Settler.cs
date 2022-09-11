public class Settler : Warrior
{
	public override void Start()
	{
		base.Start();
		HP = 150;
		Speed = 3f;
		AttackSpeed = 1000;
		DamageMin = 35;
		DamageMax = 35;
		Armor = 10;
		WeaponType = WeaponType.Cold;
		SetCircleCollider(5);
		gameObject.tag = "PlayerWarrior";
	}

	public override void Update()
	{
		base.Update();
	}

}