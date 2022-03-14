using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : MonoBehaviour
{
    public Unit UnitObject;
    public Rigidbody2D rb2D;
    Vector2 oldPos = new Vector2();
    Vector2 movement = new Vector2();
    private Vector2 target;
    int offsetX = 10;
    int offsetY = 5;
    Building workBuilding;
    bool moveStart = false;
    float movementStatus = 0;
    // Start is called before the first frame update
    void Start()
    {
        UnitObject = ScriptableObject.CreateInstance<Unit>();
        UnitObject.rb2D = rb2D = GetComponent<Rigidbody2D>();
        oldPos = rb2D.position;
        movement = rb2D.position;
        UnitObject.WorkBuildingId = 1;
        workBuilding = BuildingFactory.Buildings[UnitObject.WorkBuildingId];
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(MoveObject());
        if (workBuilding.rb2D.position != UnitObject.position)
        {
            if (!moveStart)
            {
                movement = workBuilding.rb2D.position;
                moveStart = true;
            }
            else
            {
                float speed = 5;
                while (Vector2.Distance(rb2D.position, movement) > 0.2)
                {
                    movementStatus += Time.deltaTime;
                    rb2D.position = Vector2.Lerp(oldPos, movement, movementStatus / speed);
                    print(movementStatus);
                    return;
                }
                moveStart = false;
                movementStatus = 0;
                if (UnitObject.WorkBuildingId == 1)
                    UnitObject.WorkBuildingId = 0;
                else
                    UnitObject.WorkBuildingId = 1;
                workBuilding = BuildingFactory.Buildings[UnitObject.WorkBuildingId];
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
        float speed = Vector2.Distance(rb2D.position, movement)/2;
        while (Vector2.Distance(rb2D.position, movement) > 0.1)
        {
            movementStatus += Time.deltaTime;
            rb2D.position = Vector2.Lerp(oldPos, movement, movementStatus / speed);
            print(movementStatus);
            yield return null;
        }
        if (UnitObject.WorkBuildingId == 1)
            UnitObject.WorkBuildingId = 0;
        else
            UnitObject.WorkBuildingId = 1; 
        workBuilding = BuildingFactory.Buildings[UnitObject.WorkBuildingId];
        oldPos = movement;
    }
}


