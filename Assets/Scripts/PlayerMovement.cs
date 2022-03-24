using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("The speed in which the player will move when pressing the movement keys")]
    [SerializeField] private float _speed = 1f;

    void Update()
    {
        Movement();   
    }

    private void Movement()
    {
        //creates a Vector2 for applying move direction and sets it to zero
        Vector2 moveDir = Vector2.zero;

        //applies the y of movedir to the vertical input
        moveDir.y = Input.GetAxisRaw("Vertical");
        //applies the x of movedir to the horizontal input
        moveDir.x = Input.GetAxisRaw("Horizontal");
        //normalized the input so it doesn't speed up when both inputs pressed
        moveDir.Normalize();
        
        //multiplies moveDir by the speed variable and then deltaTime as to even it out across all computers
        //applies the vector to the position of the gameobject
        moveDir *= (_speed * Time.deltaTime);
        transform.position += (Vector3)moveDir;
    }
}
