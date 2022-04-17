using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]

public class PlacementController : MonoBehaviour
{


    public bool isPlaced = false;

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
            return true;
        }
        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (aRRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon) && !isPlaced)
        {
            var hitPose = hits[0].pose;

            Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            isPlaced = true;
        }
    }

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}
