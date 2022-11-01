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

		await enemy.Rotate();
		Destroy(enemy.gameObject);
	}

	public int Damage()
	{
		return random.Next(damageMin, damageMax + 1);
	}

	public override void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D(collision);
		string tag = collision.tag;
		if (tag == this.tag) return;
		if (playerTags.Contains(tag) || enemyTags.Contains(tag))
		{
			enemy = collision.GetComponent<AbstractWarrior>();
			if (collision.GetType() == typeof(CircleCollider2D))
			{
				if (seenEnemy) return;
				if (weaponType == WeaponType.Cold && !seenEnemy)
				{
					enemyPos = collision.gameObject.transform.position;
					float offx = direction.x > 0.0f ? -0.35f : 0.35f;

					movement.x = enemyPos.x + offx;
					movement.y = enemyPos.y;
					//moveStart = true;

				}
				if (weaponType == WeaponType.Distance)
				{
					moveStart = false;
					Attack(token);
				}
				if (enemy.weaponType == WeaponType.Distance)
				{
					movement = enemy.gameObject.transform.position;
					moveStart = true;
				}
				seenEnemy = true;
			}
			if (collision.GetType() == typeof(BoxCollider2D))
			{
				if (weaponType == WeaponType.Cold)
				{
					Attack(token);
				}

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
					seenEnemy = false;
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
	}

	private void CreateToken()
	{
		tokenSource.Cancel();
		tokenSource.Dispose();
		tokenSource = new CancellationTokenSource();
		token = tokenSource.Token;
	}
}

public enum WeaponType
{
	Cold, Distance
}