using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class AbstractWarrior : UnitModel
{
	public bool SeenEnemy = false;
	public int AttackSpeed { get; set; }
	public int DamageMin { get; set; }
	public int DamageMax { get; set; }
	protected UnitModel enemy;
	Vector2 enemyPos = Vector2.zero;
	public string[] PlayerTags;
	public string[] EnemyTags;
	public WeaponType WeaponType { get; set; }

	Random random = new Random();
	public override void Start()
	{
		base.Start();
		PlayerTags = GameplayController.Instance.PlayerTags;
		EnemyTags = GameplayController.Instance.EnemyTags;
	}

	public override void Update()
	{
		base.Update();
		if (SeenEnemy && stopped)
		{
			oldPos = transform.position;
			movement = enemyPos;
			stopped = false;
		}
	}

	protected async Task Attack(CancellationToken token)
	{
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

	public virtual void OnTriggerEnter2D(Collider2D collision)
	{
		string tag = collision.tag;
		if (tag == this.tag) return;
		if (PlayerTags.Contains(tag) || EnemyTags.Contains(tag))
		{
			if (collision.GetType() == typeof(CircleCollider2D))
			{
				print(WeaponType);
				if (WeaponType == WeaponType.Cold && !SeenEnemy)
					{
						enemyPos = collision.gameObject.transform.position;
						float offx = direction.x > 0.0f ? -0.35f : 0.35f;

						movement.x = enemyPos.x + offx;
						movement.y = enemyPos.y;
						//movement = enemyPos;
						/*SeenEnemy = true;
						stopped = true;*/
					}
				if (WeaponType == WeaponType.Distance)
				{
					enemy = collision.GetComponent<UnitModel>();
					print(";");
					Attack(token);
				}
			}
			if (collision.GetType() == typeof(BoxCollider2D))
			{
				if (WeaponType == WeaponType.Cold)
				{
					enemy = collision.GetComponent<UnitModel>();
					if (enemy.sr.flipX)
					enemy.sr.flipX = false;
					float offx = direction.x > 0.0f ? -0.85f : 0.85f;
					Vector2 pos = collision.gameObject.transform.position;
					/*movement.x = pos.x + offx;
					movement.y = pos.y;*/
					//movement = pos;
					//stopped = true;
					//transform.position.x += offx;
					print("\"");
					Attack(token);
				}

			}
		}
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		string tag = collision.tag;
		if (PlayerTags.Contains(tag) || EnemyTags.Contains(tag))
		{
			if (collision.GetType() == typeof(CircleCollider2D))
			{
				if (SeenEnemy)
				{
					SeenEnemy = false;
				}

			}

			if (collision.GetType() == typeof(BoxCollider2D))
			{
				tokenSource.Cancel();
				tokenSource.Dispose();
				tokenSource = new CancellationTokenSource();
				token = tokenSource.Token;
			}
		}
	}

	private void OnMouseOn()
	{
		print("mysz");
	}
}

public enum WeaponType
{
	Cold, Distance
}