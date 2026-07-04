using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    public GameObject player;
    private const float death_y_pos=-20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }
    void Start()
    {
        player.transform.position=new Vector3(0,-4,0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerpos=player.transform.position;

        if (playerpos.y < death_y_pos)
        {
            Time.timeScale=1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
