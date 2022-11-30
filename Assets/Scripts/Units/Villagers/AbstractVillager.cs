using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbstractVillager : UnitModel
{
	Dictionary<string, int> items = new();
	public string workBuildingName;
	public int workBuildingId;
	public List<string> stockBuildingsNames;
	public List<string> getItemBuildingsNames;
	public List<string> buildingActions;
	public List<GameObject> route;
	private Dictionary<string, int> needToProduction;
	private int buildingInRoute = 0;
	private bool hitTree;
	public void AssignToBuilding(Building building)
	{
		route = new();
		workBuildingName = building.name;
		workBuildingId = building.id;
		foreach (string buildingName in building.getItemBuildingsNames)
		{
			route.Add(GameObject.Find(buildingName));
			getItemBuildingsNames.Add(buildingName);
			buildingActions.Add("get");
		}

		route.Add(building.gameObject);
		buildingActions.Add("work");
		needToProduction = building.needToProduction;
		foreach (string buildingName in building.stockBuildingsNames)
		{
			route.Add(GameObject.Find(buildingName));
			stockBuildingsNames.Add(buildingName);
			buildingActions.Add("stock");
		}
		workBuilding = building;
		workBuilding.hasWorker = true;
		//stockBuildingName = building.stockBuildingName;
		//building.stockBuilding = GameObject.Find(building.stockBuildingName).GetComponent<Building>();
		GameObject next = route[0];
		movement = next.transform.position;
		moveStart = true;
		/*movement = building.transform.position;
		moveStart = true;*/
	}

	public override async void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.GetType() == typeof(CircleCollider2D))
		{
			if (route.Count == 0) return;
			bool reachedGoal = false;
			GameObject gameObject = route[buildingInRoute];
			if (gameObject.GetComponent<Building>() != null)
			{
				//Building building = collision.GetComponent<Building>();
				Building building = gameObject.GetComponent<Building>();
				Debug.LogWarning(name);
				if (collision.name == workBuildingName)
				{
					if (building.id == workBuildingId)
					{
						moveStart = false;
						//Hide();
						sr.color = Color.clear;
						building.Reset();
						if (building is ProductionBuilding productionBuilding)
						{
							if (productionBuilding.CanProduction(items))
								items = await productionBuilding.Production();
						}

						sr.color = Color.white;
						reachedGoal = true;
					}
					else
					{
						base.OnTriggerEnter2D(collision);
						return;
					}
				}
				if (building.name == collision.name && getItemBuildingsNames.Contains(building.name))
				{
					moveStart = false;
					await Wait(2000);
					if (buildingActions[buildingInRoute] == "get")
					{
						Dictionary<string, int> itemsFromBuilding = building.GetItems(needToProduction).Result;
						while (itemsFromBuilding == null)
						{
							await Wait(2000);
							itemsFromBuilding = building.GetItems(needToProduction).Result;
						}
						foreach (KeyValuePair<string, int> item in itemsFromBuilding)
						{
							items[item.Key] = item.Value;
						}
					}
					reachedGoal = true;
					/*if (buildingActions[buildingInRoute] == "get")

						building.GetItems(items);*/
					//GameplayControllerInitializer.gameplay.items[item.Keys.First()] += item.Values.First();
					//building.AddItem();
					/*if (getItemBuildingsNames != null)
					{
						movement = building.getItemBuildings[0].transform.position;
					}
					else
					{*/
					//movement = workBuilding.transform.position;
					//}

					//moveStart = true;
				}
				if (building.name == collision.name && stockBuildingsNames.Contains(building.name))
				{
					moveStart = false;
					await Wait(2000);
					if (buildingActions[buildingInRoute] == "stock")
					{
						building.AddItems(items);
						items = new();
						/*var itemsFromBuilding = building.GetItems(needToProduction);
						foreach (KeyValuePair<string, int> item in itemsFromBuilding)
						{
							items[item.Key] = item.Value;
						}*/

					}
					reachedGoal = true;
					//building.GetItems(items);
					//GameplayControllerInitializer.gameplay.items[item.Keys.First()] += item.Values.First();
					//building.AddItem();
					/*if (getBuildingIndex < building.getItemBuildings.Length - 1)
					{
						getBuildingIndex++;
						movement = building.getItemBuildings[getBuildingIndex].transform.position;
					}
					else
					{
						movement = workBuilding.transform.position;
					}
					moveStart = true;*/
				}
			}
			if (gameObject.tag == "Tree" && collision.tag == "Tree" && !hitTree)
			{
				GameObject tree = route[buildingInRoute];
				if (collision.GetComponent<Tree>().id == tree.GetComponent<Tree>().id)
				{
					hitTree = true;
					int index = buildingInRoute;
					bool done = false;
					await Wait(2000);
					await tree.Rotate();
					if (index == buildingInRoute)
					{
						Destroy(tree);
						route[buildingInRoute] = Lumberjack.FindNearestTree(workBuilding.transform.position);
						reachedGoal = true;
						hitTree = false;
					}
					else return;
				}
			}

			if (reachedGoal)
			{
				if (++buildingInRoute >= route.Count)
					buildingInRoute = 0;
				GameObject next = route[buildingInRoute];
				movement = next.transform.position;
				moveStart = true;
			}
			else
				base.OnTriggerEnter2D(collision);
			return;
		}

	}

	public override async void OnTriggerExit2D(Collider2D collision)
	{
		string name = collision.name;
		if (!stockBuildingsNames.Contains(name) && getItemBuildingsNames.Contains(name) && workBuildingName != name) base.OnTriggerExit2D(collision);
	}
}
