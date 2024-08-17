using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class AbstractVillager : UnitModel
{
	public string workBuildingName;
	public int workBuildingId = -1;
	public List<string> stockBuildingsNames;
	public List<string> getItemBuildingsNames;
	public List<string> buildingActions;
	public List<GameObject> route;
	public Vector2 housePos { get; set; }
	public bool haveHome;
    public bool employed;
    public bool goToBarracks;
    public AbstractHouse house;
	
    private Dictionary<string, int> items = new();
	private Dictionary<string, int> needToProduction;
	private int buildingInRoute;
	private bool hitTree;
	private GameObject nextBuilding;
	private string lastEntered = "";
	
	public override void Start()
	{
		base.Start();
		workBuildingId = -1;
	}
	public void AssignToBuilding(Building building)
	{
		route = new List<GameObject>();
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
		workBuilding.worker = this;
		workBuilding.status = workBuilding.initStatus;
		//stockBuildingName = building.stockBuildingName;
		//building.stockBuilding = GameObject.Find(building.stockBuildingName).GetComponent<Building>();
		nextBuilding = route[0];
		movement = nextBuilding.transform.position;
		moveStart = true;
		employed = true;
		/*movement = building.transform.position;
		moveStart = true;*/
	}

	public void RemoveFromBuilding()
	{
		moveStart = false;
		route.Clear();
		GoToHouse();
		sr.color = Color.white;
		employed = false;
		moveStart = true;
	}

	public void BuildingStopped()
	{
		moveStart = false;
		//workBuilding = null;
		//lastWorkBuildingId = workBuildingId;
		employed = false;
		GoToHouse();
		sr.color = Color.white;
		moveStart = true;
	}

	public void BuildingResumed()
	{
		moveStart = false;
		nextBuilding = route[0];
		movement = nextBuilding.transform.position;
		buildingInRoute = 0;
		items = new Dictionary<string, int>();
		employed = true;
		moveStart = true;
	}

	public void GoToHouse()
	{
		moveStart = true;
		movement = housePos;
	}

	//This part is responsible for collision and should be refactor
	public override async void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision is CircleCollider2D)
		{
			/*if (route.Count == 0) return;
			if (lastEntered == collision.name) return;*/
			bool reachedGoal = false;
			//GameObject gameObject = route[buildingInRoute];
			if (nextBuilding.GetComponent<Building>() != null)
			{
				//Building building = collision.GetComponent<Building>();
				Building building = nextBuilding.GetComponent<Building>();
				Debug.LogWarning(name);
				if (lastEntered != building.name && building.name == collision.name && collision.name == workBuildingName)
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
							{
								items = await productionBuilding.Production();
								//building.status = BuildingStatus.transport;
							}
						}

						//workBuilding.NextStatus();
						sr.color = Color.white;
						reachedGoal = true;
					}
					else
					{
						//base.OnTriggerEnter2D(collision);
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
						workBuilding.status = BuildingStatus.workerReturnWithItem;
					}
					//workBuilding.NextStatus();
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
					if (buildingActions[buildingInRoute] == "stock")
					{
						await Wait(2000);
						building.AddItems(items);
						items = new Dictionary<string, int>();
						/*var itemsFromBuilding = building.GetItems(needToProduction);
						foreach (KeyValuePair<string, int> item in itemsFromBuilding)
						{
							items[item.Key] = item.Value;
						}*/

						workBuilding.status = BuildingStatus.workerReturnFromStock;
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
			if (collision.CompareTag("Tree") && !hitTree)
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
				{
					buildingInRoute = 0;
					/*if (workBuilding.stopping)
					{
						workBuilding.stopped = true;
						BuildingStopped();
						return;
					}*/
					workBuilding.status = workBuilding.initStatus;
				}
				lastEntered = gameObject.name;
				nextBuilding = route[buildingInRoute];
				movement = nextBuilding.transform.position;
				moveStart = true;
			}
			/*else
				base.OnTriggerEnter2D(collision);*/
			return;
		}
		if (collision is EdgeCollider2D)
			GoToHouse();
	}

	public override async void OnTriggerExit2D(Collider2D collision)
	{
		string name = collision.name;
		if (!stockBuildingsNames.Contains(name) && getItemBuildingsNames.Contains(name) && workBuildingName != name)
			base.OnTriggerExit2D(collision);
	}

	public void MoveFromStartPathToHome()
	{
		transform.position = GameplayControllerInitializer.gameplay.startPath;
		movement = GameplayControllerInitializer.gameplay.endPath;
		moveStart = true;
	}
}