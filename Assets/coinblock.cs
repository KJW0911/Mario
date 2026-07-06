using UnityEngine;
using UnityEngine.Tilemaps;

public class coinblock : MonoBehaviour
{
    public GameObject manager;
    private manager managerScript;
    public Tilemap tilemap;
    public TileBase usedcoinblock;
    private bool isOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOn=true;
        managerScript=manager.GetComponent<manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag!="Player"||!isOn){return;}
        GameObject player=collision.gameObject;
        ContactPoint2D[] contacts=new ContactPoint2D[10];
        int contactsnum=collision.GetContacts(contacts);

        for(int i = 0; i < contactsnum; i++)
        {   
            Vector2 contactpoint=contacts[i].point;
            BoxCollider2D playerCollider=player.GetComponent<BoxCollider2D>();
            if (contactpoint.y >= player.transform.position.y + playerCollider.size.y*0.5f)
            {
                Vector3Int blockcellpos=tilemap.WorldToCell(new Vector3(contactpoint.x,contactpoint.y+0.1f,0));
                tilemap.SetTile(blockcellpos,usedcoinblock);
                managerScript.addcoin(1);
                isOn=false;
                return;
            }
        }
    }
}
