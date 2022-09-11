public class HeavyInfrantry : Warrior
{
	public override void Start()
	{
		base.Start();
		HP = 120;
		Speed = 3f;
		AttackSpeed = 2500;
		DamageMin = 25;
		DamageMax = 28;
		Armor = 15;
		WeaponType = WeaponType.Cold;
		SetCircleCollider(5);
		gameObject.tag = "PlayerWarrior";
	}

	public override void Update()
	{
		base.Update();
	}

}