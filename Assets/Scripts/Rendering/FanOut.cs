﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rendering
{
    public class FanOut : MonoBehaviour
    {

        Hand parentHand;
        public int fanIndex = 0;
        float currentRadius;
        float currentRotation;
        int framesPerSampleCounter = 0;
        Collider2D col;

        // Start is called before the first frame update
        void Start()
        {
            parentHand = transform.parent.GetComponentInParent<Hand>();
            col = gameObject.GetComponent<Collider2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            framesPerSampleCounter++;
            if (framesPerSampleCounter % parentHand.framesPerSample == 0)
            {
                Interpolate();
            }
        }

        private void Interpolate()
        {
            //rotation
            float targetRotation;
            if (parentHand.GetNumberOfCardsInHand() > 1)
            {
                targetRotation = (parentHand.maxRotation / 2) -
                    fanIndex * parentHand.maxRotation / (parentHand.GetNumberOfCardsInHand() - 1);
            }
            else
            {
                targetRotation = 0f;
            }

            currentRotation = Interpolation.Clerp(currentRotation, targetRotation, parentHand.clerpRate);

            transform.eulerAngles = new Vector3(0, 0, currentRotation);
            //positioning

            //start at anchor

            transform.localPosition = parentHand.anchor;

            //moves by radius
            float targetRadius;
            if (IsMouseTouchingCollider())
            {
                targetRadius = parentHand.selectedHandWidth;
            }
            else
            {
                targetRadius = parentHand.handWidth;
            }

            //interpolate
            currentRadius = Interpolation.Clerp(currentRadius, targetRadius, parentHand.clerpRate);

            transform.localPosition +=
                currentRadius * transform.up;

            //offset
            transform.eulerAngles = new Vector3(0, 0,
                transform.eulerAngles.z + parentHand.rotationOffset);
        }

        //Card Linear interpolation
        //private float Clerp(float current, float target, float rate) {
        //}

        private bool IsMouseTouchingCollider()
        {
            var cardColliders = parentHand.GetCardColliders();
            int selectedIndex = -1;

            for (int i = 0; i < cardColliders.Count; i++)
            {

                if (cardColliders[i].OverlapPoint(
                    GameManager.gm.mainCamera.ScreenToWorldPoint(
                        Input.mousePosition)))
                {

                    selectedIndex = i;
                    break;
                }
            }

            return (selectedIndex == fanIndex);
        }
    }
}