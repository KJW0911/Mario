using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    private int coin;
    private bool isclear;
    public GameObject player;
    private bool flagOn;
    private const float death_y_pos=-20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        coin=0;
        isclear=false;
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
            Debug.Log("Game Over");
            Time.timeScale=1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void setflagOn()
    {
        if(flagOn){return;}
        flagOn=true;
        Debug.Log("Touched the flag!");
    }
    public void cleared()
    {
        isclear=true;
        Time.timeScale=0f;
        Debug.Log("Cleared!");
    }
    public void addcoin(int num)
    {
        coin+=num;
        Debug.Log("Coin +"+num+" >> CurrentCoin: "+coin);
        
    }
}
