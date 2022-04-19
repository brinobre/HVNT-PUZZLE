using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XRUFX;


namespace HVNTPUZZLE_MAC
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField]
        private GameObject Row1;
        [SerializeField]
        private GameObject Row2;
        [SerializeField]
        private GameObject Row3;
        [SerializeField]
        private GameObject Row4;

        public GameObject colliderObj;

        private bool Row1Active = true;
        private bool Row2Active = false;
        private bool Row4Active = false;

        PlacementController placementController;
        private Text mainText;

        private void Awake()
        {
            //mainText = placementController.mainText;
        }

        private void Update()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);


                if (touch.phase == TouchPhase.Moved)
                {
                    if (Row1Active)
                    {
                        Row1.gameObject.transform.Rotate(0f, 0f, touch.deltaPosition.y);
                    }
                    else if (Row2Active)
                    {
                        Row2.gameObject.transform.Rotate(0f, 0f, touch.deltaPosition.y);
                    } else if (Row4Active)
                    {
                        Row4.gameObject.transform.Rotate(0f, 0f, touch.deltaPosition.y);
                    }
                    placementController.mainText.text = "Grattis jvgare! Du löste pusslet!";
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "codeInputArea" && Row1Active)
            {
                DebugManager.Instance.AddDebugMessage("collided successfully");
                Row1Active = false;
                Row2Active = true;
            } else if (collision.gameObject.name == "codeInputArea" && Row2Active)
            {
                Row2Active = false;
                Row4Active = true;
            }
            else if (Row4.gameObject.CompareTag("codeInputArea"))
            {
                Row4Active = false;
            }
        }


    }
}
