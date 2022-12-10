public class Axer : Enemy
{
	public override void Start()
	{
		base.Start();
		hp = 110;
		speed = 3.5f;
		attackSpeed = 2500;
		damageMin = 25;
		damageMax = 27;
		armor = 12;
		weaponType = WeaponType.Cold;
		SetCircleCollider(15);
		gameObject.tag = "Enemy";
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
}
