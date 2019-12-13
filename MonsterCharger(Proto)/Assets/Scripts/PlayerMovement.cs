using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    Animator anim;
    public float speed = 3.0f;
    public float runSpeed = 6.0f;
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
            

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0){
            anim.SetBool("Walking", true);
        }
        else {
            anim.SetBool("Walking", false);
        }

        

        // Walking Animation
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = new Vector3(0, 0, 1);
            anim.SetInteger("Mcondition", 1);
        }
        // Anim is IDLE
        if (Input.GetKeyUp(KeyCode.W))
        {
            moveDirection = new Vector3(0, 0, 1);
            anim.SetInteger("Mcondition", 0);
        }



        if (Input.GetKeyDown(KeyCode.A))
        {
            moveDirection = new Vector3(0, 0, 1);
            anim.SetInteger("Mcondition", 2);
        }
        // Anim is IDLE
        if (Input.GetKeyUp(KeyCode.A))
        {
            moveDirection = new Vector3(0, 0, 1);
            anim.SetInteger("Mcondition", 0);
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            moveDirection = new Vector3(0, 0, 1);
            anim.SetInteger("Mcondition", 3);
        }
        // Anim is IDLE
        if (Input.GetKeyUp(KeyCode.S))
        {
            moveDirection = new Vector3(0, 0, 1);
            anim.SetInteger("Mcondition", 0);
        }



        if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = new Vector3(0, 0, 1);
            anim.SetInteger("Mcondition", 4);
        }
        // Anim is IDLE
        if (Input.GetKeyUp(KeyCode.D))
        {
            moveDirection = new Vector3(0, 0, 1);
            anim.SetInteger("Mcondition", 0);
        }



        //Anim Attack
        if (Input.GetMouseButtonDown(0)) {
            anim.SetTrigger("Attacking");
        }
        //Anim reset anim
        if (Input.GetMouseButtonUp(0)) {
            anim.ResetTrigger("Attacking");
        }


        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            anim.SetBool("Running", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            anim.SetBool("Running", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool("Crouching", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetBool("Crouching", false);
        }


        if (anim.GetBool("Running")){
            moveDirection *= runSpeed;
        }
        else { 
            moveDirection *= speed;
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