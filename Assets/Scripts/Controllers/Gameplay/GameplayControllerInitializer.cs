using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class GameplayControllerInitializer : MonoBehaviour
{
	//Constants
	public float mapWidth = 40;
	public float mapHeight = 40;
	public const int MONTH_DURATION = 1;
	
	//Constants collections
	public readonly string[] playerTags = { "Warrior", "PlayerInfrantry", "PlayerCrossbower", "PlayerHeavyInfrantry" };
	public readonly string[] enemyTags = { "Enemy", "EnemyInfrantry", "EnemyAxer", "EnemyBower" };
	public readonly string[] foods = { "Meat", "Apple", "Bread", "Beer", "Sausage" };
	
	//Unity objects
	public Mouse mouse;
	public Tilemap tilemap;
	public GameObject mainCamera;
	public GameObject minimapCamera;
	public GameObject settler, houseSettler;
	public GameObject roadSign;
	
	//Controllers
	public static GameplayController gameplay;
	public static HUDController hud;
	public CameraController cameraController;
	public InhabitantController ic = new();
	public AttackController attackController;
	public UnitController unitController = new();
	public SaveGameController saveGameController;
	public MusicController musicController;
	
	//Collections
	public Dictionary<string, int> items;
	public Dictionary<string, List<UnitModel>> units;
	public Dictionary<string, List<Building>> buildings;
	public List<Enemy> enemies;
	
	//Flags
	[FormerlySerializedAs("unitIsChoosen")]
	public bool unitIsChosen;
	public bool paused;
	public bool runningInhabitansTextIsShowed;
	public bool isGameOver;
	public bool attack;
	
	//Time
	public int month = 1, year = 1500;
	public float actualMonthPassed;
	public float timeScale = 1;
	
	//Buildings
	public Building lastBuilding;
	public Building clickedBuilding;
	
	//Vectors
	public Vector2 startPath, endPath;
	public Vector2 settlerHousePos;
	
	//Other public
	public Mode mode = Mode.nothing;
	public System.Random random = new();
	
	protected Vector2 mousePos, mousePosInWorld;
	protected string pathToCursors = "Prefabs/HUD/Cursors";
	protected GameObject building;
	protected bool buildingIsPlacing;

	public virtual void Start()
	{
		attackController = new AttackController();
		mode = Mode.nothing;
		tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
		items = new Dictionary<string, int>()
		{
			["Meat"] = 100,
			["Leather"] = 100,
			["Apple"] = 200,
			["Bread"] = 10,
			["Beer"] = 10,
			["Sausage"] = 10,
			["Iron"] = 50,
			["Clay"] = 100,
			["Wood"] = 100,
			["Gold"] = 100,
			["Money"] = 100,
			["Wheat"] = 100,
			["Flour"] = 100,
			["Hop"] = 10,
			["Sable"] = 10,
			["Armor"] = 10,
			["Crossbow"] = 10
		};

		buildings = new Dictionary<string, List<Building>>
		{
			["AppleField"] = new(),
			["WheatField"] = new(),
			["HopField"] = new(),
			["Pigsty"] = new(),
			["Mill"] = new(),
			["Brewery"] = new(),
			["Bakery"] = new(),
			["Butcher"] = new(),
			["Lumberjack"] = new(),
			["MineClay"] = new(),
			["MineIron"] = new(),
			["MineGold"] = new(),
			["HouseVillager"] = new(),
			["HouseRichVillager"] = new(),
			["HouseNobility"] = new(),
			["HouseSettler"] = new(),
			["CrossbowMaker"] = new(),
			["Armorer"] = new(),
			["Smith"] = new(),
			["Barracks"] = new(),
			["Granary"] = new(),
			["Magazine"] = new(),
			["Armory"] = new(),
			["Market"] = new(),
			["Well"] = new(),
			["Church"] = new()
		};

		units = new Dictionary<string, List<UnitModel>>
		{
			["Villager"] = new(),
			["RichVillager"] = new(),
			["Nobility"] = new(),
			["Priest"] = new(),
			["Infrantry"] = new(),
			["HeavyInfrantry"] = new(),
			["Crossbower"] = new(),
			["Settler"] = new(),
			["EnemyInfrantry"] = new(),
			["Axer"] = new(),
			["Bower"] = new(),
		};
		gameObject.AddComponent<HUDController>();
		hud = GetComponent<HUDController>();
		gameObject.AddComponent<AttackController>();
		gameObject.AddComponent<SaveGameController>();
		saveGameController = gameObject.GetComponent<SaveGameController>();
		houseSettler = GameObject.Find("HouseSettler");
		musicController = GameObject.Find("MusicPlayer").GetComponent<MusicController>();
		year = 1500;
		settler = GameObject.Find("Settler");
		enemies = new List<Enemy>();
	}
	public int GetItem(string item)
	{
		return items[item];
	}
}
public enum Mode{
	nothing, placing, unit, building, house, tax, recruit, market,
	repair, destroy, cut
}