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
		public Rigidbody2D rb2D;
		public Camera Camera { get; set; }
		public bool IsChoosen { get; set; } = false;

		protected Vector2 oldPos = new Vector2();
		protected Vector2 movement = new Vector2();
		private Vector2 target;
		protected Building workBuilding;
		protected bool moveStart = false;
		protected float movementStatus = 0;
		public bool stopped = true;


		public CancellationTokenSource tokenSource;
		public CancellationToken token;
		public virtual void Start()
		{
			//move = MoveObject();
			//Unit = UnitFactory.Create(gameObject.name);
			Camera = Camera.main;
			//UnitId = Units.Count;
			Name = name;
			gameObject.AddComponent<Rigidbody2D>();
			rb2D = GetComponent<Rigidbody2D>();
			//Unit.transform = GetComponent<Transform>();
			oldPos = rb2D.position;
			movement = rb2D.position;
			//WorkBuildingId = 1;

			tokenSource = new CancellationTokenSource();
		token = tokenSource.Token;
	}

		public void Update()
		{
			transform.position = Vector2.MoveTowards(transform.position, movement, 4f * Time.deltaTime);
			/*Quaternion rotation = Quaternion.Euler(0, 0, -90) * (transform.rotation);
			print(rotation);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 50f * Time.deltaTime);*/
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
			
			//yield return WaitForSeconds(1);
		}
		public virtual void DecreaseHP(int HowMany)
		{
			HP -= HowMany;
		}

		public async Task Blinking(int attackSpeed, CancellationToken token)
		{
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			while (!token.IsCancellationRequested)
			{
				sr.color = Color.red;
				await Task.Delay(500);
				sr.color = Color.white;
				await Task.Delay(attackSpeed-500);
			}
		}
	}
}
