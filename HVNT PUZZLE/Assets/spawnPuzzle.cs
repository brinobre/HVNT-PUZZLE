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

        PlacementController placementController;
       
        private Vector2 touchPos = default;
        private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();

        public GameObject puzzleObj;

        // Start is called before the first frame update
        void Start()
        {
          
        }

        // Update is called once per frame
        void Update()
        {

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
                DebugManager.Instance.AddDebugMessage(touchPos.ToString());


                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit))
                {
                    DebugManager.Instance.AddDebugMessage("Raycast hit success");
                    DebugManager.Instance.AddDebugMessage(raycastHit.ToString());
                    //Destroy(raycastHit.collider.gameObject);
                    raycastHit.collider.gameObject.SetActive(false);
                    //Instantiate(puzzleObj, placementController.oldPos, Quaternion.identity);
                }
            }
        }
    }
}

