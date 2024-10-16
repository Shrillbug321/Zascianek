using UnityEngine;
using UnityEngine.InputSystem;
using static GameplayControllerInitializer;

public class Warrior : AbstractWarrior
{
	public override void Start()
	{
		base.Start();
		color = "Green";
	}

	public override void Update()
	{
		base.Update();
		Mouse mouse = Mouse.current;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
		Vector2 mousePos = new Vector2(mousePos3D.x, mousePos3D.y);

		if (mouse.leftButton.wasPressedThisFrame)
			LeftClick(mousePos);
		
		if (mouse.rightButton.wasPressedThisFrame)
			RightClick();
	}

	protected void LeftClick(Vector2 mousePos)
	{
		if (!isChosen)
		{
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f);
			if (hit.collider?.gameObject.name == unitName)
			{
				if (gameplay.MouseInRange(mousePos, hit, 0.5f))
				{
					isChosen = true;
					gameplay.unitIsChosen = true;
					gameplay.mode = Mode.unit;
				}
			}
		}
		else
		{
			if (moveStart)
			{
				oldPos = movement;
				movement = new Vector2(mousePos.x, mousePos.y);
			}
			else
			{
				oldPos = rb2D.position;
				movement = new Vector2(mousePos.x, mousePos.y);
				moveStart = true;

			}
		}
	}

	public void RightClick()
	{
		isChosen = false;
		gameplay.unitIsChosen = false;
		gameplay.SetCursor("Assets/HUD/cursor.png");
	}

	public override async void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D(collision);
		string tag = collision.tag;
		if (CompareTag(tag)) return;
		if (collision.name == "Church" && collision.CompareTag("Building"))
			FullHP();
	}
}