using System.Security.Cryptography;
using NUnit.Framework.Constraints;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class mushroomblock : MonoBehaviour
{
    private bool spawnable;
    public Tilemap tilemap;
    public GameObject mushroom;
    public TileBase usedMushroomblockTile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spawnable=true;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag!="Player"||!spawnable){return;}
        GameObject player=collision.gameObject;
        ContactPoint2D[] contacts=new ContactPoint2D[10];
        int contactsnum=collision.GetContacts(contacts);

        for(int i = 0; i < contactsnum; i++)
        {   
            if(!spawnable){return;}
            Vector2 contactpoint=contacts[i].point;
            BoxCollider2D playerCollider=player.GetComponent<BoxCollider2D>();
            if (contactpoint.y >= player.transform.position.y + playerCollider.size.y*0.5f)
            {
                Vector3Int blockcellpos=tilemap.WorldToCell(new Vector3(contactpoint.x,contactpoint.y+0.1f,0));
                Vector3Int mushroomcellpos=new Vector3Int(blockcellpos.x,blockcellpos.y+1,blockcellpos.z);
                Vector3 mushroompos=tilemap.GetCellCenterWorld(mushroomcellpos);
                Instantiate(mushroom,mushroompos,Quaternion.identity);
                spawnable=false;
                tilemap.SetTile(blockcellpos,usedMushroomblockTile);
            }
        }
    }
}
