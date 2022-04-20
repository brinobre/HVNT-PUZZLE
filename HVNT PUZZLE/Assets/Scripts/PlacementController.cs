using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using XRUFX;

namespace HVNTPUZZLE_MAC
{
    [RequireComponent(typeof(ARRaycastManager))]
    [RequireComponent(typeof(PlacementObject))]

    public class PlacementController : MonoBehaviour
    {

        public enum GameStates
        {
            PLACE_OBJECT,
            REPLACE_OBJECT,
            PICKUP_OBJECT,
            PICKEDUP_OBJECT
        }

        private GameStates currentState = GameStates.PLACE_OBJECT;

        private bool fingerHasBeenPressed = false;

        public Vector3 oldPos;

        public Text mainText;

        private int fingerId = -1;

        private Vector3 targetPos = new Vector3(0.06f, 0f, 0.6f);
        private Vector3 targetRot = new Vector3(0f, 90f, -90f);

        public bool isPlaced = false;
        private bool puzzleObjReady = false;

        public GameObject puzzleObj;
        private GameObject spawnedObj;

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

        private void Start()
        {
            spawnedObj = Instantiate(puzzleObj, Vector3.zero, Quaternion.identity);
            spawnedObj.SetActive(false);
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

        private bool canBeActivated = false;

        IEnumerator ActivateTimer()
        {
            yield return new WaitForSeconds(1);
            canBeActivated = true;

        }

        // Update is called once per frame
        void Update()
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            DebugManager.Instance.AddDebugMessage("finger Is Pressing");

            Touch touch = Input.GetTouch(0);


            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit raycastHit;


            if (aRRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon) && !isPlaced && !fingerHasBeenPressed && currentState == GameStates.PLACE_OBJECT)
            {
                var hitPose = hits[0].pose;

                oldPos = hitPose.position;

                DebugManager.Instance.AddDebugMessage("Object instantiated");


                Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                mainText.text = "Klicka på loggan för att låsa upp pusslet!";

                spawnedObj.transform.position = hitPose.position;
                spawnedObj.transform.rotation = hitPose.rotation;
                isPlaced = true;

                StartCoroutine(ActivateTimer());
                currentState = GameStates.REPLACE_OBJECT;
            }
            else if (Physics.Raycast(ray, out raycastHit) && fingerHasBeenPressed && isPlaced && currentState == GameStates.REPLACE_OBJECT)
            {
                DebugManager.Instance.AddDebugMessage("Raycast hit success");
                DebugManager.Instance.AddDebugMessage(raycastHit.ToString());

                if (raycastHit.collider.CompareTag("HVNT") && canBeActivated)
                {
                    raycastHit.collider.gameObject.SetActive(false);
                    spawnedObj.SetActive(true);
                    mainText.text = "Klicka på pusslet att plocka upp det";
                    puzzleObjReady = true;
                    currentState = GameStates.PICKUP_OBJECT;
                }


            }
            else if (puzzleObjReady && !fingerHasBeenPressed && currentState == GameStates.PICKUP_OBJECT)
            {
                spawnedObj.transform.position = targetPos;
                spawnedObj.transform.rotation = Quaternion.Euler(targetRot.x, targetRot.y, targetRot.z);
                mainText.text = "Dra pusslet för att lösa det!";
                currentState = GameStates.PICKEDUP_OBJECT;
            }
            else
            {
            }

            if (currentState == GameStates.REPLACE_OBJECT && touch.phase == TouchPhase.Ended)
            {
                fingerHasBeenPressed = true;
            }
            else if (currentState == GameStates.PICKUP_OBJECT && touch.phase == TouchPhase.Ended)
            {
                fingerHasBeenPressed = false;

            }
  
        }
        static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    }
}

