using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MouseController;

public class GameplayController : GameplayControllerInitializer
{
	private bool leftClicked;
	public override void Start()
	{
		base.Start();
		if (SceneController.buttonClicked == "LoadGame")
		{
			SceneController.buttonClicked = "";
			saveGameController.Load();
		}
		mouse = MouseController.mouse;
		Instantiate(Resources.Load<GameObject>("Prefabs/Common/MainCamera"));
		mainCamera = GameObject.Find("MainCamera(Clone)");
		Instantiate(Resources.Load<GameObject>("Prefabs/Common/MinimapCamera"));
		minimapCamera = GameObject.Find("MinimapCamera(Clone)");
		cameraController = mainCamera.GetComponent<CameraController>();
		roadSign = GameObject.Find("RoadSign");
		startPath = GameObject.Find("StartPath").transform.position;
		endPath = GameObject.Find("EndPath").transform.position;
		
		if (SceneController.buttonClicked == "NewGamel")
			NewGame();
		else
			settlerHousePos = GameObject.Find("HouseSettler").transform.position;
	}
	
	private void Awake()
	{
		if (gameplay != null && gameplay != this)
			Destroy(this);
		else
			gameplay = this;
	}

	private void Update()
	{
		leftClicked = false;
		
		if (isGameOver)
			musicController.ChangeState("GameOver");
		
		if (isGameOver && mouse.leftButton.wasPressedThisFrame)
			SceneManager.LoadScene("MainMenu");
		
		if (paused) return;

		if (attack && enemies.Count < 2)
		{
			musicController.ChangeState("Ambient");
			attack = false;
		}

		if (!attack && enemies.Count > 1)
		{
			attack = true;
			musicController.ChangeState("Battle");
		}

		ic.Update();
		mousePos = GetMousePos();
		mousePosInWorld = GetMousePosToWorldPoint();

		actualMonthPassed += Time.deltaTime;
		if (actualMonthPassed > MONTH_DURATION)
		{
			actualMonthPassed = 0;
			if (++month > 12)
			{
				month = 1;
				year++;
			}
			if (month == 9)
			{
				if (buildings["Church"].Count > 0)
					((Church)buildings["Church"][0]).GetTax();
			}
			hud.date.text = $"{hud.monthNames[gameplay.month]} A.D. {gameplay.year}";
		}

		foreach (KeyValuePair<string, List<Building>> slot in buildings)
		{
			if (slot.Value.Count > 0)
			{
				foreach (Building building in slot.Value)
				{
					if (building is ProductionBuilding && building.worker == null)
					{
						AbstractVillager villager = FindUnemployedVillager();
						if (villager != null)
							villager.AssignToBuilding(building);
					}
				}
			}
		}
		
		if (mode == Mode.placing)
		{
			building.transform.position = GetMousePosToWorldPoint();
			/*bool inRange = building.GetComponent<Building>().CheckGround(GetMousePosToWorldPoint()) == "";
			if (!inRange)
				building.GetComponent<SpriteRenderer>().color = Color.gray;
			else
				building.GetComponent<SpriteRenderer>().color = Color.white;*/
		}
		
		if (leftClicked) return;
		
		OnMouseRightClick();
		
		RaycastHit2D hit = Physics2D.Raycast(mousePosInWorld, Vector2.zero, 1f);
		if (hit.collider != null && MouseInRange(mousePosInWorld, hit, 0.75f))
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
						if (hit.collider.gameObject.name == "Church")
							SetCursor(pathToCursors + "/heal");
					break;
				default:
					//OnMouseExit();
					break;
			}
			if (mouse.leftButton.wasPressedThisFrame)
			{
				if (mousePos.y > 270)
				{
					switch (WhatIsHit(tag))
					{
						case "Warrior" or "Villager" or "Enemy":
							mode = Mode.unit;
							hud.SwitchUnitBar(hit.collider.gameObject.GetComponent<UnitModel>());
							break;
						case "Save":
							saveGameController.Save();
							break;
						case "Load":
							saveGameController.Load();
							break;
						case "Building":
							if (mode != Mode.repair && mode != Mode.destroy)
							{
								mode = Mode.building;
								clickedBuilding = hit.collider.gameObject.GetComponent<Building>();
								hud.buildingController.BuildingClick(hit.collider.gameObject.GetComponent<Building>());
							}
							
							if (mode == Mode.repair)
							{
								Building building = hit.collider.GetComponent<Building>();
								building.Repair();

							}
							if (mode == Mode.destroy)
							{
								Building building = hit.collider.GetComponent<Building>();
								if (building.needToBuild != null)
									foreach (KeyValuePair<string, int> item in building.needToBuild)
										gameplay.items[item.Key] += (int)Math.Round(item.Value * 0.9f);
								if (building is ProductionBuilding pb)
									pb.worker.RemoveFromBuilding();
								RemoveBuilding(building);
								Destroy(building.gameObject);
							}
							break;
						case "Tree":
							if (mode == Mode.cut)
								Destroy(hit.collider.gameObject);
							break;
						/*case "Granary": //test building
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
							break;*/
					}
				}
				else
				{
					switch (WhatIsHit(tag))
					{
						case "Icon" or "GroupIcon":
							hud.IconClick(hit.collider.gameObject);
							break;
					}
				}
			}
		}
		//else OnMouseExit();
		OnMouseLeftClick();
	}

	private async void NewGame()
	{
		SceneController.buttonClicked = "";
		string[] buildings = { "Houses/HouseSettler", "Stock/Granary", "Stock/Magazine" };
		string[] texts = { "Wybierz siedzibę założyciela", "Wybierz miejsce na spichlerz", "Wybierz miejsce na skład" };
		UnitModel settler = Instantiate(Resources.Load<UnitModel>("Prefabs/Units/Warriors/Player/Settler"));
		foreach (string b in buildings)
		{
			Building building = Resources.Load<GameObject>("Prefabs/Buildings/" + b).GetComponent<Building>();
			building.Start();
			PlaceBuilding(building);
			//hud.buildingController.ShowBuildingText(texts[i], 5000);
			while (mode == Mode.placing) await Wait(1000);
		}
		settlerHousePos = GameObject.Find("HouseSettler").transform.position;
		settler.transform.position = settlerHousePos;
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
					hud.ShowShortGUIText("Budynek nie może stać na innym", time: 4000);
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
					hud.ShowShortGUIText("Budynek nie może stać na innym", time: 4000);
					return;
				}
				
				newBuilding.Build(GetMousePosToWorldPoint());
				if (newBuilding is ProductionBuilding)
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
			leftClicked = true;
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
				if (mode == Mode.unit)
					SetCursor(pathToCursors + "/cursor_go_to");
				else
					SetCursor(pathToCursors + "/cursor_highlighted");
			}
		}
		if (type == "Enemy" && mode == Mode.unit)
		{
			SetCursor(pathToCursors + "/sable_cursor2");
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
		if (mode == Mode.unit)
			SetCursor(pathToCursors + "/cursor_go_to");
		
		if (mode == Mode.nothing)
			SetCursor(pathToCursors + "/cursor");
	}

	public bool MouseInRange(Vector2 mousePosInWorld, RaycastHit2D hit, float range)
	{
		return Math.Abs(hit.collider.transform.position.x - mousePosInWorld.x) < range &&
		 Math.Abs(hit.collider.transform.position.y - mousePosInWorld.y) < range;
	}

	public void SetCursor(string path)
	{
		Texture2D cursor = Resources.Load<Texture2D>(path);
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
	}

	private string WhatIsHit(string tag)
	{
		if (playerTags.Contains(tag))
			return "Warrior";
		if (enemyTags.Contains(tag))
			return "Enemy";
		return tag switch
		{
			"EditorOnly" => "Save",
			"Respawn" => "Load",
			"Granary" => "Granary",
			"Building" => "Building",
			"Icon" => "Icon",
			"Tree" => "Tree",
			"Villager" => "Villager",
			_ => ""
		};
	}

	/*public async void Save()
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
	}*/

	public void PlaceBuilding(Building building)
	{
		building.transform.position = GetMousePosToWorldPoint();
		building.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		building.tag = "InBuild";
		building.gameObject.layer = LayerMask.NameToLayer("InBuild");
		this.building = Instantiate(building.gameObject);
		this.building.name = building.name;
		this.building.layer = LayerMask.NameToLayer("InBuild");
		mode = Mode.placing;
	}

	public AbstractVillager FindUnemployedVillager()
	{
		AbstractVillager villager = ic.inhabitants["Villager"].Find(u => !u.employed);
		if (villager == null)
			villager = ic.inhabitants["RichVillager"].Find(u => !u.employed);
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
		isGameOver = true;
	}

	public async Task Wait(int time)
	{
		await Task.Delay(time);
	}

	public void AddUnit(UnitModel unit)
	{
		units[unit.GetType().ToString()].Add(unit);
	}

	public void RemoveUnit(UnitModel unit)
	{
		units[unit.GetType().ToString()].Remove(unit);
	}

	public void AddEnemy(Enemy enemy)
	{
		enemies.Add(enemy);
	}

	public void RemoveEnemy(Enemy enemy)
	{
		enemies.Remove(enemy);
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