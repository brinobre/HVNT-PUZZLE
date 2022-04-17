using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using XRUFX;


namespace HVNTPUZZLE_MAC
{
    public class spawnPuzzle : MonoBehaviour
    {
        private Vector2 touchPos = default;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            //  if(Input.touchCount > 0)
            // {

            int fingerCount = 0;

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    fingerCount++;
            }

            if (fingerCount > 0)
            {
                DebugManager.Instance.AddDebugMessage("working");

                Touch touch = Input.GetTouch(0);

                touchPos = touch.position;

                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit raycastHit;

                    if(Physics.Raycast(ray, out raycastHit))
                    {
                        Destroy(raycastHit.collider.gameObject);
                    }
            }
        }
    }
}

