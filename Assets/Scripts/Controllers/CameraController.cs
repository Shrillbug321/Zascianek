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
	private Vector3 cameraMove;
	private Mouse mouse;
	private const float speed = 0.1f;
	private float xMin = 0;
	private float xMax = 80;
	private float yMin = 0;
	private float yMax = 80;
	private float rightScroll = 0.8f * Screen.width;
	private float leftScroll = 0.1f * Screen.width;
	private float upScroll = 0.8f * Screen.height;
	private float downScroll = 0.1f * Screen.height;

	//Im mniejsze tym bliżej
	private readonly float zoomMin = 10;
	private readonly float zoomStart = 6; //6
	private readonly float zoomMax = 4;
	private Camera camera;
	
	void Start()
	{
		cameraTransform = gameObject.transform;
		camera = gameObject.GetComponent<Camera>();
		cameraMove.x = transform.position.x;
		cameraMove.y = transform.position.y;
		cameraMove.z = transform.position.z;
		mouse = Mouse.current;
		camera.orthographicSize = zoomStart;
	}

	void Update()
	{
		float x = mouse.position.ReadValue().x;
		float y = mouse.position.ReadValue().y;
		float scroll = mouse.scroll.ReadValue().y;

		switch (scroll)
		{
			case > 0:
				ZoomPlus();
				break;
			case < 0:
				ZoomMinus();
				break;
		}

		if (x > rightScroll)
			if (cameraTransform.position.x < xMax)
				cameraTransform.position += new Vector3(speed, 0, 0);
        
		if (x < leftScroll)
			if (cameraTransform.position.x > xMin)
				cameraTransform.position += new Vector3(-speed, 0, 0);
        
		if (y > upScroll)
			if (cameraTransform.position.y < yMax)
				cameraTransform.position += new Vector3(0, speed, 0);
        
		if (y < downScroll)
			if (cameraTransform.position.y > yMin)
				cameraTransform.position += new Vector3(0, -speed, 0);
	}

	public void ZoomMinus()
	{
		if (camera.orthographicSize < zoomMin)
			camera.orthographicSize += 0.2f;
	}

	public void ZoomPlus()
	{
		if (camera.orthographicSize > zoomMax)
			camera.orthographicSize -= 0.2f;
	}
}