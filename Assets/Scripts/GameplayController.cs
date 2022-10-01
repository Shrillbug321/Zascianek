using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameplayController : GameplayControllerInitializer
{
	public override void Start()
	{
		base.Start();
		slu = gameObject.AddComponent<SaveLoadUtility>();
		mainCamera = Resources.Load<GameObject>("Prefabs/Common/MainCamera");
		Instantiate(mainCamera);
		MakeMoney();
	}


	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;
	}

	public void AddUnit(UnitModel unit)
	{
		units.Add(unit);
	}

	public void RemoveUnit(UnitModel unit)
	{
		units.Remove(unit);
	}

	void Update()
	{
		Mouse mouse = Mouse.current;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
		Vector2 mousePos = new Vector2(mousePos3D.x, mousePos3D.y);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f);
		if (hit.collider != null && MouseInRange(mousePos, hit, 0.5f))
		{
			string tag = hit.collider.gameObject.tag;
			print(tag);
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

						if (Items["money"] < 15)
						{
							//ShowGUIImage("Prefabs/HUD/Image");
							HUDController.hud.ShowGUIText("Potrzeba 15 monet!");
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
				default:
					OnMouseExit();
					break;
			}
		}
		else OnMouseExit();
	}

	private void OnMouseEnter(string type)
	{
		if (type == "Warrior")
		{
			if (units.Any(u => u.IsChoosen))
			{
				SetCursor(pathToCursors + "/cursor_go_to");
			}
			else
			{
				SetCursor(pathToCursors + "/cursor_highlighted");
			}
		}
		if (type == "Enemy" && units.Any(u => u.IsChoosen))
		{
			SetCursor(pathToCursors + "/sable_cursor2");
		}
	}

	private void OnMouseExit()
	{
		if (units.Any(u => u.IsChoosen))
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
		if (PlayerTags.Contains(tag))
			return "Warrior";
		if (EnemyTags.Contains(tag))
			return "Enemy";
		if (tag == "EditorOnly")
			return "Save";
		if (tag == "Respawn")
			return "Load";
		if (tag == "Granary")
			return "Granary";
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
			Items["money"]++;
			print("OPOP");
			await Task.Delay(2000);
		}
	}

	/*public void OnGUI()
	{

		GUILayout.Label("Kanohi and Skills");
	}*/
}
