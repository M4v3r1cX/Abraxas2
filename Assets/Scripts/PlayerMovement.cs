using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 20f;
    private CharacterController _charController;
    public Animator camAnim;
    private bool isWalking;

    private Vector3 inputVector;
    private Vector3 movementVector;
    private float myGravity = -10f;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        MovePlayer();
        CheckForHeadbob();

        camAnim.SetBool("isWalking", isWalking);
    }

    void GetInput()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        inputVector = transform.TransformDirection(inputVector);

        movementVector = (inputVector * playerSpeed) + (Vector3.up * myGravity);
    }

    void MovePlayer()
    {
        _charController.Move(movementVector * Time.deltaTime);
    }

    void CheckForHeadbob()
    {
        if (_charController.velocity.magnitude > 0.1f)
        {
            isWalking = true;
        } else
        {
            isWalking = false;
        }
    }
}
