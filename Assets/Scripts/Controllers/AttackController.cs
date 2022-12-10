using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;
using static GameplayControllerInitializer;

public class AttackController : MonoBehaviour
{
	private Range units = 1..10;
	private Range timeRange = 1..100;
	private Random random = new();
	private string[] enemiesNames = { "EnemyInfrantry", "EnemyAxer", "EnemyBower" };
	private float time;
	private int timeToNextAttack;
	//private int enemies;
	public void Start()
	{
		time = Time.time;
		MakeAttack();
	}

	public void Update()
	{
		time += Time.deltaTime;
		if (time > timeToNextAttack)
		{
			//MakeAttack();
			time = Time.time;
			timeToNextAttack = (int)(time + 1f);//random.Next(1, 2);
		}
	}

	private void MakeAttack()
	{
		//while (true)
		{
			//int units = random.Next(1, 10);
			int units = 2;
			for (int i = 0; i < units; i++)
			{
				int enemyTypeIndex = random.Next(0, 2);
				string enemyType = enemiesNames[enemyTypeIndex];
				Enemy enemy = Resources.Load<Enemy>("Prefabs/Units/Warriors/Enemy/" + enemyType);
				Instantiate(enemy);
				//enemies++;
			}
		}
	}
}
