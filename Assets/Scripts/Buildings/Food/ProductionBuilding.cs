using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ProductionBuilding : Building
{
	public int productionProgress { get; set; }

	public void Update()
	{
		if (stopped)
		{
			updateTime = 0;
			status = BuildingStatus.waitingForWorker;
			productionProgress = 0;
			return;
		}

		if (status == BuildingStatus.production)
		{
			updateTime += Time.deltaTime;
			productionProgress = (int)Math.Round(updateTime / productionTime * 100, 0);
			if (updateTime > productionTime)
			{
				updateTime = 0;
				status = BuildingStatus.transport;
				//NextStatus();
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