using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class AbstractHouse : Building
{
	public int inhabitans { get; set; }
	public int maxInhabitans { get; set; }
	public int timeToNextInhabitant { get; set; }
	public string inhabitantType { get; set; }
	public void Update()
	{
		if (CompareTag("Building"))
		{
			if (gameplay.ic.satisfaction >= 50 && inhabitans < maxInhabitans)
			{
				time += Time.deltaTime;
				if (time > timeToNextInhabitant)
				{
					time = 0;
					AddInhabitant();
				}
			}
			if (gameplay.ic.satisfaction < 50 && inhabitans > 0)
			{
				time += Time.deltaTime;
				if (time > timeToNextInhabitant)
				{
					time = 0;
					RemoveInhabitant();
				}
			}

		}

	}

	protected void AddInhabitant()
	{
		AbstractVillager inhabitant = Instantiate(Resources.Load<GameObject>("Prefabs/Units/Villagers/" + inhabitantType).GetComponent<AbstractVillager>());
		inhabitant.transform.position = new Vector2(transform.position.x, transform.position.y - 2);
		List<AbstractVillager> villagersList = gameplay.ic.inhabitants[inhabitantType];
		villagersList.Add(inhabitant);
		inhabitans++;
		//gameplay.inhabitants.Add(houseType, List. inhabitant);
	}

	protected void RemoveInhabitant()
	{
		List<AbstractVillager> villagersList = gameplay.ic.inhabitants[inhabitantType];
		AbstractVillager inhabitant = villagersList[0];
		if (inhabitant.workBuilding != null)
		inhabitant.workBuilding.hasWorker = false;
		villagersList.Remove(inhabitant);
		Destroy(inhabitant.gameObject);
		inhabitans--;
		//gameplay.inhabitants.Add(houseType, List. inhabitant);
	}
}
