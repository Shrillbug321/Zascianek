using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FoodBuilding:Building
	{
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

		public override void Reset()
		{
			base.Reset();
			status = BuildingStatus.production;
		}

	public override string CanBuild()
	{
		return base.CanBuild();
	}
}
