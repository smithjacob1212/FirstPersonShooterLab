using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float sensitivity;
    public float sprintSpeed;

    private float moveFB;
    private float moveLR;
    private float rotX;
    private float rotY;
    private CharacterController cc;
    private Camera _playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cc = gameObject.GetComponent<CharacterController>();
        _playerCamera = gameObject.transform.GetChild(0).transform.gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float movementSpeed = speed;

        // Checking to see if the player is honding down the sprint key
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = sprintSpeed;
        }

        // Checking to see if player is no longer holding down the sprint key
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = speed;
        }

        // Grabbing axis and mouse movement
        moveFB = Input.GetAxis("Vertical") * movementSpeed;
        moveLR = Input.GetAxis("Horizontal") * movementSpeed;
        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Clamp the Y rotation
        rotY = Mathf.Clamp(rotY, -60f, 60f);

        // Creating the movement vector
        Vector3 movement = new Vector3(moveLR, 0, moveFB);

        // Rotating the player camera
        transform.Rotate(0, rotX, 0);
        _playerCamera.transform.localRotation = Quaternion.Euler(rotY, 0, 0);

        movement = transform.rotation * movement;
        cc.Move(movement * Time.deltaTime);


    }

}
