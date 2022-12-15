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
	public List<AbstractVillager> inhabitansList { get; set; } = new();
	public int timeToAddInhabitant { get; set; }
	public int timeToRemoveInhabitant { get; set; }
	public string inhabitantType { get; set; }

	public override void Start()
	{

	}

	public void Update()
	{
		if (CompareTag("Building"))
		{
			time += Time.deltaTime;
			if (inhabitans < maxInhabitans)
			{
				if (gameplay.ic.homelessInhabitans > 0)
				{
					AssignHomeless();
				}
				else if (gameplay.ic.satisfaction >= 50)
				{
					if (time > timeToAddInhabitant)
					{
						time = 0;
						AddInhabitant();
					}
				}
			}

			if (gameplay.ic.satisfaction < 50 && inhabitans > 0)
			{
				if (time > timeToRemoveInhabitant)
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
		inhabitant.haveHome = true;
		inhabitant.house = this;
		//inhabitant.transform.position = gameplay.startPath;
		inhabitant.transform.position = new Vector2(transform.position.x, transform.position.y - 2);
		List<AbstractVillager> villagersList = gameplay.ic.inhabitants[inhabitantType];
		villagersList.Add(inhabitant);
		inhabitant.HousePos = new Vector2(transform.position.x, transform.position.y - 2);
		inhabitansList.Add(inhabitant);
		inhabitans++;
		inhabitant.MoveFromStartPathToHome();
		//gameplay.inhabitants.Add(houseType, List. inhabitant);
	}

	public void AddInhabitantFromWarrior()
	{
		AbstractVillager inhabitant = Instantiate(Resources.Load<GameObject>("Prefabs/Units/Villagers/" + inhabitantType).GetComponent<AbstractVillager>());
		inhabitant.haveHome = true;
		inhabitant.house = this;
		//inhabitant.transform.position = gameplay.startPath;
		inhabitant.transform.position = new Vector2(transform.position.x, transform.position.y - 2);
		List<AbstractVillager> villagersList = gameplay.ic.inhabitants[inhabitantType];
		villagersList.Add(inhabitant);
		inhabitant.HousePos = new Vector2(transform.position.x, transform.position.y - 2);
		inhabitansList.Add(inhabitant);
		inhabitans++;
		inhabitant.MoveFromStartPathToHome();
		//gameplay.inhabitants.Add(houseType, List. inhabitant);
	}

	protected void RemoveInhabitant()
	{
		List<AbstractVillager> villagersList = gameplay.ic.inhabitants[inhabitantType];
		AbstractVillager inhabitant = villagersList[0];
		if (inhabitant.workBuilding != null)
			inhabitant.workBuilding.worker = null;
		villagersList.Remove(inhabitant);
		inhabitansList.Remove(inhabitant);
		Destroy(inhabitant.gameObject);
		inhabitans--;
		//gameplay.inhabitants.Add(houseType, List. inhabitant);
	}

	public void RemoveInhabitantForWarrior()
	{
		List<AbstractVillager> villagersList = gameplay.ic.inhabitants[inhabitantType];
		AbstractVillager inhabitant = villagersList.First(i=>!i.employed);
		if (inhabitant == null)
			return;
		villagersList.Remove(inhabitant);
		inhabitansList.Remove(inhabitant);
		inhabitans--;
		Destroy(inhabitant.gameObject);
		//gameplay.inhabitants.Add(houseType, List. inhabitant);
	}

	private void AssignHomeless()
	{
		foreach (AbstractVillager inhabitant in gameplay.ic.inhabitants[inhabitantType])
		{
			if (!inhabitant.haveHome)
			{
				if (maxInhabitans - inhabitans > 0)
				{
					inhabitant.haveHome = true;
					inhabitant.HousePos = new Vector2(transform.position.x, transform.position.y - 2);
					//inhabitant.movement = inhabitant.HousePos;
					inhabitant.GoToHouse();
					inhabitansList.Add(inhabitant);
					inhabitans++;
				}
			}
		}
	}

	private void OnDestroy()
	{
		foreach (AbstractVillager inhabitan in inhabitansList)
		{
			inhabitan.haveHome = false;
		}
	}
}
