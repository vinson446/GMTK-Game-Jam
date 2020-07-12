using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController charController;

    [Header("Player Settings")]
    public float moveSpeed;
    public float sprintSpeed;
    public float jumpForce;
    public float rotationSmoothing;

    public Transform cameraTransform;
    public float gravity = 1000;

    public Vector3 moveDirection;
    Vector3 jumpVelocity;
    float tmpSpeed;

    [Header("Grounded Checks")]
    public bool isGrounded;
    public Transform groundChecker;
    public float groundCheckerRadius = 0.5f;
    public LayerMask groundCheckerLayer;

    //
    /*
    public CharacterController controller;
    public Transform camera;
    public float moveSpeed = 6f;


    public float turnSpeed = 0.1f;

    float turnVelocity;
    */

    void Start()
    {
        Cursor.visible = false;

        charController = GetComponent<CharacterController>();
        tmpSpeed = moveSpeed;

    }

    void Update()
    {
        /*
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; 

        if(direction.magnitude>= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg+ camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
        */
        MovementWithCharacterController();

    }

    void MovementWithCharacterController()
    {
        // check if grounded
        isGrounded = Physics.CheckBox(groundChecker.position, groundChecker.localScale, Quaternion.identity, groundCheckerLayer);
        if (isGrounded && jumpVelocity.y < 0)
        {
            jumpVelocity.y = 0;
        }

        // movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = cameraTransform.TransformDirection(movement);
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);

        // sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            tmpSpeed = sprintSpeed;
        }
        else
            tmpSpeed = moveSpeed;

        charController.Move(moveDirection * Time.deltaTime * tmpSpeed);

      
        // rotation
        
        if (movement != Vector3.zero)
        {
            transform.forward = moveDirection;
        }
        /*
        if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            var dir = moveDirection;
            dir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotationSmoothing);
        }
        */

        // jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            jumpVelocity.y = jumpForce;
        }

        // apply gravity
        jumpVelocity.y += gravity * Time.deltaTime;

        charController.Move(jumpVelocity * Time.deltaTime);
    }

    // for character controller's/rigidbody's grounded check
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundChecker.position, groundChecker.localScale * 2);
    }
}
