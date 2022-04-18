using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HVNTPUZZLE_MAC
{
    public class Rotate : MonoBehaviour
    {

        /*
        public float speed = 90f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }*/

        private void Update()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    transform.Rotate(0f, 0f, touch.deltaPosition.y);
                }
            }
        }
    }
}
