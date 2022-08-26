using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
	public float offset;
	//public float speed;
	public Vector2 minXPos, minYPos;
	public Vector2 target;
	public Vector2 movement;
	public Vector2 oldPos;
	public Transform cameraTransform;
	private float screenWidth, screenHeight;
	private Vector3 cameraMove;
	private Rect screenRect;
	private Mouse mouse;
	private const float speed = 0.1f;
	private float xMin = 0;
	private float xMax = 10;
	private float yMin = 0;
	private float yMax = 10;
	private float rightScroll = 0.8f * Screen.width;
	private float leftScroll = 0.2f * Screen.width;
	private float upScroll = 0.8f * Screen.height;
	private float downScroll = 0.2f * Screen.height;


	//Im mniejsze tym bli¿ej
	private readonly float zoomMin = 10;
	private readonly float zoomStart = 6; //6
	private readonly float zoomMax = 4;
	private Camera camera = null;
	// Start is called before the first frame update
	void Start()
	{
		camera = Camera.main;
		cameraMove.x = transform.position.x;
		cameraMove.y = transform.position.y;
		cameraMove.z = transform.position.z;
		mouse = Mouse.current;
		camera.orthographicSize = zoomStart;
		//target = Vector2.zero;
	}
	void Update()
	{
		float x = mouse.position.ReadValue().x;
		float y = mouse.position.ReadValue().y;
		float scroll = mouse.scroll.ReadValue().y;
		float zoom = Camera.main.orthographicSize;
		xMin = zoom * 1.45f - zoomStart * 1.75f;
		xMax = 25 - zoom * 1.15f - zoomStart * 1.15f;
		yMin = zoom - zoomStart*1.25f;
		yMax = 16 - zoom; //17-7=10

		switch (scroll)
		{
			case > 0:
				if (Camera.main.orthographicSize > zoomMax)
					Camera.main.orthographicSize -= 0.2f;
				break;
			case < 0:
				if (Camera.main.orthographicSize < zoomMin)
					Camera.main.orthographicSize += 0.2f;
				break;
		}

		if (cameraTransform.position.x > xMax)
		{
			cameraTransform.position = new Vector3(xMax, cameraTransform.position.y, -5);
		}

		if (cameraTransform.position.x < xMin)
		{
			cameraTransform.position = new Vector3(xMin, cameraTransform.position.y, -5);
		}

		if (cameraTransform.position.y > yMax)
		{
			cameraTransform.position = new Vector3(cameraTransform.position.x, yMax, -5);
		}

		if (cameraTransform.position.y < yMin)
		{
			cameraTransform.position = new Vector3(cameraTransform.position.x, yMin, -5);
		}

		if (x > rightScroll)
		{
			if (cameraTransform.position.x < xMax)
			{
				cameraTransform.position += new Vector3(speed, 0, 0);
			}
		}
		if (x < leftScroll)
		{
			if (cameraTransform.position.x > xMin)
			{
				cameraTransform.position += new Vector3(-speed, 0, 0);
			}
		}
		if (y > upScroll)
		{
			if (cameraTransform.position.y < yMax)
			{
				cameraTransform.position += new Vector3(0, speed, 0);
			}
		}
		if (y < downScroll)
		{
			if (cameraTransform.position.y > yMin)
			{
				cameraTransform.position += new Vector3(0, -speed, 0);
			}
		}

	}
}
