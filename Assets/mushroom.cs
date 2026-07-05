
using UnityEngine;



public class mushroom : MonoBehaviour
{
    private float mushroomVelocityX=3f;
    private Rigidbody2D rb;
    private CircleCollider2D mushroomcollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

    }
    void Start()
    {
        mushroomcollider=GetComponent<CircleCollider2D>();
        rb=GetComponent<Rigidbody2D>();
        rb.linearVelocityX=mushroomVelocityX;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX=mushroomVelocityX;
        transform.rotation=Quaternion.identity;
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "player")
        {
            ContactPoint2D[] contacts=new ContactPoint2D[10];
            for(int i = 0; i < collision.GetContacts(contacts); i++)
            {
                float bumpoffset=0.05f;
                Vector2 contactpoint =contacts[i].point;
                
                if((mushroomVelocityX>0)&&(contactpoint.x>=transform.position.x+mushroomcollider.radius-bumpoffset)|| (mushroomVelocityX<0)&&contactpoint.x<=transform.position.x-mushroomcollider.radius+bumpoffset)
                {
                    mushroomVelocityX*=-1;
                }
            }
        }
    }
}