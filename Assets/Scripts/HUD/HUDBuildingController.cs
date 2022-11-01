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

public class HUDBuildingController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
	protected Vector3 groupPos;
	protected GameObject groupSelected;
	protected TextMeshProUGUI buildingText;
	protected bool inList;

	public void Start()
	{
		groupSelected = GameObject.Find("BuildingsFood");
		groupPos = groupSelected.transform.position;
		buildingText = GameObject.Find("BuildingText").GetComponent<TextMeshProUGUI>();
	}
	public void ChangeGroup(string groupName)
	{
		Destroy(groupSelected);
		Image group = Instantiate(Resources.Load<Image>("Prefabs/HUD/Groups/" + groupName));

		group.transform.SetParent(GameObject.Find("BuildingsList").transform, false);
		group.rectTransform.position = groupPos;
		groupSelected = group.gameObject;
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
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Entered: " + eventData.pointerCurrentRaycast.gameObject.name);
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
		}
	}
	public void OnPointerClick(PointerEventData eventData)
	{

		Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
		switch (eventData.pointerCurrentRaycast.gameObject.name)
		{
			case "Apple":
				ChangeGroup("BuildingsFood");
				break;
			case "AppleField":
				if (gameplay.items["wood"] < 2)
				{
					print("aaa");
					ShowBuildingText("Za mało drewna!", 5000);
				}
				else
					gameplay.LoadBuilding("AppleField");
				break;
			case "Industry":
				ChangeGroup("BuildingsIndustry");
				break;
		}

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
}
