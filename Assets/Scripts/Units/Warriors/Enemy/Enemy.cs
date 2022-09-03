using Assets.Scripts;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : AbstractWarrior
{
	public override void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	protected void Update()
	{
		base.Update();
		ComparingTags = new string[] { "PlayerWarrior", "PlayerInfrantry", "PlayerCrossbower", "EnemyHeavyInfrantry" };
	}
}
