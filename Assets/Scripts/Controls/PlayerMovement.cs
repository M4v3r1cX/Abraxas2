using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera _cam;
    public LayerMask movementMask;
    private CharacterController _charController;
    public Animator camAnim;
    private bool isWalking;

    private Vector3 inputVector;
    private Vector3 movementVector;
    private float myGravity = -10f;
    public float playerSpeed = 20f;
    private Interactable focus;
    Inventory inventory;

    void Start()
    {
        _cam = Camera.main;
        _charController = GetComponent<CharacterController>();
        inventory = Inventory.instance;
    }

    void Update()
    {
        if (inventory.inventoryActive)
        {
            return;
        }

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

        // left maus
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

        // rite maus
        if (Input.GetMouseButtonDown(1))
        {
            
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();    
            }
            
            focus = newFocus;
        }
        
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();    
        }
        
        focus = null;
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
