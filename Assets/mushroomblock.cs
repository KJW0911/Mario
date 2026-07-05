using UnityEngine;

public class mushroomblock : MonoBehaviour
{
    float mushroomlinearvelocityX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        mushroomlinearvelocityX=10f;
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
        if (collision.gameObject.tag != "player")
        {
            
        }
    }
}
