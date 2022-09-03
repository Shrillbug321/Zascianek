using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


	public class EnemyInfrantry : Enemy
	{
	public override void Start()
	{
		base.Start();
		AttackSpeed = 1500;
		Damage = 13;
	}

	// Update is called once per frame
	void Update()
	{
		base.Update();
	}

	protected async override Task Attack(CancellationToken token)
	{
		do
		{
			enemy.DecreaseHP(Damage);
			enemy.Blinking(AttackSpeed, token);
			await Task.Delay(1500);
		} while (enemy.HP > 0 && !token.IsCancellationRequested);


		if (token.IsCancellationRequested)
			return;

		await enemy.Rotate();
		Destroy(enemy.gameObject);
	}
}

