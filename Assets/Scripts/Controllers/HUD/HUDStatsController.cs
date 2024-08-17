using TMPro;
using UnityEngine;
using static GameplayControllerInitializer;

public class HUDStatsController
{
	protected TextMeshProUGUI woodCounter, clayCounter, moneyCounter, ironCounter, goldCounter;
	protected TextMeshProUGUI happinessCounter, peopleCounter;
	protected TextMeshProUGUI villagerCounter, richVillagerCounter, nobilityCounter, priestCounter;
	protected TextMeshProUGUI infrantryCounter, heavyInfrantryCounter, crossbowerCounter;
	protected GameObject statsMain, statsUnits;
	private string visible = "StatsMain";

	public void Start()
	{
		woodCounter = GameObject.Find("WoodCounter").GetComponent<TextMeshProUGUI>();
		clayCounter = GameObject.Find("ClayCounter").GetComponent<TextMeshProUGUI>();
		moneyCounter = GameObject.Find("MoneyCounter").GetComponent<TextMeshProUGUI>();
		ironCounter = GameObject.Find("IronCounter").GetComponent<TextMeshProUGUI>();
		goldCounter = GameObject.Find("GoldCounter").GetComponent<TextMeshProUGUI>();
		happinessCounter = GameObject.Find("HappinessCounter").GetComponent<TextMeshProUGUI>();
		peopleCounter = GameObject.Find("PeopleCounter").GetComponent<TextMeshProUGUI>();

		villagerCounter = GameObject.Find("VillagerCounter").GetComponent<TextMeshProUGUI>();
		richVillagerCounter = GameObject.Find("RichVillagerCounter").GetComponent<TextMeshProUGUI>();
		nobilityCounter = GameObject.Find("NobilityCounter").GetComponent<TextMeshProUGUI>();
		priestCounter = GameObject.Find("PriestCounter").GetComponent<TextMeshProUGUI>();
		infrantryCounter = GameObject.Find("InfrantryCounter").GetComponent<TextMeshProUGUI>();
		heavyInfrantryCounter = GameObject.Find("HeavyInfrantryCounter").GetComponent<TextMeshProUGUI>();
		crossbowerCounter = GameObject.Find("CrossbowerCounter").GetComponent<TextMeshProUGUI>();

		statsMain = GameObject.Find("StatsMain");
		statsUnits = GameObject.Find("StatsUnits");
		visible = "StatsMain";
	}

	public void UpdateStats()
	{
		UpdateStatsMain();
		switch (visible)
		{
			case "StatsUnits":
				UpdateStatsUnits();
				break;
		}
	}

	private void UpdateStatsMain()
	{
		int money = gameplay.GetItem("Money");
		woodCounter.text = gameplay.GetItem("Wood").ToString();
		clayCounter.text = gameplay.GetItem("Clay").ToString();
		moneyCounter.text = $"{money:D2} gr";
		ironCounter.text = gameplay.GetItem("Iron").ToString();
		goldCounter.text = gameplay.GetItem("Gold").ToString();
		peopleCounter.text = $"{gameplay.ic.inhabitantsMax} / {gameplay.ic.placesInHouses}";
		happinessCounter.text = gameplay.ic.CalcSatisfaction().ToString();
		if (gameplay.ic.satisfaction < 50)
		{
			happinessCounter.color = Color.red;
			if (!gameplay.runningInhabitansTextIsShowed)
			{
				gameplay.runningInhabitansTextIsShowed = true;
				hud.buildingController.ShowBuildingText("Mieszkańcy uciekają z wioski", 5000);
			}
		}
		else
			happinessCounter.color = new Color(0.04565681f, 0.509434f, 0.06903008f, 1);
		/*peopleCounter.text = gameplay.GetItem("people").ToString();*/

	}

	private void UpdateStatsUnits()
	{
		villagerCounter.text = gameplay.units["Villager"].Count.ToString();
		richVillagerCounter.text = gameplay.units["RichVillager"].Count.ToString();
		nobilityCounter.text = gameplay.units["Nobility"].Count.ToString();
		priestCounter.text = gameplay.units["Priest"].Count.ToString();
		infrantryCounter.text = gameplay.units["Infrantry"].Count.ToString();
		heavyInfrantryCounter.text = gameplay.units["HeavyInfrantry"].Count.ToString();
		crossbowerCounter.text = gameplay.units["Crossbower"].Count.ToString();
	}

	public void SwitchStats(string name)
	{
		if (name == "GoToStatsMain")
		{
			statsMain.SetActive(true);
			statsUnits.SetActive(false);
			visible = "StatsMain";
		}
		if (name == "GoToStatsUnits")
		{
			statsMain.SetActive(false);
			statsUnits.SetActive(true);
			visible = "StatsUnits";
		}
	}
}