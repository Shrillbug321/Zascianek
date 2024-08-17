using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using static GameplayControllerInitializer;

public class Building : MonoBehaviour
{
	//General
	public int id;
	public string buildingName { get; set; }
	public static int buildingsNumber;
	public bool isFireable { get; set; }
	public bool canStartFire { get; set; }
	public int maxDp { get; set; }
	public int dp { get; set; }
	public string color;
	
	public BuildingStatus status;
	public BuildingStatus initStatus = BuildingStatus.waitingForWorker;
	public List<BuildingStatus> statuses;
	
	//Need to build
	public Dictionary<string, int> needToBuild;
	public string[] grounds;
	
	//Production
	public int productionTime { get; set; }
	public int transportTime { get; set; }
	public Dictionary<string, int> products { get; set; }
	public Building stockBuilding;
	public List<string> stockBuildingsNames;
	public Dictionary<string, Building> getItemBuildings;
	public Dictionary<string, int> stockedItems;
	public string[] getItemBuildingsNames;
	public Dictionary<string, int> needToProduction;
	public AbstractVillager worker;
	public bool stopped;
	public bool stopping;

	//Unity Components
	public Rigidbody2D rb2D;
	public SpriteRenderer sr;
	public bool isColliding;

	protected float updateTime { get; set; }
	public virtual void Start()
	{
		buildingName = name;
		status = BuildingStatus.waitingForWorker;
		initStatus = BuildingStatus.waitingForWorker;

		gameObject.AddComponent<Rigidbody2D>();
		rb2D = GetComponent<Rigidbody2D>();
		GetComponent<Rigidbody2D>().freezeRotation = true;

		sr = GetComponent<SpriteRenderer>();
		sr.sortingLayerName = "InBuild";
		gameObject.layer = LayerMask.NameToLayer("InBuild");

		//In this place should be health bar
		/*oldPos = rb2D.position;
		movement = rb2D.position;
		healthBar = Instantiate(Resources.Load<Image>("Prefabs/HUD/HealthBar"));
		healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
		healthBar.SetActive(false);*/
	}

	public virtual void DecreaseDP(int HowMany)
	{
		print(HowMany);
		dp = HowMany > 0 ? dp - HowMany : dp;
	}

	public async Task Blinking(int attackSpeed, CancellationToken token)
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		while (!token.IsCancellationRequested)
		{
			sr.color = Color.red;
			await Task.Delay(500);
			sr.color = Color.white;
			await Task.Delay(attackSpeed - 500);
		}
	}

	public virtual void Reset()
	{
	}

	public string CheckItemsNeedToBuilding()
	{
		string result = "";
		if (needToBuild == null) return result;
		foreach (var item in needToBuild)
		{
			if (gameplay.items[item.Key] < item.Value)
				result += item.Value + " " + Texts.itemsNames[item.Key] + " ";
		}
		return result;
	}

	public string CheckGround(Vector2 position)
	{
		string result = "Nieprawidłowe podłoże";
		if (grounds.Length == 0) return "";
		Tilemap tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
		foreach (string ground in grounds)
		{
			bool inRange = tilemap.TileTypeInRange(ground, position, 0);
			if (inRange)
				return "";
		}
		return result;
	}

	public bool CheckGroundIsEmpty(Vector2 position)
	{
		LayerMask mask = LayerMask.GetMask("Buildings");
		RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Buildings"));
		//if (hit.collider != null) sr.color = Color.gray;
		return hit.collider == null;
	}

	public string CheckStockBuildingWasBuilded()
	{
		foreach (string stockName in stockBuildingsNames)
		{
			GameObject stock = GameObject.Find(stockName);
			if (stock == null) return stockName;
		}
		return "";
	}
	
	public bool CanProduction(Dictionary<string, int> items = null)
	{
		if (needToProduction == null)
		{
			status = BuildingStatus.production;
			return true;
		}
		if (needToProduction != null && items == null) return false;
		foreach (var item in items)
		{
			if (item.Value < needToProduction[item.Key])
				return false;
		}
		status = BuildingStatus.production;
		return true;
	}

	public void Build(Vector2 pos)
	{
		transform.position = pos;
		tag = "Building";
		color = "Green";
		id = buildingsNumber++;
		gameplay.AddBuilding(this);
		gameObject.layer = LayerMask.NameToLayer("Buildings");
		DecreaseItemsToBuild();
	}

	public void Repair()
	{
		if (needToBuild != null)
			foreach (KeyValuePair<string, int> item in needToBuild)
				gameplay.items[item.Key] -= (int)Math.Round(item.Value * 0.1f);
		gameplay.items["Money"] -= 30;
		dp = maxDp;
	}

	public virtual void DecreaseItemsToBuild()
	{
		if (needToBuild == null) return;
		foreach (var item in needToBuild)
			gameplay.items[item.Key] -= item.Value;
	}

	public void AddItems(Dictionary<string, int> items)
	{
		foreach (var item in items)
			gameplay.items[item.Key] += item.Value;
	}

	public async Task<Dictionary<string, int>> GetItems(Dictionary<string, int> items)
	{
		Dictionary<string, int> result = new Dictionary<string, int>();
		foreach (var item in items)
		{
			if (stockedItems.ContainsKey(item.Key))
			{
				if (gameplay.items[item.Key] < item.Value)
					return null;
				if (gameplay.items[item.Key] >= item.Value)
				{
					result.Add(item.Key, item.Value);
					gameplay.items[item.Key] -= item.Value;
				}
			}

		}
		return result;
	}

	//Two next functions should be responsible for unit-building collision
	public virtual void OnTriggerEnter2D(Collider2D collision)
	{
		string tag = collision.tag;
		//print(tag);
		//if (CompareTag(tag)) return;
		/*if (tag == "Building")// && !around)
		{
			Building entered = collision.GetComponent<Building>();
			if (GetComponent<CircleCollider2D>().IsTouching(collision))
			{
				//entered.isColliding = true;
				//if (tag == "InBuild")
				entered.sr.color = Color.gray;
				//gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
			}
		}*/
	}
	public virtual void OnTriggerExit2D(Collider2D collision)
	{
		string tag = collision.tag;
		//print(tag);
		Building entered = collision.GetComponent<Building>();
		//if (CompareTag(tag)) return;
		/*if (tag == "InBuild")// && !around)
		{
			entered.isColliding = false;
			entered.sr.color = Color.white;
			//gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
		}*/
	}
}

public enum BuildingStatus
{
	production, transport, waitingForProduct, waitingForWorker, isStock,
	workerGoForItem, workerReturnFromStock, workerReturnWithItem
}