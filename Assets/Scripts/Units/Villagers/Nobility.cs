using Assets.Scripts;
using UnityEngine;
using static GameplayControllerInitializer;

public class Nobility : AbstractVillager
{
	public override void Start()
	{
		base.Start();
		hp = 80;
		speed = 3f;
		color = "Green";
		moveStart = true;
		SetMovement();
	}

	public override void Update()
	{
		base.Update();
		if (Vector2.Distance(transform.position, movement) <= 0.5)
		{
			SetMovement();
		}
	}

	private void SetMovement()
	{
		float x, y;
		do
		{
			x = Random.Range(0, gameplay.mapWidth) - 5;
			y = Random.Range(0, gameplay.mapHeight) - 5;
		} while (System.Math.Sqrt(x * x + y * y) <= 3);
		movement.x = x; movement.y = y;
	}
}
