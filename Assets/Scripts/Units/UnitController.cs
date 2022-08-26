using Assets.Scripts;
using Assets.Scripts.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitController : MonoBehaviour
{
    public UnitModel Unit;
    public Rigidbody2D rb2D;
    protected Vector2 oldPos = new Vector2();
    protected Vector2 movement = new Vector2();
    private Vector2 target;
    protected int offsetX = 20;
    protected int offsetY = 10;
    protected Building workBuilding;
    protected bool moveStart = false;
    protected float movementStatus = 0;
    public Coroutine move;
    public bool stopped = true;
    // Start is called before the first frame update
    void Start()
    {
        //move = MoveObject();
        Unit = UnitFactory.Create(gameObject.name);
        Unit.rb2D = rb2D = GetComponent<Rigidbody2D>();
        Unit.transform = GetComponent<Transform>();
        oldPos = Unit.rb2D.position;
        movement = Unit.rb2D.position;
        Unit.WorkBuildingId = 1;
        //print(Unit.UnitId);
        //workBuilding = BuildingFactory.Buildings[Unit.WorkBuildingId];
    }

    // Update is called once per frame
    public void Update()
    {

        float smoothTime = 0.11f;

        Vector2 yVelocity = Vector2.zero;
        //print(move);
        Unit.transform.position = Vector2.MoveTowards(Unit.transform.position, movement, 4f*Time.deltaTime);
        //StartCoroutine(MoveObject());
        /*if (workBuilding.rb2D.position != Unit.position)
        {
            if (!moveStart)
            {
                movement = workBuilding.rb2D.position;
                moveStart = true;
            }
            else
            {
                float speed = 2;
                while (Vector2.Distance(rb2D.position, movement) > 0.2)
                {
                    movementStatus += Time.deltaTime;
                    rb2D.position = Vector2.Lerp(oldPos, movement, movementStatus / speed);
                    //print(movementStatus);
                    return;
                }
                moveStart = false;
                movementStatus = 0;
                if (Unit.WorkBuildingId == 1)
                    Unit.WorkBuildingId = 0;
                else
                    Unit.WorkBuildingId = 1;
                workBuilding = BuildingFactory.Buildings[Unit.WorkBuildingId];
                oldPos = movement;
            }
            
            *//*movement.x = movement.x / 1920 * 20 - offsetX;
            movement.y = movement.y / 1080 * 10 - offsetY;*//*
        }*/
        /* var mouse = Mouse.current;
         if (mouse.leftButton.wasPressedThisFrame)
         {
             movement = mouse.position.ReadValue();
             movement.x = movement.x / 1920 * 20 - offsetX;
             movement.y = movement.y / 1080 * 10 - offsetY;

         }*/
    }

    protected virtual void DecreaseHP(UnitModel unit, int HowMany)
    {
        unit.HP -= HowMany;
        if (unit.HP <= 0)
            print("zgon");
    }

    public IEnumerator MoveObject()
    {
        float movementStatus = 0;
        float speed = 0.5f;
        //float speed = Vector2.Distance(Unit.transform.position, movement)/6;
        print(speed);
        while (Vector2.Distance(Unit.transform.position, movement) > 0.1 && !stopped)
        {
            /*if (stopped)
            {
                print("stop");
                Unit.rb2D.position = movement;
                    oldPos = movement;
                yield break;
            }*/
            movementStatus += 0.001f;
            Unit.transform.position = Vector2.Lerp(oldPos, movement, Time.deltaTime);
            //print(movementStatus);
            yield return null;
        }
        /*if (Unit.WorkBuildingId == 1)
            Unit.WorkBuildingId = 0;
        else
            Unit.WorkBuildingId = 1; 
        workBuilding = BuildingFactory.Buildings[Unit.WorkBuildingId];*/

        oldPos = movement;
    }
}


