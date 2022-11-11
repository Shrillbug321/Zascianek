using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbstractVillager : UnitModel
{
	Dictionary<string, int> item;
	public void AssignToBuilding(Building building)
	{
		workBuilding = building;
		stockBuildingName = building.stockBuildingName;
		building.stockBuilding = GameObject.Find(building.stockBuildingName).GetComponent<Building>();
		movement = building.transform.position;
		moveStart = true;
	}

	public override async void OnTriggerEnter2D(Collider2D collision)
	{

	base.OnTriggerEnter2D (collision);

		if (collision.GetType() == typeof(CircleCollider2D))
		{
			if (collision.tag == "Building")
			{
				Building building = collision.GetComponent<Building>();

				if (building == workBuilding)
				{
					moveStart = false;
					//Hide();
					sr.color = Color.clear;
					building.Reset();
					item = await building.Production();

					/*print(item.Keys.First());
					print(item.Values.First());*/
					sr.color = Color.white;
					movement = building.stockBuilding.transform.position;
					moveStart = true;
				}
				if (collision.name == stockBuildingName)
				{
					moveStart = false;
					await Wait(2000);
					GameplayControllerInitializer.gameplay.items[item.Keys.First()] += item.Values.First();
					//building.AddItem();

					movement = workBuilding.transform.position;
					moveStart = true;
				}
			}
			return;
		}
	}
}
