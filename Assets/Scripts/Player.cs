using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private float moveSpeed = 30;
    [SerializeField]
    private float colSpeed = 30;
    private MyCamera myCam;
    public GameObject floatingInfo;
    public TextMesh nameText;
    private Material playMaterialClone;
    [SerializeField]
    private GameObject EffHitPrefab;
    [SerializeField]
    private GameObject gameInfo;
    [SerializeField]
    private HealthShow healthShow;
    [SyncVar(hook =nameof(OnPlayerNameChanged))]
    public string playerName;
    [SyncVar(hook =nameof(OnPlayerColorChanged))]
    public Color playerColor;
    [SyncVar(hook = nameof(OnPlayerHpChanged))]
    public int playerHp;
    private void OnPlayerNameChanged(string o,string n)
    {
        nameText.text = n;
    }
    private void OnPlayerColorChanged(Color o, Color n)
    {
        nameText.color = n;
        playMaterialClone = new Material(GetComponent<Renderer>().material);
        playMaterialClone.SetColor("_EmissionColor", n);
        GetComponent<Renderer>().material = playMaterialClone;
    }
    private void OnPlayerHpChanged(int o, int n)
    {

        CmdUpdateHp();
    }
    [Command]
    public void CmdLoseHealth()
    {
        --playerHp;
        Debug.Log("CmdLoseHealth");
        transform.position = NetworkManager.singleton.GetStartPosition().position;
    }
    [Command]
    public void CmdRandColor()
    {
        playerColor = new Color(Random.value, Random.value, Random.value);
    }
    [Command]
    private void CmdUpdateHp()
    {
        string tmpstr = "";
        foreach (Player player in healthShow.players)
        {
            if (player != null)
                tmpstr += $"{player.playerName}:{player.playerHp}\n";
            if (player.playerHp <= 0)
            {
                StartCoroutine(ServerCoroutine());
                RpcGameover();
            }
        }
        healthShow.healthInfo = tmpstr;
    }
    static IEnumerator ServerCoroutine()
    {
        yield return new WaitForSeconds(5);
        NetworkServer.Shutdown();
    }
    [ClientRpc]
    private void RpcGameover()
    {
        string tmpstr = "";
        foreach(Player player in healthShow.players)
        {
            tmpstr += player.playerName + $"{(player.playerHp <= 0 ? " Lose!":"Win!")}\n";
        }
        gameInfo.GetComponent<Text>().text = tmpstr;
        gameInfo.SetActive(true);
        //StartCoroutine(ClientCoroutine());
    }
    static IEnumerator ClientCoroutine()
    {
        yield return new WaitForSeconds(5);
        NetworkClient.Shutdown();
    }
    [Command]
    private void CmdSetupPlayer(string name,Color color)
    {
        playerName = name;
        playerColor = color;
        string tmpstr = "";
        foreach (Player player in healthShow.players)
            if (player != null)
                tmpstr += $"{player.playerName}:{player.playerHp}\n";
        healthShow.healthInfo = tmpstr;
    }
    private void Awake()
    {
        gameInfo = GameObject.Find("GameInfo");
        gameInfo.GetComponent<Text>().text = "";
        healthShow = FindObjectOfType<HealthShow>();
        for(int i = healthShow.players.Count - 1; i >= 0; --i)
        {
            if (healthShow.players[i] == null)
                healthShow.players.RemoveAt(i);
        }
        healthShow.players.Add(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnStartLocalPlayer()
    {
        gameInfo.GetComponent<Text>().text = "";
        //gameInfo = FindObjectOfType<GameInfo>();
        //gameInfo.gameObject.SetActive(false);
        //gameInfo.myPlayer = this;
        //gameInfo.SetActive(false);
        myCam = Camera.main.GetComponent<MyCamera>();
        myCam.player = transform;
        CmdSetupPlayer($"Player{Random.Range(1, 999)}", new Color(Random.value, Random.value, Random.value));
    }
    // Update is called once per frame
    void Update()
    {
        floatingInfo.transform.position = transform.position;
        floatingInfo.transform.LookAt(Camera.main.transform);
        if (!isLocalPlayer)
        {
            return;
        }
        //if (playerHp <= 0)
        //    NetworkClient.Shutdown();
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * moveSpeed * Time.fixedDeltaTime;
        GetComponent<Rigidbody>().AddForce(myCam.transform.rotation * input);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "t")
        {
            Vector3 force = (collision.transform.position - transform.position) * colSpeed;
            collision.rigidbody.AddForce(force);
            GameObject eff = Instantiate(EffHitPrefab, (collision.transform.position + transform.position) / 2, Quaternion.identity);
            Destroy(eff, 1);
        }
    }
}
