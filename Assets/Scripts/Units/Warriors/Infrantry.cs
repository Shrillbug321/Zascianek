using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infrantry : Warrior
{

    // Update is called once per frame
    void Update()
    {
        base.Update();
        /*CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>();
        if (collider.CompareTag("Enemy"))
            print('j');*/
    }

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D(collision);
		/*if (collision.GetType() == typeof(CircleCollider2D))
		{
			
		print('b');
		}*/
		if (collision.GetType() == typeof(BoxCollider2D))
		{
			print('c');
		}
	}
}
