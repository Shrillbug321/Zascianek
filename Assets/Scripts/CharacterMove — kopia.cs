/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : MonoBehaviour
{
    Rigidbody2D UnitObject;
    Vector2 oldPos = new Vector2();
    Vector2 movement = new Vector2();
    private Vector2 target;
    int offsetX = 10;
    int offsetY = 5;
    // Start is called before the first frame update
    void Start()
    {
        UnitObject = GetComponent<Rigidbody2D>();
        oldPos = character.position;
        movement = character.position;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoveObject());
        var mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            movement = mouse.position.ReadValue();
            movement.x = movement.x / 1920 * 20 - offsetX;
            movement.y = movement.y / 1080 * 10 - offsetY;
            
        }
    }
    public IEnumerator MoveObject()
    {
        float movementStatus = 0;
        float speed = Vector2.Distance(character.position, movement)/2;
        while (Vector2.Distance(character.position, movement) > 0)
        {
            movementStatus += Time.deltaTime;
            character.position = Vector2.Lerp(oldPos, movement, movementStatus / speed);
            print(movementStatus);
        yield return null;
        }
        oldPos = movement;
    }
}*/


