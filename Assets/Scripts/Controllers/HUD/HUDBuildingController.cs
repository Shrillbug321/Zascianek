﻿using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameplayControllerInitializer;

public class HUDBuildingController : MonoBehaviour, IPointerEnterHandler
{
	protected Vector3 groupPos;
	protected GameObject groupSelected;
	protected GameObject detailsBar;
	public GameObject list;
	protected GameObject stocks;
	protected GameObject stockSelected;
	protected GameObject houseBar;
	protected GameObject barracks;
	protected GameObject market;
	protected GameObject taxBar;
	protected TextMeshProUGUI buildingText;
	protected bool inList;
	protected TextMeshProUGUI buildingName;
	protected TextMeshProUGUI buildingDP;
	protected TextMeshProUGUI productionProgress;
	protected TextMeshProUGUI buildingStatus;
	protected Image buildingItem;
	protected Image buildingClicked;
	private const int ITEM_WIDTH = 265;
	private const int ITEM_HEIGHT = 265;
	private string status, lastStatus, lastBuildingName;
	private bool buildingChanged;
	private CancellationTokenSource refreshTokenSource;
	private CancellationToken refreshToken;

	public static HUDBuildingController hudBuilding;
	public void Start()
	{
		groupSelected = GameObject.Find("BuildingsFood");
		stockSelected = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "StockFood");
		detailsBar = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "DetailsBar");
		groupPos = groupSelected.transform.position;
		buildingText = ((GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "BuildingText")).GetComponent<TextMeshProUGUI>();
		buildingName = GameObject.Find("BuildingName").GetComponent<TextMeshProUGUI>();
		//buildingName = Resources.FindObjectsOfTypeAll<GameObject>().First(go => go.name == "BuildingName").GetComponent<TextMeshProUGUI>();
		buildingDP = GameObject.Find("BuildingDP").GetComponent<TextMeshProUGUI>();
		buildingStatus = ((GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "BuildingStatus")).GetComponent<TextMeshProUGUI>();
		list = GameObject.Find("BuildingsList");
		buildingClicked = GameObject.Find("BuildingClicked").GetComponent<Image>();
		buildingItem = ((GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "BuildingItem")).GetComponent<Image>();
		productionProgress = ((GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "ProductionProgress")).GetComponent<TextMeshProUGUI>();
		stocks = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "StockBars");
		houseBar = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "HouseBar");
		barracks = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "BarracksBar");
		market = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "MarketBar");
		taxBar = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "TaxBar");
		refreshTokenSource = new();
		refreshToken = refreshTokenSource.Token;
		stocks.SetActive(false);
	}

	private void Awake()
	{
		if (hudBuilding != null && hudBuilding != this)
			Destroy(this);
		else
		{
			hudBuilding = this;
		}
	}

	public void ChangeGroup(string groupName)
	{
		groupSelected.SetActive(false);
		groupSelected = list.transform.Find(groupName).gameObject;
		groupSelected.SetActive(true);
	}
	public void ChangeStock(string groupName)
	{
		list.SetActive(false);
		detailsBar.SetActive(false);
		stocks.SetActive(true);
		Debug.LogWarning(groupName);
		stockSelected.SetActive(false);
		stockSelected = stocks.transform.transform.Find(groupName).gameObject;
		stockSelected.SetActive(true);
	}
	public void Update()
	{
		if (hud.lastEntered == "BuildingsList")
		{
			Transform child = groupSelected.transform;
			Transform enteredBuilding = null;
			Vector2 mousePos = MouseController.GetMousePos();
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
		}
		if (gameplay.mode == Mode.building && gameplay.clickedBuilding is ProductionBuilding)
		{
			status = gameplay.clickedBuilding.status.ToString();
			if (lastStatus != status)
			{
				lastStatus = status;
				buildingStatus.text = Texts.statuses[gameplay.clickedBuilding.name][status];
			}
		}
	}

	public bool OnMouseRightClick(Mode mode)
	{
		switch (mode)
		{
			case Mode.unit:
				list.SetActive(true);
				hud.unitBar.SetActive(false);
				return true;
			case Mode.building:
				list.SetActive(true);
				stocks.SetActive(false);
				refreshTokenSource.Cancel();
				refreshTokenSource.Dispose();
				refreshTokenSource = new CancellationTokenSource();
				refreshToken = refreshTokenSource.Token;
				return true;
			case Mode.house:
				List<Object> clones = FindObjectsOfType(typeof(GameObject)).Where(go => go.name == "Inhabitan(Clone)").ToList();
				foreach (Object item in clones)
					Destroy(item);
				list.SetActive(true);
				houseBar.SetActive(false);
				return true;
			case Mode.tax:
				list.SetActive(true);
				taxBar.SetActive(false);
				return true;
			case Mode.recruit:
				list.SetActive(true);
				barracks.SetActive(false);
				return true;
			case Mode.market:
				if (MouseController.GetMousePos().y > market.transform.localPosition.y)
				{
					list.SetActive(true);
					market.SetActive(false);
					return true;
				}
				break;
			case Mode.repair:
			case Mode.destroy:
			case Mode.cut:
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
				Vector2 mousePos = MouseController.GetMousePos();
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
	public void IconClick(GameObject gameObject)
	{
		string name = gameObject.name;
		string tag = gameObject.tag;
		GroupClick(name);
		if (tag == "Icon")
		{
			switch (gameplay.mode)
			{
				case Mode.nothing:
					BuildingIconClick(name);
					break;
				case Mode.building:
					ItemClick(name);
					break;
				case Mode.tax:
					UpdateTaxBar(name);
					break;
				case Mode.recruit:
					Recruit(name);
					break;
				case Mode.market:
					MarketIconClick(name);
					break;
			}
		}
	}

	public async Task ShowBuildingText(string text, int time = int.MaxValue)
	{
		buildingText.text = text;

		if (time == int.MaxValue)
			hud.lastText = buildingText;
		else
		{
			await Task.Delay(time);
			buildingText.text = "";
		}
	}

	public void GroupClick(string click)
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
		string groupName = groupSelected.name.Substring(groupSelected.name.IndexOf("Buildings") + "Buildings".Length);
		Building building = Resources.Load<GameObject>("Prefabs/Buildings/" + groupName + "/" + name).GetComponent<Building>();
		building.Start();
		Debug.ClearDeveloperConsole();
		string hasItems = building.CheckItemsNeedToBuilding();
		string stockWasBuilded = building.CheckStockBuildingWasBuilded();
		if (hasItems == "" && stockWasBuilded == "")
			gameplay.PlaceBuilding(building);
		else
		{
			if (hasItems != "")
				ShowBuildingText(hasItems, 5000);
			if (stockWasBuilded != "")
				ShowBuildingText(Texts.buildingsNames[stockWasBuilded] + " musi być wybudowany!", 5000);
		}
	}

	public void BuildingClick(Building building)
	{
		string name = building.name;
		list.SetActive(false);
		if (lastBuildingName != name)
		{
			buildingChanged = true;
			lastBuildingName = name;
		}
		else

			buildingChanged = false;
		switch (building)
		{
			case ProductionBuilding a:
				//OnMouseRightClick(gameplay.mode);
				refreshToken = refreshTokenSource.Token;
				detailsBar.SetActive(true);
				buildingItem.gameObject.SetActive(true);
				buildingStatus.gameObject.SetActive(true);
				productionProgress.gameObject.SetActive(true);
				buildingName.gameObject.SetActive(true);
				buildingDP.gameObject.SetActive(true);
				buildingClicked.gameObject.SetActive(true);


				buildingName.text = Texts.buildingsNames[name];
				buildingDP.text = building.dp.ToString() + "/" + building.maxDp.ToString();
				refreshProgress(a, refreshToken);
				//refreshStatus(a.name, a.status.ToString());
				buildingClicked.sprite = Resources.Load<Sprite>("Sprites/Buildings/" + name.ToSnakeCase());
				buildingItem.sprite = Resources.Load<Sprite>("HUD/Icons/Items/" + Texts.itemInBuilding[name]);
				//buildingClicked.SetNativeSize();
				if (building.stopped)
				{
					lastStatus = status = "stopped";
					buildingStatus.text = Texts.other["stopped"];
					buildingItem.color = new Color(1, 1, 1, 0.5f);
				}
				else
				{
					lastStatus = status = a.status.ToString();
					buildingStatus.text = Texts.statuses[name][lastStatus];
					buildingItem.color = new Color(1, 1, 1, 1f);
				}

				buildingItem.SetNativeSize();
				SetSize(buildingItem);
				SetSize(buildingClicked);
				gameplay.mode = Mode.building;
				Debug.LogWarning("Sprites/Buildings/" + name.ToSnakeCase());
				Debug.LogWarning("HUD/Icons/Items/" + Texts.itemInBuilding[name]);
				break;
			case StockBuilding:
				if (building.name == "Granary")
					ChangeStock("StockFood");
				if (building.name == "Magazine")
					ChangeStock("StockMagazine");
				if (building.name == "Armory")
					ChangeStock("StockArmory");
				refreshStockCount(stockSelected.transform);
				break;
			case Barracks:
				list.SetActive(false);
				barracks.SetActive(true);
				gameplay.mode = Mode.recruit;
				refreshBarrackCount(barracks.transform);
				break;
			case HouseSettler:
				list.SetActive(false);
				taxBar.SetActive(true);
				UpdateTaxBar(gameplay.ic.taxLevel.ToString());
				gameplay.mode = Mode.tax;
				GameObject.Find("TaxBuildingDP").GetComponent<TextMeshProUGUI>().text = building.dp.ToString() + "/" + building.maxDp.ToString();
				break;
			case AbstractHouse house:
				list.SetActive(false);
				detailsBar.SetActive(true);

				buildingName.text = Texts.buildingsNames[name];
				buildingDP.text = building.dp.ToString() + "/" + building.maxDp.ToString();
				buildingClicked.sprite = Resources.Load<Sprite>("Sprites/Buildings/" + name.ToSnakeCase());
				buildingItem.gameObject.SetActive(false);
				//buildingStatus.gameObject.SetActive(false);
				productionProgress.gameObject.SetActive(false);
				buildingStatus.text = house.inhabitans + "/" + house.maxInhabitans;

				GameObject inhabitan = GameObject.Find("Inhabitan");
				GameObject inhabitans = GameObject.Find("Inhabitans");
				for (int i = 0; i < house.inhabitans; i++)
				{
					GameObject newInhabitan = Instantiate(inhabitan);
					Image image = newInhabitan.GetComponent<Image>();
					image.sprite = Resources.Load<Sprite>("Sprites/Units/" + house.inhabitantType.ToSnakeCase());
					image.SetNativeSize();
					image.color = Color.white;
					newInhabitan.transform.parent = inhabitans.transform;
					newInhabitan.transform.localScale = new Vector3(1f, 1f, 1f);
					newInhabitan.transform.localPosition = new Vector2(110 * i - 450, -50);
					//image.color = Color.black;
				}
				gameplay.mode = Mode.house;
				break;
			case Market:
				list.SetActive(false);
				market.SetActive(true);
				gameplay.mode = Mode.market;
				break;
			case Church:
				detailsBar.SetActive(true);
				buildingItem.gameObject.SetActive(true);
				productionProgress.gameObject.SetActive(false);
				buildingName.text = Texts.buildingsNames[name];
				buildingDP.text = building.dp.ToString() + "/" + building.maxDp.ToString();
				buildingClicked.sprite = Resources.Load<Sprite>("Sprites/Buildings/" + name.ToSnakeCase());
				buildingItem.sprite = Resources.Load<Sprite>("Sprites/Units/priest");
				buildingItem.SetNativeSize();
				buildingStatus.text = "Księży: " + Church.priests;
				break;
		}
	}

	private void Recruit(string unitName)
	{
		bool hasItems = true;
		AbstractVillager villager = gameplay.FindUnemployedVillager();
		if (villager == null)
		{ ShowBuildingText("Brak ludzi", 5000); return; }
		villager.goToBarracks = true;
		villager.movement = GameObject.Find("Barracks").transform.position;
		villager.StartMove();
		GameObject.Find("Barracks").GetComponent<Barracks>().unitTypes.Add(unitName);
		//GameObject.Find("Barracks").GetComponent<Barracks>().unitIds.Add(villager.unitId);
		/*foreach (var item in gameplay.unitController.needToRecruits[unitName])
		{
			if (gameplay.items[item.Key] == 0)
			{
				hasItems = false;
				break;
			}
		}

		if (hasItems)
		{
			UnitModel warrior = Instantiate(Resources.Load<UnitModel>("Prefabs/Units/Warriors/Player/" + unitName));
			Vector3 pos = GameObject.Find("Barracks").transform.position;
			warrior.transform.position = new Vector3(pos.x, pos.y - 1f, 0);

			foreach (var item in gameplay.unitController.needToRecruits[unitName])
				gameplay.items[item.Key] -= item.Value;
		}
		else
		{
			ShowBuildingText("Brak wymaganych przedmiotów", 5000);
		}*/
	}

	public void MarketIconClick(string item)
	{
		if (gameplay.mouse.leftButton.wasReleasedThisFrame)
			Market.Buy(item);

		if (gameplay.mouse.rightButton.wasReleasedThisFrame)
			Market.Sell(item);
	}

	private void UpdateTaxBar(string level)
	{
		Dictionary<string, int> taxes = gameplay.ic.ChangeTax(int.Parse(level));
		foreach (KeyValuePair<string, int> tax in taxes)
			GameObject.Find("Tax" + tax.Key).GetComponent<TextMeshProUGUI>().text = tax.Value.ToString();
		GameObject.Find("TaxHappiness").GetComponent<TextMeshProUGUI>().text = gameplay.ic.CalculateTaxSatisfaction().ToString();
	}

	private void ItemClick(string item)
	{
		if (Texts.buildingsNames.First(n => n.Value == buildingName.text).Key == "Church")
		{
			AbstractVillager priest = Instantiate(Resources.Load<AbstractVillager>("Prefabs/Units/Villagers/Priest"));
			Vector3 pos = GameObject.Find("Church").transform.position;
			priest.transform.position = new Vector3(pos.x - 2.5f, pos.y - 3f, 0);
			List<AbstractVillager> villagersList = gameplay.ic.inhabitants["Priest"];
			villagersList.Add(priest);
			Church.priests++;
		}
		if (gameplay.clickedBuilding is ProductionBuilding pb)
		{
			if (pb.stopped)
			{
				AbstractVillager villager = gameplay.FindUnemployedVillager();
				if (villager.workBuildingId == pb.id)
					pb.worker.BuildingResumed();
				else
					villager.AssignToBuilding(pb);
				pb.stopping = false;
				pb.stopped = false;
				buildingItem.color = new Color(1, 1, 1, 1f);
			}
			else
			{
				pb.worker.BuildingStopped();
				pb.stopped = true;
				buildingItem.color = new Color(1, 1, 1, 0.5f);
				//pb.stopping = true;
			}
		}
	}

	private void SetSize(Image image)
	{
		Vector2 size = image.rectTransform.sizeDelta;
		if (size.x > size.y)
		{
			float multiplier = ITEM_WIDTH / size.x * 0.6f;
			image.rectTransform.localScale = new Vector3(multiplier, multiplier, multiplier);
		}
		if (size.x < size.y)
		{
			float multiplier = ITEM_HEIGHT / size.y * 0.4f;
			image.rectTransform.localScale = new Vector3(multiplier, multiplier, multiplier);
		}
	}

	private async Task refreshProgress(ProductionBuilding building, CancellationToken token)
	{
		while (!token.IsCancellationRequested)
		{
			productionProgress.text = building.productionProgress.ToString() + "%";
			await Task.Delay(100);
		}
	}
	private async Task refreshStatus(string name, string status)
	{
		while (true)
		{
			buildingStatus.text = Texts.statuses[name][status];
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
	private async Task refreshBarrackCount(Transform items)
	{
		while (true)
		{
			foreach (Transform item in items.transform)
			{
				item.transform.Find("Count").gameObject.GetComponent<TextMeshProUGUI>().text = gameplay.units[item.name].Count.ToString();
			}
			await Task.Delay(100);
		}
	}
}
//547