using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{

	public LineRenderer LineBegin;
	public LineRenderer Line;
	public LineRenderer LineFinish;
	
	public void UpdateRoad(List<Vector3> positions)
	{
		if (positions.Count < 2)
		{
			LineBegin.positionCount = 0;
			Line.positionCount = 0;
			LineFinish.positionCount = 0;
			return;
		}
		
		LineBegin.positionCount = 2;
		LineBegin.SetPositions(positions.GetRange(0,2).ToArray());
		
		Line.positionCount = positions.Count - 2;
		Line.SetPositions(positions.GetRange(1, positions.Count-2).ToArray());

		if (positions.Count < 4)
			return;
		
		LineFinish.positionCount = 2;
		LineFinish.SetPositions(positions.GetRange(positions.Count-2,2).ToArray());
	}
}
