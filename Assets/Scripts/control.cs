using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float x =  Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Debug.Log("x:" + x + " y:" + y);
        float posx = player.gameObject.transform.position.x;
        float posy = player.gameObject.transform.position.y;
        posx += 2 * x;
        posy += 2 * y;

        
        player.gameObject.transform.position.Set(posx, posy, 0) ;
        player.gameObject.transform.Translate(posx, posy, 0);
        */

        float x =  Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 pos = transform.position;
        pos.x = pos.x + 2f * x * Time.deltaTime;
        pos.y = pos.y + 2f * y * Time.deltaTime;
        transform.position = pos;

    }
}
