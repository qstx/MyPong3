using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class HealthShow : NetworkBehaviour
{
    [SerializeField]
    private Text healthText;
    [SerializeField]
    public List<Player> players = new List<Player>();
    [SyncVar(hook =nameof(OnHealthInfoChanged))]
    public string healthInfo;
    private void OnHealthInfoChanged(string o,string n)
    {
        healthText.text = n;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
