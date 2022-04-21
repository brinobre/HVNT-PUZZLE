using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRUFX;

namespace HVNTPUZZLE_MAC
{
    public class WheelHandler : MonoBehaviour
    {
        /// <summary>
        /// Anger hur stort steg av vinkel vi kan ha. ju st?rre v?rde desto snabbare kommer
        /// hjulet rotera n?r vi drar fingret och ju mindre v?rde dvs < 1.0f desto l?ngsammare
        /// rotation kommer vi f?
        /// </summary>
        [SerializeField]
        private float rotationSpeed = 1.0f;


        private float currentAngle = 0.0f;
        private float previousAngle = 0.0f;

        private Vector2 startPos;
        private Vector2 endPos;
        private WheelRotator wr;
        private Touch touch;

        private bool beginPressed = false;

        bool TryGetTouchPosition(out Vector2 touchPos)
        {
            if (Input.touchCount > 0)
            {
                touchPos = Input.GetTouch(index: 0).position;
                return true;
            }

            touchPos = default;
            return false;
        }


        // Update is called once per frame
        void Update()
        {
            // Kolla om vi ?verhuvudtaget har tryckt p? sk?rmen
            if (!TryGetTouchPosition(out Vector2 touchPosition))
            {
                return;
            }

            touch = Input.GetTouch(0);

            Ray rayOrigin = Camera.main.ScreenPointToRay(touchPosition);

            // Kolliderar vi med ett objekt ?
            if (Physics.Raycast(rayOrigin, out RaycastHit hitInfo, Mathf.Infinity))
            {
                // DebugManager.Instance.AddDebugMessage("Its an object!");

                // L?s av om detta ?r ett objekt med en collider
                // alternativ: vi kan l?sa av med en viss tag om vi vill!
                if (hitInfo.collider.gameObject.CompareTag("Wheel"))
                {
                    if (beginPressed == false)
                    {
                        wr = hitInfo.collider.gameObject.GetComponent<WheelRotator>();
                        startPos = touchPosition;
                        beginPressed = true;
                    }

                    // Ber?kna f?rst skillnaden i y-led som vi f?r fingrets position p? touchsk?rmen
                    float deltaAngle = (touchPosition.y - startPos.y);
                    
                    // Ber?knar vilken vinkel hjulet kommer ha baserat ? 
                    // nuvarande vinkeln som hjulet hade fr?n f?reg?ende fall pluss
                    // skillnaden av hur l?ngt och i vilken riktning vi drog v?rt finger p? sk?rmen
                    // g?nger rotationshastigheten som avg?r hur "snabbt" hjulet kommer rotera
                    currentAngle = previousAngle + deltaAngle*rotationSpeed;

                    DebugManager.Instance.AddDebugMessage("wheels angle = " + currentAngle.ToString());
                    
                    if (wr != null)
                        wr.SetRotate(currentAngle);

                }
            }

            if (beginPressed && touch.phase == TouchPhase.Ended)
            {
                DebugManager.Instance.AddDebugMessage("Finger has been released from screen");
                beginPressed = false;
                previousAngle = currentAngle;
            }
            
        }

        
    }
}