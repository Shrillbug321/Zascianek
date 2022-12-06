using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameplayController : GameplayControllerInitializer
{
	public override void Start()
	{
		base.Start();
		mouse = Mouse.current;
		slu = gameObject.AddComponent<SaveLoadUtility>();
		mainCamera = Resources.Load<GameObject>("Prefabs/Common/MainCamera");
		Instantiate(mainCamera);
		//MakeMoney();
		//MakeTree();
		/*object[] a = Resources.FindObjectsOfTypeAll(typeof(GameObject));
		foreach (GameObject b in a)
		{
			var collider = b.GetComponent<CircleCollider2D>();
			if (collider != null)
			{
				collider.radius += 0.01f;
			}
		}*/
	}


	private void Awake()
	{
		if (gameplay != null && gameplay != this)
			Destroy(this);
		else
			gameplay = this;
	}

	void Update()
	{
		if (paused) return;
		ic.Update();
		mousePos = GetMousePosToWorldPoint();

		actualMonthPassed += Time.deltaTime;
		if (actualMonthPassed > MONTH_DURATION)
		{
			actualMonthPassed = 0;
			if (++month > 12)
			{
				month = 1;
				year++;
			}
			hud.date.text = $"{hud.monthNames[gameplay.month]} A.D. {gameplay.year}";
		}

		foreach (KeyValuePair<string, List<Building>> slot in buildings)
		{
			if (slot.Value.Count > 0)
			{
				foreach (Building building in slot.Value)
				{
					if (building is ProductionBuilding && !building.hasWorker)
					{
						AbstractVillager villager = FindUnemployedVillager();
						if (villager != null)
							villager.AssignToBuilding(building);
					}
				}
			}
		}

		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f);
		if (hit.collider != null && MouseInRange(mousePos, hit, 0.75f))
		{
			string tag = hit.collider.gameObject.tag;

			switch (WhatIsHit(tag))
			{
				case "Warrior":
					OnMouseEnter("Warrior");
					break;
				case "Enemy":
					OnMouseEnter("Enemy");
					break;
				case "Building":
					if (mode == Mode.unit)
					{
						if (hit.collider.gameObject.name == "Church")
						{
							SetCursor(pathToCursors + "/heal");
						}
					}
					break;
				default:
					OnMouseExit();
					break;
			}
			if (mouse.leftButton.wasPressedThisFrame)
			{
				switch (WhatIsHit(tag))
				{
					case "Warrior" or "Villager" or "Enemy":
						mode = Mode.unit;
						hud.SwitchUnitBar(hit.collider.gameObject.GetComponent<UnitModel>());
						break;
					case "Save":
						Save();
						break;
					case "Load":
						Load();
						break;
					case "Granary":
						if (items["Money"] < 15)
						{
							//ShowGUIImage("Prefabs/HUD/Image");
							hud.ShowGUIText("Potrzeba 15 monet!");
							return;
						}
						else
						{
							var a = Instantiate(Resources.Load<UnitModel>("Prefabs/Units/infrantry"));
							Vector3 pos = hit.collider.transform.position;
							a.transform.position = new Vector3(pos.x, pos.y - 1f, 0);
							a.gameObject.name = units.Count.ToString();
						}
						break;
					case "Building":
						if (mode == Mode.nothing)
						{
							mode = Mode.building;
							hud.buildingController.BuildingClick(hit.collider.gameObject.GetComponent<Building>());
						}
						if (mode == Mode.destroy)
						{
							Building building = hit.collider.GetComponent<Building>();
							if (building.needToBuild != null)
								foreach (KeyValuePair<string, int> item in building.needToBuild)
									gameplay.items[item.Key] += (int)Math.Round(item.Value * 0.9f);
							//if (building is AbstractHouse)
							RemoveBuilding(building);
							Destroy(building.gameObject);
						}
						break;
					case "Tree":
						if (mode == Mode.cut)
						{
							Destroy(hit.collider.gameObject);
						}
						break;
					case "Icon" or "GroupIcon":
						hud.IconClick(hit.collider.gameObject);
						break;

				}
			}
		}

		else OnMouseExit();
		//print(mode);
		if (mode == Mode.placing)
		{
			building.transform.position = GetMousePosToWorldPoint();
			/*bool inRange = building.GetComponent<Building>().CheckGround(GetMousePosToWorldPoint()) == "";
			if (!inRange)
				building.GetComponent<SpriteRenderer>().color = Color.gray;
			else
				building.GetComponent<SpriteRenderer>().color = Color.white;*/
		}
		OnMouseLeftClick();
		OnMouseRightClick();
	}

	private void OnMouseLeftClick()
	{
		if (mouse.leftButton.wasPressedThisFrame)
		{
			if (mode == Mode.placing)
			{
				Building newBuilding = building.GetComponent<Building>();
				if (newBuilding.isColliding)
				{
					hud.ShowShortGUIText("Budynek nie mo¿e staæ na innym", time: 4000);
					return;
				}
				string checkResult = newBuilding.CheckItemsNeedToBuilding();
				if (checkResult != "")
				{
					hud.ShowShortGUIText(checkResult, time: 4000);
					return;
				}
				checkResult = newBuilding.CheckGround(GetMousePosToWorldPoint());
				if (checkResult != "")
				{
					hud.ShowShortGUIText(checkResult, time: 4000);
					return;
				}
				if (!newBuilding.CheckGroundIsEmpty(GetMousePosToWorldPoint()))
				{
					hud.ShowShortGUIText("Budynek nie mo¿e staæ na innym", time: 4000);
					return;
				}
				else
				{
					newBuilding.Build(GetMousePosToWorldPoint());
					if (newBuilding is ProductionBuilding productionBuilding)
					{
						AbstractVillager villager = FindUnemployedVillager();
						if (villager == null)
							hud.ShowShortGUIText("Budynek nie ma pracownika", time: 4000);
						else
							villager.AssignToBuilding(building.GetComponent<Building>());
					}

					mode = Mode.nothing;
					lastBuilding = newBuilding;
					hud.undo.color = new Color(1, 1, 1, 1);
				}

			}
			else
			{

			}
			/*else
			{
				tilemap.TileTypeInRange("grass8", GetMousePosToWorldPoint(), 2);
			}*/
		}
	}

	private void OnMouseRightClick()
	{
		if (mouse.rightButton.wasPressedThisFrame)
		{
			switch (mode)
			{
				case Mode.placing:
					Destroy(building);
					mode = Mode.nothing;
					break;
			}
			mode = hud.buildingController.OnMouseRightClick(mode) ? Mode.nothing : mode;
		}
	}

	private void OnMouseEnter(string type)
	{
		if (mode == Mode.nothing)
		{
			if (type == "Warrior")
			{
				if (units.Any(u => u.isChoosen))
				{
					SetCursor(pathToCursors + "/cursor_go_to");
				}
				else
				{
					SetCursor(pathToCursors + "/cursor_highlighted");
				}
			}
			if (type == "Enemy" && units.Any(u => u.isChoosen))
			{
				SetCursor(pathToCursors + "/sable_cursor2");
			}
		}

		/*if (mode == Mode.unit)
		{
			if (hit.collider.gameObject.GetType().ToString() == "Church")
			{
				SetCursor(pathToCursors + "/heal");
			}
		}*/
	}

	private void OnMouseExit()
	{
		/*if (units.Any(u => u.isChoosen))
		{
			SetCursor(pathToCursors + "/cursor_go_to");
		}
		else SetCursor(pathToCursors + "/cursor");*/
	}

	public bool MouseInRange(Vector2 mousePos, RaycastHit2D hit, float range)
	{
		return Math.Abs(hit.collider.transform.position.x - mousePos.x) < range &&
		 Math.Abs(hit.collider.transform.position.y - mousePos.y) < range;
	}

	public void SetCursor(string path)
	{
		Texture2D cursor = Resources.Load<Texture2D>(path);
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
	}

	private string WhatIsHit(string tag)
	{
		//print(tag);
		if (playerTags.Contains(tag))
			return "Warrior";
		if (enemyTags.Contains(tag))
			return "Enemy";
		if (tag == "EditorOnly")
			return "Save";
		if (tag == "Respawn")
			return "Load";
		if (tag == "Granary")
			return "Granary";
		if (tag == "Building")
			return "Building";
		if (tag == "Icon")
			return "Icon";
		if (tag == "Tree")
			return "Tree";
		if (tag == "Villager")
			return "Villager";
		return "";
	}

	public async void Save()
	{
		slu.SaveGame("n");
	}

	public async void Load()
	{
		slu.LoadGame("n");
		Start();
		hud.Start();
		//Instantiate(hud);
		Instantiate(mainCamera);
	}

	public async Task MakeMoney()
	{
		while (true)
		{
			items["Money"]++;
			await Task.Delay(300);
		}
	}

	public void PlaceBuilding(Building building)
	{
		//building = Instantiate(Resources.Load<GameObject>("Prefabs/Buildings/" + buildingName));
		//Vector3 pos = hit.collider.transform.position;
		building.transform.position = GetMousePosToWorldPoint();
		building.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		//building.name = building.buildingName;
		building.tag = "InBuild";
		building.gameObject.layer = LayerMask.NameToLayer("InBuild");
		this.building = Instantiate(building.gameObject);
		this.building.name = building.name;
		this.building.layer = LayerMask.NameToLayer("InBuild");
		mode = Mode.placing;
		//print(building);
		//building.gameObject.name = buildings.Count.ToString();
	}

	private AbstractVillager FindUnemployedVillager()
	{
		AbstractVillager villager = ic.inhabitants["Villager"].Find(u => u.workBuilding == null);
		if (villager == null)
			villager = ic.inhabitants["RichVillager"].Find(u => u.workBuilding == null);
		return villager;
	}

	private async Task MakeTree()
	{
		GameObject tree = Resources.Load<GameObject>("Prefabs/Outdoor/Tree");
		int trees = 2;
		float x, y;
		while (true)
		{
			GameObject newTree = Instantiate(tree);
			x = UnityEngine.Random.Range(-10, 20);
			y = UnityEngine.Random.Range(-10, 20);
			newTree.transform.position = new Vector3(x, y, 0);
			newTree.GetComponent<Tree>().id = trees++;
			await Task.Delay(100000);
		}

	}

	public void GameOver()
	{
		hud.ShowGameOverScreen();
		paused = true;
	}

	public Vector2 GetMousePosToWorldPoint()
	{
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
		Vector2 mousePos = new Vector2(mousePos3D.x, mousePos3D.y);
		return mousePos;
	}
	public Vector2 GetMousePos()
	{
		Vector3 mousePos3D = mouse.position.ReadValue();
		Vector2 mousePos = new Vector2(mousePos3D.x, mousePos3D.y);
		return mousePos;
	}
	public async Task Wait(int time)
	{
		await Task.Delay(time);
	}

	public void AddUnit(UnitModel unit)
	{
		units.Add(unit);
	}

	public void RemoveUnit(UnitModel unit)
	{
		units.Remove(unit);
	}

	public void AddWarrior(Warrior warrior)
	{
		warriors[warrior.GetType().ToString()].Add(warrior);
	}

	public void RemoveWarrior(Warrior warrior)
	{
		warriors[warrior.GetType().ToString()].Remove(warrior);
	}

	public void AddBuilding(Building building)
	{
		buildings[building.GetType().ToString()].Add(building);
	}

	public void RemoveBuilding(Building building)
	{
		buildings[building.GetType().ToString()].Remove(building);
	}
}
//354
//381