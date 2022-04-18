using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRUFX;

namespace HVNTPUZZLE_MAC
{
    public class puzzleScript : MonoBehaviour
    {

        private Vector2 touchPos = default;

        private Vector3 startPos;
        private Vector3 endPos;
        private bool firstPos = false;
        private float angle;
        private rotateCylinder rotateCylinder;


        private void Awake()
        {
        }

        void Start()
        {
        
        }

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
                    if (raycastHit.collider.gameObject.CompareTag("RowColliders"))
                    {
                        if(firstPos == false)
                        {
                            startPos = raycastHit.point;
                            firstPos = true;
                            rotateCylinder = raycastHit.collider.gameObject.GetComponent<rotateCylinder>();
                        }

                        angle = raycastHit.point.y - startPos.y;
                        rotateCylinder.rotate(angle);
                        DebugManager.Instance.AddDebugMessage("Angle is" + angle.ToString());

                    }

                }
            }
        }
    }
}

