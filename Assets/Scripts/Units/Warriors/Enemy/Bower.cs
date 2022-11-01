public class Bower : Enemy
{
	public override void Start()
	{
		base.Start();
		hp = 85;
		speed = 5;
		attackSpeed = 1000;
		damageMin = 12;
		damageMax = 14;
		armor = 7;
		weaponType = WeaponType.Distance;
		SetCircleCollider(9);
		gameObject.tag = "Enemy";
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
}
