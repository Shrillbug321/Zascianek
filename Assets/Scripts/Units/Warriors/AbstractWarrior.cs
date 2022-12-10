using Assets.Scripts;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;
using Random = System.Random;

public class AbstractWarrior : UnitModel
{
	public bool seenEnemy = false;
	public int attackSpeed { get; set; }
	public int damageMin { get; set; }
	public int damageMax { get; set; }
	protected AbstractWarrior enemy;
	protected Building building;
	Vector2 enemyPos = Vector2.zero;
	public string[] playerTags;
	public string[] enemyTags;
	public WeaponType weaponType { get; set; }

	Random random = new();
	public override void Start()
	{
		base.Start();
		playerTags = gameplay.playerTags;
		enemyTags = gameplay.enemyTags;
	}

	public override void Update()
	{
		base.Update();
	}

	protected async Task Attack(CancellationToken token)
	{
		if ((direction.x < 0 && enemy.sr.flipX) || (direction.x > 0 && !enemy.sr.flipX))
		{
			enemy.sr.flipX = !enemy.sr.flipX;
		}

		do
		{
			enemy.DecreaseHP(Damage() - enemy.armor);
			enemy.Blinking(attackSpeed, token);
			await Task.Delay(attackSpeed);
		} while (enemy.hp > 0 && !token.IsCancellationRequested);


		if (token.IsCancellationRequested)
			return;

		//await enemy.gameObject.Rotate();
		Destroy(enemy.gameObject);
	}

	protected async Task AttackBuilding(CancellationToken token)
	{
		do
		{
			building.DecreaseDP(Damage());
			building.Blinking(attackSpeed, token);
			print(building.dp);
			await Task.Delay(attackSpeed);
		} while (building.dp > 0 && !token.IsCancellationRequested);


		if (token.IsCancellationRequested)
			return;

		Destroy(building.gameObject);
	}

	public int Damage()
	{
		return random.Next(damageMin, damageMax + 1);
	}

	public override async void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D(collision);
		string tag = collision.tag;
		if (tag == this.tag) return;
		//Debug.LogWarning("lllll");
		if ((playerTags.Contains(tag) || enemyTags.Contains(tag)))// && !seenEnemy)
		{
			enemy = collision.GetComponent<AbstractWarrior>();
			if (collision.GetType() == typeof(CircleCollider2D))
			{
				if (seenEnemy) return;
				if (weaponType == WeaponType.Cold && !seenEnemy)
				{
					enemyPos = collision.gameObject.transform.position;
					float offx = direction.x > 0.0f ? -0.35f : 0.35f;

					temp.Push(movement);
					movement.x = enemyPos.x + offx;
					movement.y = enemyPos.y;


					/*Vector2 difference = (Vector2)transform.position - enemyPos;

					movement.x = (difference.x / 2 + offx) * direction.x;
					movement.y = difference.y / 2 * direction.y;*/
					//moveStart = true;

				}
				if (weaponType == WeaponType.Distance)
				{
					moveStart = false;
					temp.Push(movement);
					await Attack(unitToken);
					moveStart = true;
				}
				if (enemy.weaponType == WeaponType.Distance)
				{
					//movement = enemy.gameObject.transform.position;
					temp.Push(movement);
					moveStart = true;
				}
				//movement = enemy.gameObject.transform.position;
				//moveStart = true;
				seenEnemy = true;
			}
			if (collision.GetType() == typeof(BoxCollider2D))
			{
				if (weaponType == WeaponType.Cold)
				{
					moveStart = false;
					await Attack(unitToken);
					moveStart = true;
				}

			}
		}

		if (tag == "Building")
		{
			building = collision.GetComponent<Building>();
			//string color = building.GetComponent<Building>().color;
			if (collision.GetType() == typeof(CircleCollider2D) &&
			color != building.color)
			{
				moveStart = false;
				await AttackBuilding(buildingToken);
				moveStart = true;
			}
		}
	}

	public override void OnTriggerExit2D(Collider2D collision)
	{
		base.OnTriggerExit2D(collision);
		string tag = collision.tag;
		if (playerTags.Contains(tag) || enemyTags.Contains(tag))
		{
			if (collision.GetType() == typeof(CircleCollider2D))
			{
				if (seenEnemy)
				{
					seenEnemy = false;
					movement = temp.Pop();
				}
				if (weaponType == WeaponType.Distance)
				{
					CreateToken();
				}
			}

			if (collision.GetType() == typeof(BoxCollider2D))
			{
				CreateToken();
			}
		}

		if(tag=="Building")
		{
			CreateBuildingToken();
		}
	}

	private void CreateToken()
	{
		unitTokenSource.Cancel();
		unitTokenSource.Dispose();
		unitTokenSource = new CancellationTokenSource();
		unitToken = unitTokenSource.Token;
	}

	private void CreateBuildingToken()
	{
		buildingTokenSource.Cancel();
		buildingTokenSource.Dispose();
		buildingTokenSource = new CancellationTokenSource();
		buildingToken = buildingTokenSource.Token;
	}
}

public enum WeaponType
{
	Cold, Distance
}