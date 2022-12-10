using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class GameplayControllerInitializer : MonoBehaviour
{
	protected string Type;
	protected Vector2 mousePos;
	public readonly string[] playerTags = { "Warrior", "PlayerInfrantry", "PlayerCrossbower", "PlayerHeavyInfrantry" };
	public readonly string[] enemyTags = { "Enemy", "EnemyInfrantry", "EnemyAxer", "EnemyBower" };
	public Dictionary<string, int> items;
	public static GameplayController gameplay;
	public Camera camera;
	public Mouse mouse;
	public GameObject mainCamera;
	public GameObject minimapCamera;
	public Dictionary<string, List<UnitModel>> units;
	public Dictionary<string, List<Building>> buildings;
	public List<Enemy> enemies;
	protected string pathToCursors = "Prefabs/HUD/Cursors";
	protected SaveLoadUtility slu;
	protected GameObject building;
	protected bool buildingIsPlacing;
	public static HUDController hud;
	public string lastClicked;
	public bool unitIsChoosen;
	public Mode mode = Mode.nothing;
	public Tilemap tilemap;
	public float mapWidth = 20;
	public float mapHeight = 10;
	public string[] foods = { "Meat", "Apple", "Bread", "Beer", "Sausage" };
	//public const int MONTH_DURATION = 30000;
	public const int MONTH_DURATION = 1;
	public GameObject settler, houseSettler;
	public bool paused = false;
	public Building lastBuilding;
	public int month = 1, year = 1500;
	public float actualMonthPassed = 0;
	public Building clickedBuilding;
	public bool runningInhabitansTextIsShowed = false;
	public bool isGameOver = false;
	public float timeScale = 1;
	public bool attack = false;
	public Vector2 settlerHousePos;
	public System.Random random = new();
	public CameraController cameraController;
	public InhabitantController ic = new();
	public AttackController attackController;
	public UnitController unitController = new();
	public SaveGameController saveGameController;
	public MusicController musicController;

	public virtual void Start()
	{
		//hud = new();
		attackController = new();
		mode = Mode.nothing;
		tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
		items = new Dictionary<string, int>()
		{
			["Meat"] = 100,
			["Leather"] = 0,
			["Apple"] = 200,
			["Bread"] = 0,
			["Beer"] = 0,
			["Sausage"] = 0,
			["Iron"] = 50,
			["Clay"] = 10,
			["Wood"] = 100,
			["Gold"] = 0,
			["Money"] = 100,
			["Wheat"] = 10,
			["Flour"] = 0,
			["Hop"] = 0,
			["Sable"] = 0,
			["Armor"] = 0,
			["Crossbow"] = 0
		};

		buildings = new() {
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

		units = new()
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
		enemies = new();
		//camera = GameObject.Find("MainCamera(Clone)").GetComponent<Camera>();
		//attackController.Start();
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