using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRUFX;

namespace HVNTPUZZLE_MAC
{
    public class WheelRotator : MonoBehaviour
    {
        [SerializeField]
        private GameObject wheelToRotate;

        [SerializeField]
        private List<GameObject> wheels;

        private void Start()
        {
            wheels = new List<GameObject>();
        }

        [SerializeField]
        private Vector3 rotationAxis = new Vector3(1f, 0f, 0f);

        public void Rotate(float angle)
        {
            wheelToRotate.transform.Rotate(rotationAxis, angle);
        }

        public void SetRotate(float angle)
        {
            wheelToRotate.transform.localRotation = Quaternion.Euler(rotationAxis * angle);
        }

        private void Update()
        {
            // Roterar j?mt , dvs 15 grader ?t g?ngen l?ngs x-axeln :
            //Rotate(Time.deltaTime * 15.0f);

            // S?tt en vinkel
            //SetRotate(15.0f);


        }
    }
}