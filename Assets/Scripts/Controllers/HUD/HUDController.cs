using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameplayControllerInitializer;

public class HUDController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	private GameObject hudObject;
	//public static HUDController hud;
	public string lastEntered;
	public TextMeshProUGUI lastText;
	public TextMeshProUGUI buildingText;
	public HUDBuildingController buildingController;
	private List<HouseVillager> housesVillagers;
	private Image gameOver;
	public Dictionary<int, string> monthNames = new()
	{
		[1] = "styczeń",
		[2] = "luty",
		[3] = "marzec",
		[4] = "kwiecień",
		[5] = "maj",
		[6] = "czerwiec",
		[7] = "lipiec",
		[8] = "sierpień",
		[9] = "wrzesień",
		[10] = "październik",
		[11] = "listopad",
		[12] = "grudzień",
	};
	public Image undo;
	public TextMeshProUGUI date;
	public GameObject startButton, pauseButton;
	private float forwardDelta = 0.5f;
	public Dictionary<string, int> inhabitants = new()
	{
		["HouseVillager"] = 0,
		["HouseRichVillager"] = 0,
		["HouseNobile"] = 0
	};
	public Dictionary<string, string> unitNames = new()
	{
		["Infrantry"] = "Piechur",
		["HeavyInfrantry"] = "Ciężki piechur",
		["Crossbower"] = "Kusznik",
		["Settler"] = "Założyciel",

		["EnemyInfrantry"] = "Wrogi piechur",
		["Axer"] = "Topornik",
		["Bower"] = "Łucznik",

		["Villager"] = "Chłop",
		["RichVillager"] = "Kmieć",
		["Nobility"] = "Szlachcic",
		["Priest"] = "Ksiądz",
	};
	public HUDStatsController stats = new();

	public GameObject unitBar;
	private TextMeshProUGUI unitName;
	private TextMeshProUGUI unitHP;
	private Image unitChosen;

	public void Start()
	{
		hudObject = GameObject.Find("HUD");
		hudObject.GetComponent<CanvasScaler>().scaleFactor = PlayerPrefs.GetFloat("hudScale");
		buildingController = new HUDBuildingController();
		stats.Start();
		buildingText = GameObject.Find("BuildingText").GetComponent<TextMeshProUGUI>();
		//stats.UpdateStats();
		housesVillagers = gameplay.buildings["HouseVillager"].Cast<HouseVillager>().ToList();

		buildingController.Start();
		gameOver = GameObject.Find("GameOver").GetComponent<Image>();
		gameOver.color = Color.clear;
		undo = GameObject.Find("Undo").GetComponent<Image>();
		undo.color = new Color(1, 1, 1, 0.5f);
		date = GameObject.Find("DateText").GetComponent<TextMeshProUGUI>();

		unitBar = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "UnitBar");
		unitName = ((GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "UnitName")).GetComponent<TextMeshProUGUI>();
		unitHP = ((GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "UnitHP")).GetComponent<TextMeshProUGUI>();
		unitChosen = ((GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "UnitChosen")).GetComponent<Image>();

		startButton = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "StartButton");
		pauseButton = (GameObject)Resources.FindObjectsOfTypeAll(typeof(GameObject)).First(go => go.name == "PauseButton");
		startButton.SetActive(false);
		//DontDestroyOnLoad(this);
	}

	public virtual void Update()
	{
		stats.UpdateStats();
		buildingController.Update();
	}

	public void ShowGameOverScreen()
	{
		gameOver.color = Color.white;
		Time.timeScale = 0;
	}

	public void HideGameOverScreen()
	{
		gameOver.color = Color.clear;
		Time.timeScale = 1;
	}

	public void IconClick(GameObject gameObject)
	{
		string name = gameObject.name;
		string tag = gameObject.tag;
		if (tag == "Icon")
		{
			switch (name)
			{
				case "ZoomPlus":
					gameplay.cameraController.ZoomPlus();
					gameplay.minimapCamera.GetComponent<CameraController>().ZoomPlus();
					break;
				case "ZoomMinus":
					gameplay.cameraController.ZoomMinus();
					gameplay.minimapCamera.GetComponent<CameraController>().ZoomMinus();
					break;
				case "SlowForward":
					gameplay.timeScale -= forwardDelta;
					if (!gameplay.paused)
						Time.timeScale = gameplay.timeScale;
					break;
				case "PauseButton":
					Time.timeScale = 0;
					gameplay.paused = true;
					pauseButton.SetActive(false);
					startButton.SetActive(true);
					break;
				case "StartButton":
					Time.timeScale = gameplay.timeScale;
					gameplay.paused = false;
					startButton.SetActive(false);
					pauseButton.SetActive(true);
					break;
				case "FastForward":
					gameplay.timeScale += forwardDelta;
					if (!gameplay.paused)
						Time.timeScale = gameplay.timeScale;
					break;
				case "GoToStatsMain" or "GoToStatsUnits":
					stats.SwitchStats(name);
					break;
				case "UnitChosen":
					var inh = Instantiate(Resources.Load<GameObject>("Prefabs/Units/Villagers/Villager"));
					inh.transform.position = gameObject.transform.position;
					Destroy(gameObject);
					break;
				default:
					buildingController.IconClick(gameObject);
					break;
			}
		}
		if (tag == "ActionBarIcon")
			ActionBarIconClick(name);
		if (tag == "GroupIcon")
			buildingController.GroupClick(name);
	}

	private string CalculateInhabitants()
	{
		List<List<AbstractHouse>> housesOfTypes = new()
		{
			gameplay.buildings["HouseVillager"].Cast<AbstractHouse>().ToList(),
			gameplay.buildings["HouseRichVillager"].Cast<AbstractHouse>().ToList(),
			gameplay.buildings["HouseNobility"].Cast<AbstractHouse>().ToList()
		};
		int max = 0, sum = 0;
		foreach (List<AbstractHouse> houses in housesOfTypes)
		{
			if (houses.Count > 0)
			{
				string houseType = houses[0].GetType().ToString();
				inhabitants[houseType] = houses.Sum(h => h.inhabitans);
				sum += inhabitants[houseType];
				max += houses.Sum(h => h.maxInhabitans);
			}
		}
		sum += Church.priests;
		max += Church.priests;
		return $"{sum}/{max}";
	}

	private void ActionBarIconClick(string name)
	{
		switch (name)
		{
			case "OpenMenu":
				SceneManager.LoadScene("MainMenu");
				break;
			case "Repair":
				gameplay.SetCursor("HUD/Icons/hammer");
				gameplay.mode = Mode.repair;
				break;
			case "Destroy":
				gameplay.SetCursor("HUD/Icons/destroy");
				gameplay.mode = Mode.destroy;
				break;
			case "Cut":
				gameplay.SetCursor("HUD/Icons/cut");
				gameplay.mode = Mode.cut;
				break;
			case "Undo":
				Building lastBuilding = gameplay.lastBuilding;
				if (lastBuilding == null) return;
				if (lastBuilding.needToBuild != null)
					foreach (KeyValuePair<string, int> item in lastBuilding.needToBuild)
						gameplay.items[item.Key] += item.Value;
				Destroy(lastBuilding.gameObject);
				undo.color = new Color(1, 1, 1, 0.5f);
				gameplay.lastBuilding = null;
				break;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		switch (gameplay.mode)
		{
			case Mode.unit:
				UnitModel unit = eventData.pointerCurrentRaycast.gameObject.GetComponent<UnitModel>();
				buildingController.list.SetActive(false);
				unitName.text = unitNames[unit.name];
				unitHP.text = unit.hp + "/" + unit.maxHp;
				break;
			default:
				hud.IconClick(eventData.pointerCurrentRaycast.gameObject);
				break;
		}
	}

	public void SwitchUnitBar(UnitModel unit)
	{
		string name = unit.GetType().ToString();
		buildingController.list.SetActive(false);
		unitBar.SetActive(true);
		unitName.text = unitNames[name];
		unitHP.text = $"{unit.hp}/{unit.maxHp}";
		unitChosen.sprite = Resources.Load<Sprite>("Sprites/Units/" + name.ToSnakeCase());
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		buildingController.OnPointerEnter(eventData);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		lastEntered = "";
		//ShowBuildingText("");
		/*switch (eventData.pointerCurrentRaycast.gameObject.name)
		{
			case "BuildingsList":
				inList = false;
				break;
			case "Pigsty":
				ShowGUIText("2 drewna");
				break;
		}*/
	}

	public async Task ShowGUIImage(string path)
	{
		Image image = Instantiate(Resources.Load<Image>(path));
		image.transform.parent = GameObject.Find("HUD").transform;
		image.rectTransform.position = new Vector3(300, 400, 0);
		await Task.Delay(2000);
		Destroy(image);
	}

	public async Task ShowGUIText(string text, float xPos = 675, float yPos = 225, int time = int.MaxValue)
	{
		TextMeshProUGUI guiText = Instantiate(Resources.Load<TextMeshProUGUI>("Prefabs/HUD/Text"));
		guiText.text = text;
		print(text);
		guiText.rectTransform.position = new Vector3(xPos, yPos, 0);
		guiText.transform.parent = GameObject.Find("HUD").transform;

		if (time == int.MaxValue)
			lastText = guiText;
		else
		{
			await Task.Delay(time);
			Destroy(guiText);
		}
	}

	public async Task ShowShortGUIText(string text, int time = int.MaxValue)
	{
		buildingText.text = text;
		await Task.Delay(time);
		buildingText.text = "";
	}

}