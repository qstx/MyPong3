using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Trap : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "t")
        {
            other.transform.position = NetworkManager.singleton.GetStartPosition().position;
            other.gameObject.GetComponent<Player>().CmdLoseHealth();
            other.gameObject.GetComponent<Player>().CmdRandColor();
            //other.gameObject.GetComponent<Player>().playerColor = new Color(Random.value, Random.value, Random.value);
        }
    }
}
