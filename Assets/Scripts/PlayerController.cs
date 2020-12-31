using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float gravity;

    private bool isWalking = false;

    private Animator animator;
    private CharacterController cc;


    private Vector3 moveDir;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Axes x,z
        moveDir = new Vector3(-Input.GetAxis("Horizontal") * moveSpeed, moveDir.y, -Input.GetAxis("Vertical") * moveSpeed);

        //Movement axe Y/Jump
        if (Input.GetButtonDown("Jump") && cc.isGrounded) 
        {
            moveDir.y = jumpForce;
        }

        //Add gravity
        moveDir.y -= gravity * Time.deltaTime;

        //Rotation
        if(moveDir.x != 0 || moveDir.z != 0) 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x,0,moveDir.z)), 0.15f);
            isWalking = true;
        }
        else 
        {
            isWalking = false;
        }

        animator.SetBool("isWalking", isWalking);

        //Move CharacterController
        cc.Move(moveDir * Time.deltaTime);
    }
}
