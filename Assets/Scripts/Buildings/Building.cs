using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Building : MonoBehaviour
{
	public int id { get; set; }
	public int typeId { get; set; }
	public string buildingName { get; set; }
	public bool isFireable { get; set; }
	public bool canStartFire { get; set; }
	public int maxDp { get; set; }
	public int dp { get; set; }
	public int productionTime { get; set; }
	public int transportTime { get; set; }
	protected float time { get; set; }
	public Dictionary<string, int> product { get; set; }
	public Building stockBuilding;
	public string stockBuildingName;
	public int productionProgress { get; set; }
	public string color;
	//public const int MONTH = 30000;
	public const int MONTH = 1;
	public static Dictionary<string, int> needToBuild = new Dictionary<string, int>()
	{
		["Iron"] = 0,
		["Clay"] = 0,
		["Wood"] = 0,
		["Gold"] = 0,
		["Money"] = 0
	};
	public BuildingStatus status;

	public Rigidbody2D rb2D;
	public SpriteRenderer sr;
	public bool isColliding;


	public virtual void Start()
	{
		//UnitId = Units.Count;
		buildingName = name;
		//WorkBuildingId = 1;

		gameObject.AddComponent<Rigidbody2D>();
		rb2D = GetComponent<Rigidbody2D>();
		GetComponent<Rigidbody2D>().freezeRotation = true;

		//gameObject.AddComponent<SpriteRenderer>();
		sr = GetComponent<SpriteRenderer>();
		sr.sortingLayerName = "Buildings";
		gameObject.layer = LayerMask.NameToLayer("Buildings");

		BoxCollider2D bc2d = gameObject.AddComponent<BoxCollider2D>();
		//bc2d.isTrigger = true;
		bc2d.size = new Vector2(1.17f, 2.27f);

		/*oldPos = rb2D.position;
		movement = rb2D.position;
		healthBar = Instantiate(Resources.Load<Image>("Prefabs/HUD/HealthBar"));
		healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
		//healthBar.SetActive(false);

		unitTokenSource = new CancellationTokenSource();
		unitToken = unitTokenSource.Token;*/

		gameplay.AddBuilding(this);
		//Production();
	}

	public virtual async Task<Dictionary<string, int>> Production()
	{
		while (productionProgress < 99)
			await Task.Delay(100);

		return product;
	}


	/*public async Task WaitForProduct()
	{
	}*/

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

	/*public void AddItem(string item, int howMany)
	{
		productionProgress =0;
	}*/

	public virtual void Reset()
	{
		productionProgress = 0;
	}

	public virtual string CanBuild()
	{
		string result = "";
		foreach (var item in needToBuild)
		{
		Debug.LogWarning(item.Key);
		Debug.LogWarning(item.Value);
		Debug.LogWarning(gameplay.items[item.Key]);
			if (gameplay.items[item.Key] < item.Value)
			{
				result += item.Value + " " + item.Key;
			}
		}
		return result;
	}

	public virtual void OnTriggerEnter2D(Collider2D collision)
	{
		string tag = collision.tag;
		//print(tag);
		//if (CompareTag(tag)) return;
		if (GetComponent<CircleCollider2D>().IsTouching(collision))
		{
			Building entered = collision.GetComponent<Building>();
			if (tag == "InBuild")// && !touchCollider)
			{
				entered.isColliding = true;
				entered.sr.color = Color.gray;
				//gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
			}
		}
	}
	public virtual void OnTriggerExit2D(Collider2D collision)
	{
		string tag = collision.tag;
		//print(tag);
		Building entered = collision.GetComponent<Building>();
		//if (CompareTag(tag)) return;
		if (tag == "InBuild")// && !touchCollider)
		{
			entered.isColliding = false;
			entered.sr.color = Color.white;
			//gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
		}
	}
}

public enum BuildingStatus
{
	production, transport, waitingForProduct, waitingForWorker, isStock
}