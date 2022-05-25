using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameInfo : MonoBehaviour
{
    [SerializeField]
    public Player myPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        if(myPlayer!=null)
        {
            if (myPlayer.playerHp <= 0)
                gameObject.GetComponent<Text>().text = "You lost";
            else
                gameObject.GetComponent<Text>().text = "You win";
        }
    }
}
