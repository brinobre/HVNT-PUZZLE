using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace HVNTPUZZLE_MAC
{
    [RequireComponent(typeof(ARRaycastManager))]
    public class ARTapToPlaceObject : MonoBehaviour
    {
        public GameObject gameObjToInst;

        private GameObject spawnedObj;
        private ARRaycastManager aRRaycastManager;
        private Vector2 touchPos;

        static List<ARRaycastHit> hits = new List<ARRaycastHit>();

        private void Awake()
        {
            aRRaycastManager = GetComponent<ARRaycastManager>(); 
        }

        bool tryGetTouchPos(out Vector2 touchPos)
        {
            if(Input.touchCount > 0)
            {
                touchPos = Input.GetTouch(0).position;
                return true;
            }

            touchPos = default;
            return false;
        }

        void Update()
        {
            if (!tryGetTouchPos(out Vector2 touchPos))
                return;

            if(aRRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;

                if(spawnedObj == null)
                {
                    spawnedObj = Instantiate(gameObjToInst, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnedObj.transform.position = hitPose.position;
                }

            }
        }
    }
}

