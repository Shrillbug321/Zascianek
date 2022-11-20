using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StockBuilding : Building
{
	public void Update()
	{

	}

	public override void Reset()
	{
		base.Reset();
		//productionProgress = 0;
		//status = BuildingStatus.waitingForWorker;
	}
}
