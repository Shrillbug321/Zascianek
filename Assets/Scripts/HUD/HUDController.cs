using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameplayControllerInitializer;

public class HUDController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	//public GameObject hud;
	//public static HUDController hud;
	protected TextMeshProUGUI woodCounter, clayCounter, moneyCounter, ironCounter, goldCounter;
	protected TextMeshProUGUI happinessCounter, peopleCounter;
	public string lastEntered;
	public TextMeshProUGUI lastText;
	public TextMeshProUGUI buildingText;
	public HUDBuildingController buildingController;

	public void Start()
	{
		buildingController = new();
		woodCounter = GameObject.Find("WoodCounter").GetComponent<TextMeshProUGUI>();
		clayCounter = GameObject.Find("ClayCounter").GetComponent<TextMeshProUGUI>();
		moneyCounter = GameObject.Find("MoneyCounter").GetComponent<TextMeshProUGUI>();
		ironCounter = GameObject.Find("IronCounter").GetComponent<TextMeshProUGUI>();
		goldCounter = GameObject.Find("GoldCounter").GetComponent<TextMeshProUGUI>();
		happinessCounter = GameObject.Find("HappinessCounter").GetComponent<TextMeshProUGUI>();
		peopleCounter = GameObject.Find("PeopleCounter").GetComponent<TextMeshProUGUI>();
		buildingText = GameObject.Find("BuildingText").GetComponent<TextMeshProUGUI>();
		buildingController.Start();
	}

	/*private void Awake()
	{
		if (hud != null && hud != this)
			Destroy(this);
		else
		{
			hud = this;

			//buildingText.transform.parent = GameObject.Find("HUD").transform;
			//buildingText.rectTransform.position = new Vector3(475, 225, 0);
		}
	}*/

	public void UpdateStats()
	{
		int money = gameplay.GetItem("Money");
		//print(money);
		woodCounter.text = gameplay.GetItem("Wood").ToString();
		clayCounter.text = gameplay.GetItem("Clay").ToString();
		moneyCounter.text = string.Format("{0:D2} z³p {1:D2} gr", money / 30, money % 30);
		ironCounter.text = gameplay.GetItem("Iron").ToString();
		goldCounter.text = gameplay.GetItem("Gold").ToString();
		//peopleCounter.text = gameplay.GetItem("Apple").ToString();
		/*happinessCounter.text = gameplay.GetItem("happiness").ToString();
		peopleCounter.text = gameplay.GetItem("people").ToString();*/

	}
	public void OnPointerClick(PointerEventData eventData)
	{
		buildingController.OnPointerClick(eventData);
	}
	// Update is called once per frame
	public virtual void Update()
	{
		UpdateStats();
		buildingController.Update();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		buildingController.OnPointerEnter(eventData);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//Destroy(lastText);
		Debug.LogWarning(lastEntered);
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
		//GUILayout.Label(text);
		//Text guiText = go.AddComponent<Text>();
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
/*281*/