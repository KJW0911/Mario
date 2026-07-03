using UnityEngine;

public class mainCamera : MonoBehaviour
{
    private GameObject playerObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerObj=GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition=playerObj.transform.position;
        transform.position=new Vector3(playerPosition.x,transform.position.y,transform.position.z);
    }
}
