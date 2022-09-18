using Assets.Scripts;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

public class AbstractWarrior : UnitModel
{
	public bool SeenEnemy = false;
	public int AttackSpeed { get; set; }
	public int DamageMin { get; set; }
	public int DamageMax { get; set; }
	protected AbstractWarrior enemy;
	Vector2 enemyPos = Vector2.zero;
	public string[] PlayerTags;
	public string[] EnemyTags;
	public WeaponType WeaponType { get; set; }
	public const string Path = "Assets/Sprites/Units/Units";

	Random random = new();
	public override void Start()
	{
		base.Start();
		PlayerTags = GameplayController.Instance.PlayerTags;
		EnemyTags = GameplayController.Instance.EnemyTags;
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
			enemy.DecreaseHP(Damage() - enemy.Armor);
			enemy.Blinking(AttackSpeed, token);
			await Task.Delay(AttackSpeed);
		} while (enemy.HP > 0 && !token.IsCancellationRequested);


		if (token.IsCancellationRequested)
			return;

		await enemy.Rotate();
		Destroy(enemy.gameObject);
	}

	public int Damage()
	{
		return random.Next(DamageMin, DamageMax + 1);
	}

	public override void OnTriggerEnter2D(Collider2D collision)
	{
	base.OnTriggerEnter2D(collision);
		string tag = collision.tag;
		if (tag == this.tag) return;
		if (PlayerTags.Contains(tag) || EnemyTags.Contains(tag))
		{
			enemy = collision.GetComponent<AbstractWarrior>();
			if (collision.GetType() == typeof(CircleCollider2D))
			{
				if (WeaponType == WeaponType.Cold && !SeenEnemy)
				{
					enemyPos = collision.gameObject.transform.position;
					float offx = direction.x > 0.0f ? -0.35f : 0.35f;

					movement.x = enemyPos.x + offx;
					movement.y = enemyPos.y;
					//moveStart = true;

				}
				if (WeaponType == WeaponType.Distance)
				{
					moveStart = false;
					Attack(token);
				}
				if (enemy.WeaponType == WeaponType.Distance)
				{
					movement = enemy.gameObject.transform.position;
					moveStart = true;
				}
			}
			if (collision.GetType() == typeof(BoxCollider2D))
			{
				if (WeaponType == WeaponType.Cold)
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
		if (PlayerTags.Contains(tag) || EnemyTags.Contains(tag))
		{
			if (collision.GetType() == typeof(CircleCollider2D))
			{
				if (SeenEnemy)
					SeenEnemy = false;
				if (WeaponType == WeaponType.Distance)
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