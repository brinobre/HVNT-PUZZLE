using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRUFX;

namespace HVNTPUZZLE_MAC
{
    public class ActivateRow : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    if(hit.transform.tag == "RowCol1" || hit.transform.tag == "RowCol2" || hit.transform.tag == "RowCol3" || hit.transform.tag == "RowCol4")
                    {
                        DebugManager.Instance.AddDebugMessage(hit.transform.gameObject.tag.ToString());
                       var objectScript = hit.collider.GetComponent<Rotate>();
                        
                    }
                }
            }


        }
    }
}

