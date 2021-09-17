using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float gravityValue = -9.81f;

    public float moveSpeed;
    private float currentMoveSpeed;
    public float jumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        currentMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = CameraController.instance.transform.right * Input.GetAxis("Horizontal") + new Vector3(CameraController.instance.transform.forward.x, 0, CameraController.instance.transform.forward.z) * Input.GetAxis("Vertical");
        characterController.Move(Vector3.ProjectOnPlane(move.normalized, Vector3.up).normalized * Time.deltaTime * currentMoveSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButton("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
