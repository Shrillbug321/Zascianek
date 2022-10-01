public class Infrantry : Warrior
{
	public override void Start()
	{
		base.Start();
		HP = 100;
		Speed = 4f;
		AttackSpeed = 2000;
		DamageMin = 18;
		DamageMax = 20;
		Armor = 8;
		WeaponType = WeaponType.Cold;
		SetCircleCollider(5);
		gameObject.tag = "PlayerWarrior";
	}

	public override void Update()
	{
		base.Update();
	}

}