using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameplayControllerInitializer : MonoBehaviour
{
	protected string Type;
	public readonly string[] PlayerTags = { "PlayerWarrior", "PlayerInfrantry", "PlayerCrossbower", "PlayerHeavyInfrantry" };
	public readonly string[] EnemyTags = { "Enemy", "EnemyInfrantry", "EnemyAxer", "EnemyBower" };
	public Dictionary<string, int> Items;
	public static GameplayController Instance;
	public Camera Camera;
	public GameObject mainCamera;
	public List<UnitModel> units = new List<UnitModel>();
	protected string pathToCursors = "Prefabs/HUD/Cursors";
	protected SaveLoadUtility slu;

	public virtual void Start()
	{
		Items = new Dictionary<string, int>()
		{
			["meat"] = 0,
			["leather"] = 0,
			["apple"] = 0,
			["bread"] = 0,
			["beer"] = 0,
			["sausage"] = 0,
			["iron"] = 0,
			["clay"] = 0,
			["wood"] = 0,
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

}
