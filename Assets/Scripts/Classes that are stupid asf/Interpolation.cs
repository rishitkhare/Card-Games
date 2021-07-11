using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rendering
{
    //class for interpolation
    public static class Interpolation
    {
        public static float Clerp(float current, float target, float rate)
        {
            if (Mathf.Abs(target - current) < 0.05f)
            {
                return target;
            }
            else
            {
                return current + ((target - current) * rate);
            }
        }

        public static float Lerp(float current, float target, int currentStep, int interpolations)
        {
            if (currentStep >= interpolations)
            {
                return target;
            }
            else
            {
                float rate = (target - current) / interpolations;
                return current + (rate * currentStep);
            }
        }
    }
}