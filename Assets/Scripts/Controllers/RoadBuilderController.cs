using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilderController : MonoBehaviour
{
	public Transform LineParent;
	public float PointOffset;
	
	public List<Vector3> RoadPoints = new List<Vector3>();
	public GameObject RoadPrefab;

	public List<Vector3> ActualRoadPoints = new List<Vector3>();
	public RoadController ActualLine;

	private Vector3 _lastPoint;
	
	void Start ()
	{
		InputController.Instance.OnInputBegin += OnInputBegin;
		InputController.Instance.OnInputFinish += OnInputFinish;

		_lastPoint = Vector3.zero;
		RoadPoints.Clear();
	}
	
	void Update ()
	{
		if (!InputController.Instance.InputActive)
			return;

		var point = InputController.Instance.InputPosition;
		if (Vector3.Distance(point, _lastPoint) > PointOffset)
		{
			ActualRoadPoints.Add(point);
			_lastPoint = point;
			ActualLine.UpdateRoad(ActualRoadPoints);
		}
	}

	public void OnInputBegin()
	{
		ActualRoadPoints.Clear();

		var line = Instantiate(RoadPrefab, LineParent);
		ActualLine = line.GetComponent<RoadController>();
		
		ActualLine.UpdateRoad(ActualRoadPoints);

		var point = InputController.Instance.InputPosition;
		ActualRoadPoints.Add(point);
		_lastPoint = point;
	}

	public void OnInputFinish()
	{
		RoadPoints.AddRange(ActualRoadPoints);
	}

	public void Refresh()
	{
		RoadPoints.Clear();
		for (var i = LineParent.childCount - 1; i >= 0; i--)
		{
			Destroy(LineParent.GetChild(i).gameObject);
		}
	}
}
