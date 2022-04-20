using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRUFX;


namespace HVNTPUZZLE_MAC
{
    public class puzzleSolved : MonoBehaviour
    {

        private PlacementController placementController;

        [SerializeField]
        private GameObject H;
        [SerializeField]
        private GameObject V;
        [SerializeField]
        private GameObject N;
        [SerializeField]
        private GameObject T;
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
                placementController.mainText.text = "Grattis jvgare! Du l?ste pusslet!";
            }
        }
    }

}

