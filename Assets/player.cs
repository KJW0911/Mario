using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private float jumptime;
    private bool jumpable;
    private int velocity;
    private int jumpspeed;
    private InputAction moveAction,jumpAction;
    private const float raycastorigin_offset_x=0.51f;
    private BoxCollider2D playerCollider;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {   velocity=10;
        jumpspeed=10;
        jumptime=0;
        jumpable=false;  

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        playerCollider=GetComponent<BoxCollider2D>();  
    }

    // Update is called once per frame
    void Update()
    {

        float length_of_sensor_ray_for_floors_jumpcheck=0.007f;
        

        Vector2 moveInput=moveAction.ReadValue<Vector2>();
        Vector3 rayhit_bottom_middle_jumpable_origin=transform.position+(-Vector3.up*playerCollider.size.y*raycastorigin_offset_x);
        RaycastHit2D rayhit_bottom_middle_jumpable = Physics2D.Raycast(rayhit_bottom_middle_jumpable_origin,Vector2.down,length_of_sensor_ray_for_floors_jumpcheck);
        


        transform.position+=new Vector3(velocity*moveInput.x*Time.deltaTime,0,0);


        if (jumpAction.WasPressedThisFrame() && rayhit_bottom_middle_jumpable)
        {
            jumpable=true;
        }

        if (jumpAction.WasReleasedThisFrame())
        {
            Debug.Log("Released");

            jumpable=false;
            jumptime=0;
        }

        if (jumpable&&(jumptime<=0.3))
        {
            rb.linearVelocityY=jumpspeed;
            jumptime+=Time.deltaTime;
        }
        transform.rotation=Quaternion.identity;

        
    }
}
