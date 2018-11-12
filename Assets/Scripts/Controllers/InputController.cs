using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class InputController : MonoBehaviour
{

	public static InputController Instance;

	private bool _lastInputActive;
	public bool InputActive;

	public Vector3 InputPosition;

	private Camera _camera;
	public Camera Camera { get { return _camera ?? (_camera = Camera.main); } }
	
	public delegate void InputBegan();
	public delegate void InputFinished();

	public InputBegan OnInputBegin;
	public InputFinished OnInputFinish;
	
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	void Update () {
		ManageMouseInput();
		ManageTouchInput();
	}

	private void ManageMouseInput()
	{
		var active = Input.GetMouseButton(0);
		if (active)
		{
			InputPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
			InputPosition.z = 0;
			
			if (!_lastInputActive)
				if (OnInputBegin != null)
					OnInputBegin();
		}
		else
		{
			if(_lastInputActive)
				if (OnInputFinish != null)
					OnInputFinish();
		}

		InputActive = active;
	}

	private void ManageTouchInput()
	{
		if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
			return;

		var active = Input.touchCount > 0;
		if (active)
		{
			InputPosition = Camera.ScreenToWorldPoint(Input.GetTouch(0).position);
			InputPosition.z = 0;
			
			if (!_lastInputActive)
				if (OnInputBegin != null)
					OnInputBegin();
		}
		else
		{
			if(_lastInputActive)
				if (OnInputFinish != null)
					OnInputFinish();
		}

		InputActive = active;
	}

	public void LateUpdate()
	{
		_lastInputActive = InputActive;
	}
}
