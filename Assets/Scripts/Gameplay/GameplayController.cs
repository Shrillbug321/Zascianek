using Assets.Scripts;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
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
		MakeMoney();
	}


	private void Awake()
	{
		if (gameplay != null && gameplay != this)
			Destroy(this);
		else
			gameplay = this;
	}

	public void AddUnit(UnitModel unit)
	{
		units.Add(unit);
	}

	public void RemoveUnit(UnitModel unit)
	{
		units.Remove(unit);
	}

	public void AddBuilding(Building building)
	{
		buildings.Add(building);
	}

	public void RemoveBuilding(Building building)
	{
		buildings.Remove(building);
	}

	void Update()
	{
		mousePos = GetMousePosToWorldPoint();
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f);
		if (hit.collider != null && MouseInRange(mousePos, hit, 0.75f))
		{
			string tag = hit.collider.gameObject.tag;
			//print(tag);
			//print(hit.collider.gameObject.name);
			switch (WhatIsHit(tag))
			{
				case "Warrior":
					OnMouseEnter("Warrior");
					break;
				case "Enemy":
					OnMouseEnter("Enemy");
					break;
				case "Save":
					if (mouse.leftButton.wasPressedThisFrame)
						Save();
					break;
				case "Load":
					if (mouse.leftButton.wasPressedThisFrame)
						Load();
					break;
				case "Granary":
					if (mouse.leftButton.wasPressedThisFrame)
					{

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
					}
					print("p");

					//units.Add(a);
					break;
				case "Building":
					if (mouse.leftButton.wasPressedThisFrame && !unitIsChoosen)
					//	print(hit.collider.gameObject.name);
					//hud.SendMessage("BuildingClick", "AppleField");
					{
						mode = Mode.building;
						hud.buildingController.BuildingClick(hit.collider.gameObject.GetComponent<Building>());
					}
					break;
				default:
					OnMouseExit();
					break;
			}
		}
		else OnMouseExit();
		//print(mode);
		if (mode == Mode.placing)
		{
			building.transform.position = GetMousePosToWorldPoint();
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
				if (building.GetComponent<Building>().isColliding)
				{
					hud.ShowShortGUIText("Budynek nie mo¿e staæ na innym", time: 4000);
				}
				else
				{
					building.transform.position = GetMousePosToWorldPoint();
					building.tag = "Building";
					building.GetComponent<Building>().color = "Green";
					building.layer = LayerMask.NameToLayer("Buildings");
					items["Wood"] -= 2;
					mode = Mode.nothing;

					AbstractVillager villager = (AbstractVillager)units.Find(u => u.tag == "Villager" && u.workBuilding == null);
					if (villager == null)
						hud.ShowShortGUIText("Budynek nie ma pracownika", time: 4000);
					else
						villager.AssignToBuilding(building.GetComponent<Building>());
				}

			}
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

	private void OnMouseExit()
	{
		if (units.Any(u => u.isChoosen))
		{
			SetCursor(pathToCursors + "/cursor_go_to");
		}
		else SetCursor(pathToCursors + "/cursor");
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
		return "";
	}

	public async void Save()
	{
		slu.SaveGame("n");
	}

	public async void Load()
	{
		slu.LoadGame("n");
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

	public void LoadBuilding(string buildingName)
	{
		building = Instantiate(Resources.Load<GameObject>("Prefabs/Buildings/" + buildingName));
		//Vector3 pos = hit.collider.transform.position;
		building.transform.position = GetMousePosToWorldPoint();
		building.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		building.name = buildingName;
		building.tag = "InBuild";
		mode = Mode.placing;
		//print(building);
		//building.gameObject.name = buildings.Count.ToString();
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

	public void OnCollisionStay(Collision col)
	{
		if (col.gameObject.tag == "Building")
			print("bgf");
	}
}
