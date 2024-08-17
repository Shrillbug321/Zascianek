using UnityEngine;
using static GameplayControllerInitializer;

public class Settler : Warrior
{
	private float xMin, xMax, yMin, yMax;
	private float offsetX = 12, offsetY = 8;
	public override void Start()
	{
		base.Start();
		hp = 50;
		speed = 3f;
		attackSpeed = 1000;
		damageMin = 35;
		damageMax = 35;
		armor = 10;
		weaponType = WeaponType.Cold;
		//SetCircleCollider(5);
		gameObject.tag = "Warrior";
		Vector2 pos = GameObject.Find("HouseSettler").transform.position;
		xMin = pos.x - offsetX;
		xMax = pos.x + offsetX;
		yMin = pos.y - offsetY;
		yMax = pos.y + offsetY;
		moveStart = true;
		SetMovement();
	}

	public override void Update()
	{
		base.Update();
		if (!seenEnemy && Vector2.Distance(transform.position, movement) <= 0.5)
			SetMovement();
	}

	private void SetMovement()
	{
		float x, y;
		do
		{
			x = Random.Range(xMin, xMax);
			y = Random.Range(yMin, yMax);
		} while (System.Math.Sqrt(x * x + y * y) <= 3);
		movement.x = x; movement.y = y;
	}

	private void OnDestroy()
	{
		gameplay.GameOver();
	}
}