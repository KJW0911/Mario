using UnityEngine;

public class flag : MonoBehaviour
{
    public GameObject manager;
    manager managerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        managerScript=manager.GetComponent<manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag!="Player"){return;}
        managerScript.setflagOn();
    }
}
