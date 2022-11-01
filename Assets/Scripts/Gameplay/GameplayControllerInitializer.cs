using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayControllerInitializer : MonoBehaviour
{
	protected string Type;
	public readonly string[] playerTags = { "PlayerWarrior", "PlayerInfrantry", "PlayerCrossbower", "PlayerHeavyInfrantry" };
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
	protected bool isMove;

	public virtual void Start()
	{
		items = new Dictionary<string, int>()
		{
			["meat"] = 0,
			["leather"] = 0,
			["apple"] = 0,
			["bread"] = 0,
			["beer"] = 0,
			["sausage"] = 0,
			["iron"] = 0,
			["clay"] = 0,
			["wood"] = 4,
			["gold"] = 0,
			["money"] = 0,
			["wheat"] = 0,
			["flour"] = 0,
			["hop"] = 0,
			["sable"] = 0,
			["armor"] = 0,
			["crossbow"] = 0
		};
	}
	public int GetItem(string item)
	{
		return items[item];
	}
}
