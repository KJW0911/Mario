using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private float jumptime;
    private bool jumpable;
    private int velocity;
    private int jumpspeed;
    private InputAction moveAction,jumpAction;
    private const float raycastorigin_offset=0.51f;
    private BoxCollider2D playerCollider;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {   velocity=10;
        jumpspeed=12;
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

        float length_of_sensor_ray_for_floors_jumpcheck=0.01f;
        float length_of_sensor_ray_for_walls=0.005f;

        Vector2 moveInput=moveAction.ReadValue<Vector2>();

        Vector3 rayhit_bottom_middle_jumpable_origin=transform.position+new Vector3(0,-1*playerCollider.size.y*raycastorigin_offset,0);
        Vector3 rayhit_bottom_right_jumpable_origin=transform.position+new Vector3(playerCollider.size.x*0.4f,-1*playerCollider.size.y*raycastorigin_offset,0);
        Vector3 rayhit_bottom_left_jumpable_origin=transform.position+new Vector3(-playerCollider.size.x*0.4f,-1*playerCollider.size.y*raycastorigin_offset,0);

        Vector3 rayhit_right_middle_wallcheck_origin=transform.position+new Vector3(playerCollider.size.x*raycastorigin_offset,0,0);
        Vector3 rayhit_left_middle_wallcheck_origin=transform.position+new Vector3(-1*playerCollider.size.x*raycastorigin_offset,0,0);

        
        RaycastHit2D rayhit_bottom_middle_jumpable = Physics2D.Raycast(rayhit_bottom_middle_jumpable_origin,Vector2.down,length_of_sensor_ray_for_floors_jumpcheck);
        RaycastHit2D rayhit_bottom_right_jumpable = Physics2D.Raycast(rayhit_bottom_right_jumpable_origin,Vector2.down,length_of_sensor_ray_for_floors_jumpcheck);
        RaycastHit2D rayhit_bottom_left_jumpable = Physics2D.Raycast(rayhit_bottom_left_jumpable_origin,Vector2.down,length_of_sensor_ray_for_floors_jumpcheck);
        
        
        RaycastHit2D rayhit_right_middle_wallcheck = Physics2D.Raycast(rayhit_right_middle_wallcheck_origin,Vector2.right,length_of_sensor_ray_for_walls);
        RaycastHit2D rayhit_left_middle_wallcheck = Physics2D.Raycast(rayhit_left_middle_wallcheck_origin,Vector2.left,length_of_sensor_ray_for_walls);

        float x_movedirection=moveInput.x;
        Vector3 movement=new Vector3(velocity*x_movedirection*Time.deltaTime,0,0);

        if (rayhit_left_middle_wallcheck)
        {
            if (x_movedirection < 0)
            {
                movement=new Vector3(0,0,0);
            }    
        }

        if (rayhit_right_middle_wallcheck)
        {
            if (x_movedirection > 0)
            {
                movement=new Vector3(0,0,0);
            }
        }

        
        transform.position+=movement;


        if (jumpAction.WasPressedThisFrame() && (rayhit_bottom_middle_jumpable||rayhit_bottom_left_jumpable||rayhit_bottom_right_jumpable))
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts=new ContactPoint2D[10];
        int contactsarraysize=collision.GetContacts(contacts);
        for(int i = 0; i < contactsarraysize; i++)
        {
            if (contacts[i].point.y >= (transform.position.y + playerCollider.size.y * 0.5))
            {
                jumpable=false;
            }
        }
    }
}
