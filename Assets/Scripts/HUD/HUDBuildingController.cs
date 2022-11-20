﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameplayControllerInitializer;
using static HUDController;

public class HUDBuildingController : HUDBuildingText, IPointerClickHandler, IPointerEnterHandler
{
	protected Vector3 groupPos;
	protected GameObject groupSelected;
	protected GameObject detailsBar;
	protected GameObject list;
	protected GameObject stocks;
	protected GameObject stockSelected;
	protected TextMeshProUGUI buildingText;
	protected bool inList;
	protected TextMeshProUGUI buildingName;
	protected TextMeshProUGUI buildingDP;
	protected TextMeshProUGUI productionProgress;
	protected TextMeshProUGUI buildingStatus;
	protected Image buildingItem;
	protected Image buildingClicked;

	public static HUDBuildingController hudBuilding;
	public void Start()
	{
		groupSelected = GameObject.Find("BuildingsFood");
		stockSelected = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "StockFood");
		detailsBar = GameObject.Find("DetailsBar");
		groupPos = groupSelected.transform.position;
		buildingText = GameObject.Find("BuildingText").GetComponent<TextMeshProUGUI>();
		buildingName = GameObject.Find("BuildingName").GetComponent<TextMeshProUGUI>();
		buildingDP = GameObject.Find("BuildingDP").GetComponent<TextMeshProUGUI>();
		buildingStatus = GameObject.Find("BuildingStatus").GetComponent<TextMeshProUGUI>();
		list = GameObject.Find("BuildingsList");
		buildingClicked = GameObject.Find("BuildingClicked").GetComponent<Image>();
		buildingItem = GameObject.Find("BuildingItem").GetComponent<Image>();
		productionProgress = GameObject.Find("ProductionProgress").GetComponent<TextMeshProUGUI>();
		stocks = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "StockBars");
		//detailsBar.SetActive(false);
	}

	private void Awake()
	{
		if (hudBuilding != null && hudBuilding != this)
			Destroy(this);
		else
		{
			hudBuilding = this;
			//buildingText.transform.parent = GameObject.Find("HUD").transform;
			//buildingText.rectTransform.position = new Vector3(475, 225, 0);
		}
	}

	public void ChangeGroup(string groupName)
	{
		//Destroy(groupSelected);
		groupSelected.SetActive(false);
		//groupSelected = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == groupName);
		groupSelected = list.transform.Find(groupName).gameObject;
		groupSelected.SetActive(true);
		//Image group = Instantiate(Resources.Load<Image>("Prefabs/HUD/Groups/" + groupName));

		/*group.transform.SetParent(GameObject.Find("BuildingsList").transform, false);
		group.rectTransform.position = groupPos;
		groupSelected = group.gameObject;*/
	}
	public void ChangeStock(string groupName)
	{
		list.SetActive(false);
		detailsBar.SetActive(false);
		/*stocks = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "StockBars");
		stocks.SetActive(true);*/
		stocks.SetActive(true);
		Debug.LogWarning(groupName);
		//Destroy(stockSelected);
		/*stockSelected.SetActive(false);
		stockSelected = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == groupName);
		stockSelected.SetActive(true);*/
		stockSelected.SetActive(false);
		stockSelected = stocks.transform.transform.Find(groupName).gameObject;
		stockSelected.SetActive(true);
		/*foreach (GameObject item in stocks.transform)
		{
		item.transform.Find(groupName).gameObject.SetActive(true);

			//if (item.GetComponent =
		}*/

		/*Image group = Instantiate(Resources.Load<Image>("Prefabs/HUD/Stocks/" + groupName));

		group.transform.SetParent(GameObject.Find("StockBars").transform, false);
		group.rectTransform.position = groupPos;
		stockSelected = group.gameObject;*/
	}
	public void Update()
	{
		if (hud.lastEntered == "BuildingsList")
		{
			Transform child = groupSelected.transform;
			Transform enteredBuilding = null;
			Vector2 mousePos = gameplay.GetMousePos();
			foreach (Transform b in child.transform)
			{
				if (b.transform.position.x > mousePos.x - 50)
				{
					enteredBuilding = b;
					break;
				}
			}
			switch (enteredBuilding?.gameObject.name)
			{
				case "AppleField":
					ShowBuildingText("2 drewna");
					break;
				case "Pigsty":
					ShowBuildingText("10 drewna");
					break;
			}
			//print(enteredBuilding.gameObject.name);
		}
	}

	public bool OnMouseRightClick(Mode mode)
	{
		switch (mode)
		{
			case Mode.building:
				list.SetActive(true);
				stocks.SetActive(false);
				return true;
		}
		return false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//lastEntered = eventData.pointerCurrentRaycast.gameObject.name;
		/*GraphicRaycaster raycaster = GameObject.Find("HUD").GetComponent<GraphicRaycaster>();
		List<RaycastResult> results = new List<RaycastResult>();
		List<RaycastResult> results2 = new List<RaycastResult>();
		raycaster.Raycast(eventData, results);
		foreach (RaycastResult result in results)
		{
  print(result);
		}*/
		switch (eventData.pointerCurrentRaycast.gameObject.name)
		{
			case "BuildingsList":
				/*print(results[0].screenPosition);
				print(results[0].worldPosition);
				results2 = new List<RaycastResult>();
				raycaster.Raycast(results[0]., results2);
				foreach (RaycastResult result in results2)
				{
					print(result);
				}*/
				inList = true;
				GameObject a = eventData.pointerCurrentRaycast.gameObject;
				Transform child = eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0);
				Transform enteredBuilding = null;
				Vector2 mousePos = gameplay.GetMousePos();
				foreach (Transform b in child.transform)
				{
					if (b.transform.position.x > mousePos.x - 50)
					{
						enteredBuilding = b;
						break;
					}
				}

				break;
			case "AppleField":
				ShowBuildingText("2 drewna");
				break;
			case "Pigsty":
				ShowBuildingText("10 drewna");
				break;
		}
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		string name = eventData.pointerCurrentRaycast.gameObject.name;
		string tag = eventData.pointerCurrentRaycast.gameObject.tag;
		Debug.LogWarning(name);
		GroupClick(name);
		if (tag == "Icon")
			BuildingIconClick(name);
		//if (tag == "Building")
		//BuildingClick(name);
	}

	public async Task ShowBuildingText(string text, int time = int.MaxValue)
	{
		//TextMeshProUGUI guiText = Instantiate(Resources.Load<TextMeshProUGUI>("Prefabs/HUD/Text"));
		//GUILayout.Label(text);
		//Text guiText = go.AddComponent<Text>();
		buildingText.text = text;
		print(text);

		if (time == int.MaxValue)
			hud.lastText = buildingText;
		else
		{
			await Task.Delay(time);
			buildingText.text = "";
			//Destroy(buildingText);
		}
	}

	private void GroupClick(string click)
	{
		switch (click)
		{
			case "Apple":
				ChangeGroup("BuildingsFood");
				break;
			case "Industry":
				ChangeGroup("BuildingsIndustry");
				break;
			case "Feedstock":
				ChangeGroup("BuildingsFeedstock");
				break;
			case "House":
				ChangeGroup("BuildingsHouses");
				break;
			case "Army":
				ChangeGroup("BuildingsArmy");
				break;
			case "Stock":
				ChangeGroup("BuildingsStock");
				break;
			case "Other":
				ChangeGroup("BuildingsOther");
				break;
		}
	}

	private void BuildingIconClick(string name)
	{
		string groupName = groupSelected.name.Substring(groupSelected.name.IndexOf("Buildings")+ "Buildings".Length);
		Building building = Resources.Load<GameObject>("Prefabs/Buildings/" + groupName + "/" + name).GetComponent<Building>();

		Debug.ClearDeveloperConsole();
		//Debug.LogWarning(building.GetType());
		string result = building.CheckItemsNeedToBuilding();
		if (result == "")
			gameplay.PlaceBuilding(building);
		else
		{
			ShowBuildingText(result, 5000);
		}
		/*switch (name)
		{
			case "AppleField":
				if (gameplay.items["Wood"] < 2)
					ShowBuildingText("Za mało drewna!", 5000);
				else
					gameplay.LoadBuilding("AppleField");
				break;
		}*/
	}

	public void BuildingClick(Building building)
	{
		string name = building.name;
		print("zxcvbn");
		list.SetActive(false);
		Debug.LogWarning(name);
		switch (building)
		{
			case StockBuilding:
				if (building.name == "Granary")
					ChangeStock("StockFood");
				if (building.name == "Magazine")
					ChangeStock("StockMagazine");
				if (building.name == "Armory")
					ChangeStock("StockArmory");
				refreshStockCount(stockSelected.transform);
				break;
			case ProductionBuilding a:
		detailsBar.SetActive(true);
				buildingName.text = buildingsNames[name];
				buildingDP.text = building.dp.ToString() + "/" + building.maxDp.ToString();
				//refreshStatus(name, building.status.ToString());
				//buildingStatus.text = statuses[name][building.status.ToString()];
				refreshProgress(a);
				buildingClicked.sprite = Resources.Load<Sprite>("Sprites/Buildings/" + buildingsImages[name]);
				buildingItem.sprite = Resources.Load<Sprite>("HUD/Icons/Items/" + itemInBuilding[name]);
				Debug.LogWarning("Sprites/Buildings/" + buildingsImages[name]);
				Debug.LogWarning("HUD/Icons/Items/" + itemInBuilding[name]);
				break;
		}
	}

	private async Task refreshProgress(ProductionBuilding building)
	{
		while (true)
		{
			productionProgress.text = building.productionProgress.ToString() + "%";
			await Task.Delay(100);
		}
	}
	private async Task refreshStatus(string name, string status)
	{
		while (true)
		{
			buildingStatus.text = statuses[name][status];
			await Task.Delay(100);
		}
	}
	private async Task refreshStockCount(Transform items)
	{
		while (true)
		{
			foreach (Transform item in items)
			{
				item.transform.Find("Count").gameObject.GetComponent<TextMeshProUGUI>().text = gameplay.items[item.name].ToString();
			}
			await Task.Delay(100);
		}
	}
}
