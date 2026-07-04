using UnityEngine;
using UnityEngine.Tilemaps;

public class breaking : MonoBehaviour
{
    private Tilemap tilemap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilemap=GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player;
        if (collision.gameObject.tag == "Player")
        {
            player=collision.gameObject;
        }
        else
        {
            return;
        }

        Vector3 playerPosition=player.transform.position;
        Vector2 playerCollidersize=player.GetComponent<BoxCollider2D>().size;
        ContactPoint2D[] contacts=new ContactPoint2D[10];
        int contactsnum=collision.GetContacts(contacts); 
        
        for(int i=0;i<contactsnum;i++){
            Vector2 contactpoint=contacts[i].point;
            if (contactpoint.y >= (player.transform.position.y + playerCollidersize.y * 0.5))
            {
                Vector3 blockworldpos=new Vector3(contactpoint.x,contactpoint.y+0.5f,0);
                Vector3Int cellpos=tilemap.WorldToCell(blockworldpos);
                tilemap.SetTile(cellpos,null);
            }
        }
    }
}
