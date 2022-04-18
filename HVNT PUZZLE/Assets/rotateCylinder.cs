using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HVNTPUZZLE_MAC
{
    public class rotateCylinder : MonoBehaviour
    {
        [SerializeField]
        private GameObject cylinder;

        public void rotate(float angle)
        {
            cylinder.transform.Rotate(new Vector3(0, 0, 1), angle);
        }
    }
}

