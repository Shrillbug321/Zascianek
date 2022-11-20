using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AbstractHouse : Building
{
	public int residents { get; set; }
	public int timeToNextResident { get; set; }
	public string houseType { get; set; }
	public void Update()
	{
		if (status == BuildingStatus.production)
		{
			time += Time.deltaTime;
			if (time > timeToNextResident)
			{
				time = 0;
				//AddResident();
			}
		}

	}

	/*public virtual async Task<Dictionary<string, int>> AddResident()
	{
		AbstractVillager resident = Instantiate(Resources.Load<GameObject>("Prefabs/Units/Villagers/" + name).GetComponent<AbstractVillager>());
		*//*while (productionProgress < 99)
			await Task.Delay(100);

		return products;*//*
	}*/
}
