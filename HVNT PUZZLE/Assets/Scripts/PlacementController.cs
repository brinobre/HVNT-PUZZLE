using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using XRUFX;

namespace HVNTPUZZLE_MAC
{
    [RequireComponent(typeof(ARRaycastManager))]

    public class PlacementController : MonoBehaviour
    {

        private bool fingerHasBeenReleased = false;

        public Vector3 oldPos;

        private Touch pressed;

        private int fingerId = -1;

        public bool isPlaced = false;

        public GameObject puzzleObj;

        [SerializeField]
        private GameObject placedPrefab;

        public GameObject PlacedPrefab
        {
            get
            {
                return placedPrefab;
            }
            set
            {
                placedPrefab = value;
            }
        }

        private ARRaycastManager aRRaycastManager;

        private void Awake()
        {
            aRRaycastManager = GetComponent<ARRaycastManager>();
        }

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if(Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                fingerId = Input.GetTouch(index: 0).fingerId;
                return true;
            }
            touchPosition = default;
            return false;
        }

        IEnumerator activateBool()
        {
            yield return new WaitForSeconds(.5f);
            isPlaced = true;

        }

        // Update is called once per frame
        void Update()
        {



            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;


            if (fingerHasBeenReleased)
            {
                DebugManager.Instance.AddDebugMessage("working");

                Touch touch = Input.GetTouch(0);


                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit raycastHit;


            if (aRRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon) && !isPlaced && !fingerHasBeenReleased)
            {
                var hitPose = hits[0].pose;

                oldPos = hitPose.position;
                Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                    isPlaced = true;

            
            }
                if (Physics.Raycast(ray, out raycastHit) && fingerHasBeenReleased && isPlaced)
                {
                    DebugManager.Instance.AddDebugMessage("Raycast hit success");
                    DebugManager.Instance.AddDebugMessage(raycastHit.ToString());
                    Destroy(raycastHit.collider.gameObject);
                    //raycastHit.collider.gameObject.SetActive(false);
                    Instantiate(puzzleObj, oldPos, Quaternion.identity);
                }
            }



            if (Input.touches[fingerId].phase == TouchPhase.Ended)
            {
                fingerHasBeenReleased = true;
            }
        }

        static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    }
}

