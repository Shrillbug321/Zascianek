using UnityEngine;

public class Enemy : AbstractWarrior
{
	public override void Start()
	{
		base.Start();
		type = "Enemy";
		color = "Brown";
		//movement = new Vector2(-10, -20);
		movement = GameObject.Find("HouseSettler").transform.position;
		moveStart = true;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}

	/*public virtual void OnTriggerEnter2D(Collider2D collision)
	{
		*//*string tag = collision.tag;
		colliderObject = collision;
		if (tag == this.tag) return;
		if (GetComponent<BoxCollider2D>().IsTouching(collision))
		{
			print("ppp");
			if (tag == "Building")
			{
				oldPos = transform.position;
				temp = movement;
				//todo ruch
				movement.x = (transform.position.x + ((CircleCollider2D)collision).radius + 3) * direction.x;
				movement.y = (transform.position.y + ((CircleCollider2D)collision).radius + 3) * direction.y;*//*
					movement.x = (transform.position.x + ((BoxCollider2D)collision).size.x + 3) * direction.x;
					movement.y = (transform.position.y + ((BoxCollider2D)collision).size.y + 3) * direction.y;*//*
				gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
			}
		}
		if (collision.GetType() == typeof(CircleCollider2D))
		{

		}*//*

	}*/
}
