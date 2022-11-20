using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ProductionBuilding : Building
{
	public int productionProgress { get; set; }
	public void Update()
	{
		if (status == BuildingStatus.production)
		{
			time += Time.deltaTime;
			productionProgress = (int)Math.Round(time / productionTime * 100, 0);
			if (time > productionTime)
			{
				time = 0;
				status = BuildingStatus.transport;
			}
		}

	}

	public virtual async Task<Dictionary<string, int>> Production()
	{
		while (productionProgress < 99)
			await Task.Delay(100);

		return products;
	}

	public override void Reset()
	{
		base.Reset();
		productionProgress = 0;
		status = BuildingStatus.waitingForWorker;
	}
}
