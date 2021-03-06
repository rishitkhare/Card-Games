﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rendering
{
    public class GoToMouse : MonoBehaviour
    {
        public float fancyRotationFactor = 10f;
        public float decelerationFactor = 0.9f;
        public float maxDegreesOfRotation = 40f;

        GameObject cameraObject;
        Camera cam;

        Vector2 previousPosition;
        float rotationZ;
        // Start is called before the first frame update
        void Start()
        {
            cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
            cam = cameraObject.GetComponent<Camera>();
            rotationZ = 0f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float initialRotX = transform.eulerAngles.x;
            float initialRotY = transform.eulerAngles.y;

            float timeScale = Time.fixedDeltaTime * 60f;
            Vector2 MouseWorldSpace = cam.ScreenToWorldPoint(Input.mousePosition);
            rotationZ += (previousPosition - MouseWorldSpace).x * timeScale;

            rotationZ *= decelerationFactor;
            if (Mathf.Abs(rotationZ) < 0.1f)
            {
                rotationZ = 0f;
            }

            if (Mathf.Abs(rotationZ * fancyRotationFactor) > maxDegreesOfRotation)
            {
                transform.eulerAngles = new Vector3(initialRotX, initialRotY, maxDegreesOfRotation * Mathf.Sign(rotationZ));
                rotationZ = Mathf.Sign(rotationZ) * maxDegreesOfRotation / fancyRotationFactor;
            }
            else
            {
                transform.eulerAngles = new Vector3(initialRotX, initialRotY, rotationZ * fancyRotationFactor);
            }

            transform.position = MouseWorldSpace;
            previousPosition = transform.position;
        }
    }
}