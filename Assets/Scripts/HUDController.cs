using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUDController : MonoBehaviour, IPointerClickHandler
{
	//public GameObject hud;
	public static HUDController hud;
	// Start is called before the first frame update
	/*void Start()
    {
		hud = gameObject;
		//hud.AddComponent
		GUI.TextField(new Rect(10, 10, 200, 20), "lll");
	}*/

	private void Awake()
	{
		if (hud != null && hud != this)
			Destroy(this);
		else
			hud = this;
	}

	// Update is called once per frame
	void Update()
	{
		/*Mouse mouse = Mouse.current;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
		Vector2 mousePos = new Vector2(mousePos3D.x, mousePos3D.y);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f);
		if (hit.transform != null)
		{
			string tag = hit.transform.gameObject.tag;
			print(tag);
			switch (WhatIsHit(tag))
			{
				case "HUD_Food":
					GameplayController.Instance.ShowGUIText("Potrzeba 15 monet!");
					break;
				*//*case "Enemy":
					OnMouseEnter("Enemy");
					break;
				default:
					OnMouseExit();
					break;*//*
			}
		}*/
		//else OnMouseExit();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
		switch (eventData.pointerCurrentRaycast.gameObject.name)
		{
			case "Apple":
				ShowGUIImage("Prefabs/HUD/Image");
				break;
		}

		/*private void OnGUI()
		{

		}

		private void OnMouseEnter()
		{
			print("bbb");
		}*/

	}
	private string WhatIsHit(string tag)
	{
		if (tag == "HUD_Food")
			return "HUD_Food";
		if (tag == "Respawn")
			return "Load";
		if (tag == "Granary")
			return "Granary";
		return "";
	}

	public async Task ShowGUIImage(string path)
	{
		Image image = Instantiate(Resources.Load<Image>(path));
		image.transform.parent = GameObject.Find("HUD").transform;
		image.rectTransform.position = new Vector3(300, 400, 0);
		await Task.Delay(2000);
		Destroy(image);
	}

	public async Task ShowGUIText(string text)
	{
		TextMeshProUGUI guiText = Instantiate(Resources.Load<TextMeshProUGUI>("Prefabs/HUD/Text"));
		//GUILayout.Label(text);
		//Text guiText = go.AddComponent<Text>();
		guiText.text = text;
		print(text);
		guiText.rectTransform.position = new Vector3(675, 225, 0);
		guiText.transform.parent = GameObject.Find("HUD").transform;
		await Task.Delay(2000000);
		Destroy(guiText);
	}
}