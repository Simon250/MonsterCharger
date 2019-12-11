using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    Animator anim;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

   

    private Vector3 moveDirection = Vector3.zero;
    

    void Start()
    {
        Camera main;
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            var camera = Camera.main;

            //camera forward and right vectors:
            var forward = camera.transform.forward;
            var right = camera.transform.right;

            //project forward and right vectors on the horizontal plane (y = 0)
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            moveDirection = forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal");
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

       
        // Walking Animation
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = new Vector3(0, 0, 1);
            anim.SetInteger("condition", 1);
        }
        // Anim is IDLE
        if (Input.GetKeyUp(KeyCode.W))
        {
            moveDirection = new Vector3(0, 0, 1);
            anim.SetInteger("condition", 0);
        }
        //Anim Attack
        if (Input.GetMouseButtonDown(0)) {
            anim.SetInteger("condition", 3);
        }
        //Anim reset anim
        if (Input.GetMouseButtonUp(0)) {
            if (Input.GetKey(KeyCode.W)){
                anim.SetInteger("condition", 1);
            }
            else {
                anim.SetInteger("condition", 0);
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        float x = 5 * Input.GetAxis("Mouse X");
        float y = 5 * -Input.GetAxis("Mouse Y");
        Camera.main.transform.Rotate(0, x, 0);
    }

}