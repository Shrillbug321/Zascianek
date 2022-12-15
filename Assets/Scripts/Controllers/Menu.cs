using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour, IPointerClickHandler
{
	SaveGameController saveGameController;
	private GameObject mainMenu, optionsMenu;
	private Image musicButton, fullScreenButton;
	private Sprite shortButton, checkedButton;

	public void Start()
	{
		gameObject.AddComponent<SaveGameController>();
		saveGameController = gameObject.GetComponent<SaveGameController>();
		//gameObject.AddComponent<SceneController>();

		mainMenu = GameObject.Find("MainMenu");
		optionsMenu = GameObject.Find("OptionsMenu");

		musicButton = GameObject.Find("MusicButton").GetComponent<Image>();
		fullScreenButton = GameObject.Find("FullScreenButton").GetComponent<Image>();

		shortButton = Resources.Load<Sprite>("HUD/Menu/button_short");
		checkedButton = Resources.Load<Sprite>("HUD/Menu/button_checked");

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
		if (!PlayerPrefs.HasKey("hudScale"))
		{
			PlayerPrefs.SetFloat("hudScale", 1);
			PlayerPrefs.Save();
		}
		SetScale();

		musicButton.sprite = PlayerPrefs.GetString("music") == "true" ? checkedButton : shortButton;
		fullScreenButton.sprite = PlayerPrefs.GetString("fullScreen") == "true" ? checkedButton : shortButton;

		optionsMenu.SetActive(false);
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
			case "LoadGame":
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
			case "MusicButton":
				PlayerPrefs.SetString("music", (PlayerPrefs.GetString("music") == "true" ? "false" : "true").ToString());
				PlayerPrefs.Save();
				musicButton.sprite = PlayerPrefs.GetString("music") == "true" ? checkedButton : shortButton;
				break;
			case "FullScreenButton":
				Screen.fullScreen = !Screen.fullScreen;
				PlayerPrefs.SetString("fullScreen", (PlayerPrefs.GetString("fullScreen") == "true" ? "false" : "true").ToString());
				PlayerPrefs.Save();
				fullScreenButton.sprite = PlayerPrefs.GetString("fullScreen") == "true" ? checkedButton : shortButton;
				break;
			case string button when button.Contains("Scale"):
				PlayerPrefs.SetFloat("hudScale", float.Parse(button[5..]) / 100);
				SetScale();
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

	private void SetScale()
	{
		float scale = PlayerPrefs.GetFloat("hudScale");
		Vector2 scaleVector = new(scale, scale);
		mainMenu.GetComponent<RectTransform>().localScale = scaleVector;
		optionsMenu.GetComponent<RectTransform>().localScale = scaleVector;

	}
}
