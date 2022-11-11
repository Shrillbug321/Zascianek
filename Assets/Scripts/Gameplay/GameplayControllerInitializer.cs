using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
	public List<UnitModel> units = new List<UnitModel>();
	public List<Building> buildings = new List<Building>();
	protected string pathToCursors = "Prefabs/HUD/Cursors";
	protected SaveLoadUtility slu;
	protected GameObject building;
	protected bool buildingIsPlacing;
	public static HUDController hud;
	public string lastClicked;
	public bool unitIsChoosen;
	public Mode mode = Mode.nothing;

	public virtual void Start()
	{
		hud = new();
		mode = Mode.nothing;
		items = new Dictionary<string, int>()
		{
			["Meat"] = 0,
			["Leather"] = 0,
			["Apple"] = 0,
			["Bread"] = 0,
			["Beer"] = 0,
			["Sausage"] = 0,
			["Iron"] = 0,
			["Clay"] = 0,
			["Wood"] = 4,
			["Gold"] = 0,
			["Money"] = 0,
			["Wheat"] = 0,
			["Flour"] = 0,
			["Hop"] = 0,
			["Sable"] = 0,
			["Armor"] = 0,
			["Crossbow"] = 0
		};
		hud.Start();
	}
	public int GetItem(string item)
	{
		return items[item];
	}
}
public enum Mode{
	placing, unit, building, nothing
}