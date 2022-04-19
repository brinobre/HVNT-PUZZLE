using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        public Button placementBtn;
        public bool isPlaced = false;

        static List<ARRaycastHit> hits = new List<ARRaycastHit>();

        private void Awake()
        {
            aRRaycastManager = GetComponent<ARRaycastManager>();
        }

        public void restartBtn()
        {
            isPlaced = false;
            Destroy(spawnedObj);
            spawnedObj = null;
        }

        bool tryGetTouchPos(out Vector2 touchPos)
        {
            if (Input.touchCount > 0)
            {
                touchPos = Input.GetTouch(0).position;
                return true;
            }

            touchPos = default;
            return false;
        }

        void placeObject()
        {

            if (!tryGetTouchPos(out Vector2 touchPos))
                return;

            if (aRRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon) && !isPlaced)
            {
                var hitPose = hits[0].pose;

                if (spawnedObj == null)
                {
                    spawnedObj = Instantiate(gameObjToInst, hitPose.position, hitPose.rotation);
                    isPlaced = true;
                }
            }
        }

        void Update()
        {
            placeObject();
        }
    }
}

