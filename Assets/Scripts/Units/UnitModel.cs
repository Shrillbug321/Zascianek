using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
	public class UnitModel : MonoBehaviour
	{
		public int UnitId { get; set; }
		public int WorkBuildingId { get; set; }
		public string Name { get; set; }
		public float Speed { get; set; }
		public int HP { get; set; } = 100;
		public string Type { get; set; }
		public int Armor { get; set; }
		public bool IsChoosen { get; set; } = false;

		public Rigidbody2D rb2D;
		public SpriteRenderer sr;
		protected Vector2 oldPos = new Vector2();
		protected Vector2 movement = new Vector2();
		private Vector2 target;
		public Vector2 direction;
		protected Building workBuilding;
		protected bool moveStart = false;
		protected float movementStatus = 0;
		public bool stopped = true;


		public CancellationTokenSource tokenSource;
		public CancellationToken token;
		public virtual void Start()
		{
			//UnitId = Units.Count;
			Name = name;
			//WorkBuildingId = 1;

			gameObject.AddComponent<Rigidbody2D>();
			rb2D = GetComponent<Rigidbody2D>();
			GetComponent<Rigidbody2D>().simulated = true;

			gameObject.AddComponent<SpriteRenderer>();
			sr = GetComponent<SpriteRenderer>();
			sr.sortingLayerName = "Characters";

			BoxCollider2D bc2d = gameObject.AddComponent<BoxCollider2D>();
			bc2d.isTrigger = true; 
			bc2d.size = new Vector2(1.17f, 2.27f);
			//gameObject.AddComponent<BoxCollider2D>();
			/*GetComponent<BoxCollider2D>().isTrigger = true;
			GetComponent<BoxCollider2D>().size = new Vector2(1.17f, 2.27f);*/
			//gameObject.AddComponent<CircleCollider2D>();

			
			/*GetComponent<CircleCollider2D>().isTrigger = true;
			GetComponent<CircleCollider2D>()*/
			gameObject.layer = LayerMask.NameToLayer("Characters");
			//print(GetComponent<CircleCollider2D>().radius);

			oldPos = rb2D.position;
			movement = rb2D.position;

			tokenSource = new CancellationTokenSource();
			token = tokenSource.Token;
			GameplayController.Instance.AddUnit(this);
		}

		public virtual void Update()
		{
			if (Vector2.Distance(transform.position, movement) > 0.5)
			transform.position = Vector2.MoveTowards(transform.position, movement, 4f * Time.deltaTime);
			direction = ((Vector2)transform.position - oldPos).normalized;
			sr.flipX =  direction.x <= 0;
		}

		public async Task Rotate()
		{
			int times = 7;
			for (int i = 0; i < times; i++)
			{
				Quaternion rotation = Quaternion.Euler(0, 0, -90) * (transform.rotation);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 50 * Time.deltaTime);
				await Task.Delay(100);
			}
		}
		public virtual void DecreaseHP(int HowMany)
		{
			HP = HowMany > 0 ? HP - HowMany : HP;
			print(HowMany);
		}

		public async Task Blinking(int attackSpeed, CancellationToken token)
		{
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			while (!token.IsCancellationRequested)
			{
				sr.color = Color.red;
				await Task.Delay(500);
				sr.color = Color.white;
				await Task.Delay(attackSpeed - 500);
			}
		}

		public void SetCircleCollider(int radius)
		{
			CircleCollider2D cc2d = gameObject.AddComponent<CircleCollider2D>();
			cc2d.isTrigger = true;
			cc2d.radius = radius;
		}

		public void OnDestroy()
		{
			GameplayController.Instance.RemoveUnit(this);
		}
	}
}
