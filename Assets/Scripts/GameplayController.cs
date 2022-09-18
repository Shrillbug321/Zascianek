using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayController : MonoBehaviour
{
	protected string Type;
	public readonly string[] PlayerTags = { "PlayerWarrior", "PlayerInfrantry", "PlayerCrossbower", "PlayerHeavyInfrantry" };
	public readonly string[] EnemyTags = { "Enemy", "EnemyInfrantry", "EnemyAxer", "EnemyBower" };
	public static GameplayController Instance;
	public Camera Camera;
	public	GameObject mainCamera;
	public List<UnitModel> units = new List<UnitModel>();

	SaveLoadUtility slu;
	public void Start()
	{
		slu = gameObject.AddComponent<SaveLoadUtility>();
		mainCamera = Resources.Load<GameObject>("Prefabs/Common/MainCamera");
		Instantiate(mainCamera);
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
				SetCursor("Assets/HUD/cursor_go_to.png");
			}
			else
			{
				SetCursor("Assets/HUD/cursor_highlighted.png");
			}
		}
		if (type == "Enemy" && units.Any(u => u.IsChoosen))
		{
			SetCursor("Assets/HUD/sable_cursor2.png");
		}
	}

	private void OnMouseExit()
	{
		if (units.Any(u => u.IsChoosen))
		{
			SetCursor("Assets/HUD/cursor_go_to.png");
		}
		else SetCursor("Assets/HUD/cursor.png");
	}

	public bool MouseInRange(Vector2 mousePos, RaycastHit2D hit, float range)
	{
		return Math.Abs(hit.collider.transform.position.x - mousePos.x) < range &&
		 Math.Abs(hit.collider.transform.position.y - mousePos.y) < range;
	}

	public void SetCursor(string path)
	{
		Texture2D cursor = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
	}

	private string WhatIsHit(string tag)
	{
		if (PlayerTags.Contains(tag))
			return "Warrior";
		if (EnemyTags.Contains(tag))
			return "Enemy";
		if (tag=="EditorOnly")
			return "Save";
		if (tag=="Respawn")
			return "Load";
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
}
