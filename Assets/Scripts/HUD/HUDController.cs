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
	public static HUDController hud;
	protected TextMeshProUGUI woodCounter, clayCounter, moneyCounter, ironCounter, goldCounter;
	protected TextMeshProUGUI happinessCounter, peopleCounter;
	public string lastEntered;
	public TextMeshProUGUI lastText;
	private HUDBuildingController buildingController;

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
		buildingController.Start();
	}

	private void Awake()
	{
		if (hud != null && hud != this)
			Destroy(this);
		else
		{
			hud = this;

			//buildingText.transform.parent = GameObject.Find("HUD").transform;
			//buildingText.rectTransform.position = new Vector3(475, 225, 0);
		}
	}

	public void UpdateStats()
	{
		int money = gameplay.GetItem("money");
		print(money);
		woodCounter.text = gameplay.GetItem("wood").ToString();
		clayCounter.text = gameplay.GetItem("clay").ToString();
		moneyCounter.text = money/30 + " z³p " + money%30 + " gr";
		ironCounter.text = gameplay.GetItem("iron").ToString();
		goldCounter.text = gameplay.GetItem("gold").ToString();
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
	/*	Debug.Log("Entered: " + eventData.pointerCurrentRaycast.gameObject.name);
		lastEntered = eventData.pointerCurrentRaycast.gameObject.name;
		*//*GraphicRaycaster raycaster = GameObject.Find("HUD").GetComponent<GraphicRaycaster>();
		List<RaycastResult> results = new List<RaycastResult>();
		List<RaycastResult> results2 = new List<RaycastResult>();
		raycaster.Raycast(eventData, results);
		foreach (RaycastResult result in results)
		{
  print(result);
		}*//*
		switch (eventData.pointerCurrentRaycast.gameObject.name)
		{
			case "BuildingsList":
				*//*print(results[0].screenPosition);
				print(results[0].worldPosition);
				results2 = new List<RaycastResult>();
				raycaster.Raycast(results[0]., results2);
				foreach (RaycastResult result in results2)
				{
					print(result);
				}*//*
				inList = true;
				GameObject a = eventData.pointerCurrentRaycast.gameObject;
				Transform child = eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0);
				Transform enteredBuilding = null;
				print(a.transform.GetChild(0).name);
				print(gameplay.GetMousePos());
				Vector2 mousePos = gameplay.GetMousePos();
				foreach (Transform b in child.transform)
				{
					print(mousePos.x);
					print(b.transform.position.x);
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
		}*/
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

}
/*281*/