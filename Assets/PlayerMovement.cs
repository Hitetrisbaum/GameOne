using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float movementspeed = 12f;
    public float sprintspeed = 16f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        //Erstellt eine Sphere unter dem Player um zu checken ob dieser am Boden steht
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log(isGrounded);
        //falls am Boden, keine Beschleunigung nach unten
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        //Springen
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);



        //Bewegen
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //Kalkulation von Bewegung
        Vector3 move = transform.right * x + transform.forward * z;

        //Ausführen der Bewegung
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(move * sprintspeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(move * movementspeed * Time.deltaTime);
        }
        //Fallen falls in der Luft
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
