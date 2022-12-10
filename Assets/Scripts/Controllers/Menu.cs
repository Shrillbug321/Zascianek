using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour, IPointerClickHandler
{
	SaveGameController saveGameController;
	GameObject mainMenu, optionsMenu;
	public void Start()
	{
		gameObject.AddComponent<SaveGameController>();
		saveGameController = gameObject.GetComponent<SaveGameController>();
		//gameObject.AddComponent<SceneController>();

		mainMenu = GameObject.Find("MainMenu");
		optionsMenu = GameObject.Find("OptionsMenu");
		optionsMenu.SetActive(false);

		if (!PlayerPrefs.HasKey("music"))
		{
			PlayerPrefs.SetString("music", "true");
			PlayerPrefs.Save();
		}
		if (!PlayerPrefs.HasKey("fullScreen"))
		{
			PlayerPrefs.SetString("fullScreen", "true");
			PlayerPrefs.Save();
		}
		Screen.fullScreen = PlayerPrefs.GetString("fullScreen") == "true";
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		switch (eventData.pointerCurrentRaycast.gameObject.name)
		{
			case "NewGame":
				SceneController.buttonClicked = "NewGame";
				//SceneManager.LoadScene("Gameplay");
				SceneManager.LoadScene("Gameplay");
				break;
			case "Load":
				SceneController.buttonClicked = "LoadGame";
				SceneManager.LoadScene("Gameplay");
				//saveGameController.Load();
				break;
			case "Options":
				mainMenu.SetActive(false);
				optionsMenu.SetActive(true);
				break;
			case "Quit":
				Application.Quit();
				break;
			case "Music":
				PlayerPrefs.SetString("music", (PlayerPrefs.GetString("music") == "true" ? "false" : "true").ToString());
				PlayerPrefs.Save();
				break;
			case "FullScreen":
				Screen.fullScreen = !Screen.fullScreen;
				PlayerPrefs.SetString("fullScreen", Screen.fullScreen.ToString());
				PlayerPrefs.Save();
				break;
			case "ReturnToMainMenu":
				optionsMenu.SetActive(false);
				mainMenu.SetActive(true);
				break;
		}
	}

	public void Update()
	{
		RaycastHit2D hit = Physics2D.Raycast(MouseController.GetMousePos(), Vector2.zero, 1f);
		if (hit.collider != null)
		{
			string tag = hit.collider.gameObject.tag;
		}
	}
}
