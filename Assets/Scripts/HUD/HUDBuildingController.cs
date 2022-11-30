using Assets.Scripts;
using System;
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

public class HUDBuildingController : HUDBuildingText, IPointerEnterHandler
{
	protected Vector3 groupPos;
	protected GameObject groupSelected;
	protected GameObject detailsBar;
	protected GameObject list;
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
		houseBar = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "HouseBar");
		barracks = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "Barracks");
		market = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "MarketBar");
		taxBar = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "TaxBar");
		stocks.SetActive(false);
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
			case Mode.house:
				List<UnityEngine.Object> clones = FindObjectsOfType(typeof(GameObject)).Where(go => go.name == "Inhabitan(Clone)").ToList();
				foreach (UnityEngine.Object item in clones)
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
				list.SetActive(true);
				market.SetActive(false);
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
	public void IconClick(GameObject gameObject)
	{
		string name = gameObject.name;
		string tag = gameObject.tag;
		Debug.LogWarning(name);
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
					Dictionary<string, int> taxes = gameplay.ic.ChangeTax(int.Parse(name));
					foreach (KeyValuePair<string, int> tax in taxes)
						GameObject.Find("Tax" + tax.Key).GetComponent<TextMeshProUGUI>().text = tax.Value.ToString();
					GameObject.Find("TaxHappiness").GetComponent<TextMeshProUGUI>().text = gameplay.ic.CalculateTaxSatisfaction().ToString();
					break;
				case Mode.recruit:
					Recruit(name);
					break;
				case Mode.market:
					MarketIconClick(name);
					break;
			}
		}

		//if (tag == "Building")
		//BuildingClick(name);
	}/*
	public void OnPointerClick(PointerEventData eventData)
	{
		string name = eventData.pointerCurrentRaycast.gameObject.name;
		string tag = eventData.pointerCurrentRaycast.gameObject.tag;
		Debug.LogWarning(name);
		GroupClick(name);
		if (tag == "Icon")
		{
			switch (GameplayControllerInitializer.gameplay.mode)
			{
				case Mode.nothing:
					BuildingIconClick(name);
					break;
				case Mode.recruit:
					Recruit(name);
					break;
			}
		}

		//if (tag == "Building")
		//BuildingClick(name);
	}*/

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
		string groupName = groupSelected.name.Substring(groupSelected.name.IndexOf("Buildings") + "Buildings".Length);
		Building building = Resources.Load<GameObject>("Prefabs/Buildings/" + groupName + "/" + name).GetComponent<Building>();
		building.Start();
		Debug.ClearDeveloperConsole();
		//Debug.LogWarning(building.GetType());
		string hasItems = building.CheckItemsNeedToBuilding();
		string stockWasBuilded = building.CheckStockBuildingWasBuilded();
		if (hasItems == "" && stockWasBuilded == "")
			gameplay.PlaceBuilding(building);
		else
		{
			if (hasItems != "")
				ShowBuildingText(hasItems, 5000);
			if (stockWasBuilded != "")
				ShowBuildingText(buildingsNames[stockWasBuilded] + " musi być wybudowany!", 5000);
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
			case ProductionBuilding a:
				detailsBar.SetActive(true);
				buildingItem.gameObject.SetActive(true);
				buildingStatus.gameObject.SetActive(true);
				productionProgress.gameObject.SetActive(true);

				buildingName.text = buildingsNames[name];
				buildingDP.text = building.dp.ToString() + "/" + building.maxDp.ToString();
				//refreshStatus(name, building.status.ToString());
				//buildingStatus.text = statuses[name][building.status.ToString()];
				refreshProgress(a);
				buildingClicked.sprite = Resources.Load<Sprite>("Sprites/Buildings/" + buildingsImages[name]);
				buildingItem.sprite = Resources.Load<Sprite>("HUD/Icons/Items/" + itemInBuilding[name]);

				buildingItem.SetNativeSize();
				SetSize(buildingItem);
				Debug.LogWarning("Sprites/Buildings/" + buildingsImages[name]);
				Debug.LogWarning("HUD/Icons/Items/" + itemInBuilding[name]);
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
				break;
			case HouseSettler:
				list.SetActive(false);
				taxBar.SetActive(true);
				gameplay.mode = Mode.tax;
				break;
			case AbstractHouse house:
				list.SetActive(false);
				detailsBar.SetActive(true);

				buildingName.text = buildingsNames[name];
				buildingDP.text = building.dp.ToString() + "/" + building.maxDp.ToString();
				buildingClicked.sprite = Resources.Load<Sprite>("Sprites/Buildings/" + buildingsImages[name]);
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
				buildingName.text = buildingsNames[name];
				buildingDP.text = building.dp.ToString() + "/" + building.maxDp.ToString();
				buildingClicked.sprite = Resources.Load<Sprite>("Sprites/Buildings/" + buildingsImages[name]);
				buildingItem.sprite = Resources.Load<Sprite>("Sprites/Units/priest");
				buildingItem.SetNativeSize();

				buildingStatus.text = "Księży: " + Church.priests;
				break;
		}
	}

	private void Recruit(string unitName)
	{
		UnitModel warrior = Instantiate(Resources.Load<UnitModel>("Prefabs/Units/Warriors/Player/" + unitName));
		Vector3 pos = GameObject.Find("Barracks").transform.position;
		warrior.transform.position = new Vector3(pos.x, pos.y - 1f, 0);
		//warrior.gameObject.name = units.Count.ToString();
	}

	private void MarketIconClick(string item)
	{
		if (gameplay.mouse.leftButton.wasReleasedThisFrame)
			Market.Buy(item);

		if (gameplay.mouse.rightButton.wasReleasedThisFrame)
			Market.Sell(item);
	}

	private void ItemClick(string item)
	{
		if (buildingsNames.First(n => n.Value == buildingName.text).Key == "Church")
		{
			AbstractVillager priest = Instantiate(Resources.Load<AbstractVillager>("Prefabs/Units/Villagers/Priest"));
			Vector3 pos = GameObject.Find("Church").transform.position;
			priest.transform.position = new Vector3(pos.x - 2.5f, pos.y - 3f, 0);
			List<AbstractVillager> villagersList = gameplay.ic.inhabitants["Priest"];
			villagersList.Add(priest);
			Church.priests++;
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
