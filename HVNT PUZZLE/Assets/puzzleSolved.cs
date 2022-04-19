using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRUFX;


public class puzzleSolved : MonoBehaviour
{

    [SerializeField]
    private GameObject H;
    [SerializeField]
    private GameObject V;
    [SerializeField]
    private GameObject N;
    [SerializeField]
    private GameObject T;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("H") && collision.gameObject.CompareTag("V") && collision.gameObject.CompareTag("N") && collision.gameObject.CompareTag("T"))
        {
            DebugManager.Instance.AddDebugMessage("HVNT PUZZLE SOLVED!");
        }
    }
}
