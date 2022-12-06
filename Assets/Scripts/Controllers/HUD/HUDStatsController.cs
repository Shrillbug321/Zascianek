using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using static GameplayControllerInitializer;

public class HUDStatsController
{
	protected TextMeshProUGUI woodCounter, clayCounter, moneyCounter, ironCounter, goldCounter;
	protected TextMeshProUGUI happinessCounter, peopleCounter;
	public void Start()
	{
		woodCounter = GameObject.Find("WoodCounter").GetComponent<TextMeshProUGUI>();
		clayCounter = GameObject.Find("ClayCounter").GetComponent<TextMeshProUGUI>();
		moneyCounter = GameObject.Find("MoneyCounter").GetComponent<TextMeshProUGUI>();
		ironCounter = GameObject.Find("IronCounter").GetComponent<TextMeshProUGUI>();
		goldCounter = GameObject.Find("GoldCounter").GetComponent<TextMeshProUGUI>();
		happinessCounter = GameObject.Find("HappinessCounter").GetComponent<TextMeshProUGUI>();
		peopleCounter = GameObject.Find("PeopleCounter").GetComponent<TextMeshProUGUI>();
	}


	public void UpdateStats()
	{
		int money = gameplay.GetItem("Money");
		//print(money);
		woodCounter.text = gameplay.GetItem("Wood").ToString();
		clayCounter.text = gameplay.GetItem("Clay").ToString();
		moneyCounter.text = string.Format("{0:D2} złp {1:D2} gr", money / 30, money % 30);
		ironCounter.text = gameplay.GetItem("Iron").ToString();
		goldCounter.text = gameplay.GetItem("Gold").ToString();
		peopleCounter.text = $"{gameplay.ic.inhabitantsSum} / {gameplay.ic.inhabitantsMax}";
		happinessCounter.text = gameplay.ic.CalcSatisfaction().ToString();
		/*peopleCounter.text = gameplay.GetItem("people").ToString();*/

	}
}