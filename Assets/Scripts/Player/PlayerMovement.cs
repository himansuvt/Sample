using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public bool isFPV = false; // Flag to determine if in first-person view

    Vector3 movementV;
    Animator animator;
    Rigidbody playerRb;
    int floorMask;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Walk(h, v);
        if (!isFPV)
        {
            Aiming();
            Animate(h, v);
        }
        
    }

    void Walk(float h, float v)
    {
        movementV.Set(h, 0f, v);
        movementV = movementV.normalized * speed * Time.deltaTime;
        playerRb.MovePosition(transform.position + movementV);
    }

    void Aiming()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, 100f, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);
            playerRb.MoveRotation(newRotatation);
        }
    }

    void Animate(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        animator.SetBool("IsWalking", walking);
    }
}
