using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float mouseSenX = 250f;
    public float mouseSenY = 250f;
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpForce;
    public float pickUpDist = 2.5f;

    Rigidbody rb;
    Transform camt;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float verticalLookRotation;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camt = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSenX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSenY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -61, 90);
        camt.localEulerAngles = Vector3.left * verticalLookRotation;

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed);
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

        if (Input.GetButton("Jump"))
        {
            transform.position += 10.0f * Time.fixedDeltaTime * Vector3.up;
        }

        if (Input.GetButton("Crouch"))
        {
            transform.position -= 10.0f * Time.fixedDeltaTime * Vector3.up;
        }
    }
}
