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
    // Start is called before the first frame update
    void Start()
    {
        Unit = UnitFactory.Create();
        Unit.rb2D = rb2D = GetComponent<Rigidbody2D>();
        Unit.transform = GetComponent<Transform>();
        oldPos = rb2D.position;
        movement = rb2D.position;
        Unit.WorkBuildingId = 1;
        workBuilding = BuildingFactory.Buildings[Unit.WorkBuildingId];
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(MoveObject());
        if (workBuilding.rb2D.position != Unit.position)
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
            
            /*movement.x = movement.x / 1920 * 20 - offsetX;
            movement.y = movement.y / 1080 * 10 - offsetY;*/
        }
       /* var mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            movement = mouse.position.ReadValue();
            movement.x = movement.x / 1920 * 20 - offsetX;
            movement.y = movement.y / 1080 * 10 - offsetY;
            
        }*/
    }
    public IEnumerator MoveObject()
    {
        float movementStatus = 0;
        //float speed = 0.8f;
        float speed = Vector2.Distance(rb2D.position, movement)/6;
        while (Vector2.Distance(rb2D.position, movement) > 0.1)
        {
            movementStatus += 0.001f;
            rb2D.position = Vector2.Lerp(oldPos, movement, movementStatus / speed);
            //print(movementStatus);
            yield return null;
        }
        if (Unit.WorkBuildingId == 1)
            Unit.WorkBuildingId = 0;
        else
            Unit.WorkBuildingId = 1; 
        workBuilding = BuildingFactory.Buildings[Unit.WorkBuildingId];
        oldPos = movement;
    }
}


