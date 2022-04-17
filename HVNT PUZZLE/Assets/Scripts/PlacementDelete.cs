using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlacementObject))]
public class PlacementDelete : MonoBehaviour
{

    private PlacementObject placementObject;


    private void Awake()
    {
        placementObject = GetComponent<PlacementObject>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (placementObject.IsSelected)
        {
            Destroy(placementObject.GetComponent<GameObject>());
        }
    }
}
