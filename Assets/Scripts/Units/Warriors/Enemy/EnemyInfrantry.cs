using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyInfrantry : Enemy
	{
	public override void Start()
	{
		base.Start();
		AttackSpeed = 2000;
		DamageMin = 18;
		DamageMax = 20;
		Armor = 8;
		WeaponType = WeaponType.Cold;
		SetCircleCollider(5);
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}
}