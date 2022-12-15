using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Barracks : Building
{
	public List<string> unitTypes = new();
	public List<int> unitIds = new();
	public override void Start()
	{
		base.Start();
		dp = maxDp = 60;
		buildingName = "Barracks";
		needToBuild = new()
		{
			["Wood"] = 10,
			["Clay"] = 5
		};
	}

	public virtual void OnTriggerEnter2D(Collider2D collision)
	{
		string tag = collision.tag;
		if (collision.GetType() == typeof(CircleCollider2D))
		{
			AbstractVillager unit = collision.GetComponent<AbstractVillager>();
			if (collision.tag == "Villager" && unit.goToBarracks)
			{
				unit.house.RemoveInhabitantForWarrior();
				//Destroy(collision.gameObject);
				UnitModel warrior = Instantiate(Resources.Load<UnitModel>("Prefabs/Units/Warriors/Player/" + unitTypes[0]));
				Vector3 pos = transform.position;
				warrior.transform.position = new Vector3(pos.x, pos.y - 1f, 0);
				unitTypes.RemoveAt(0);
			}
		}
	}
}
