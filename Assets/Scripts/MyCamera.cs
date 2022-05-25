using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0, 1, -2);
    [SerializeField]
    private Vector3 rot = new Vector3(0, 0, 0);
    [SerializeField]
    private float rotSpeedX = 30;
    [SerializeField]
    private float rotSpeedY = 50;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float roty = Input.GetAxis("Mouse X");
            float rotx = -Input.GetAxis("Mouse Y");
            rot += new Vector3(rotx*rotSpeedX*Time.deltaTime, roty * rotSpeedY * Time.deltaTime, 0);
            rot.x = Mathf.Clamp(rot.x, -15, 45);
            transform.rotation = Quaternion.Euler(rot);
            transform.position = player.transform.position + transform.rotation * offset;
        }
    }
}
