using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Trent
{
    public class UIBehaviour : MonoBehaviour
    {
        private Slider cohesion;
        private Slider dispersion;
        private Slider alignment;
        private Slider offset;

        private BaseFlockBehaviour flock;
        
        void Start()
        {
            cohesion = GameObject.Find("CohesionSlider").GetComponent<Slider>();
            dispersion = GameObject.Find("DispersionSlider").GetComponent<Slider>();
            alignment = GameObject.Find("AlignmentSlider").GetComponent<Slider>();
            offset = GameObject.Find("BoidOffsetSlider").GetComponent<Slider>();
            flock = GameObject.FindObjectOfType<BaseFlockBehaviour>();

            cohesion.maxValue = 100.0f;
            dispersion.maxValue = 100.0f;
            alignment.maxValue = 100.0f;
            offset.maxValue = 100.0f;

            cohesion.minValue = 0.0f;
            dispersion.minValue = 0.0f;
            alignment.minValue = 0.0f;
            offset.minValue = 1.0f;

            cohesion.value = 1.0f;
            dispersion.value = 1.0f;
            alignment.value = 1.0f;
            offset.value = 1.0f;
        }
        
        void FixedUpdate()
        {
            flock.CohesionForce = cohesion.value;
            flock.DispersionForce = dispersion.value;
            flock.AlignmentForce = alignment.value;
            flock.AgentOffset = offset.value;
        }
    }
}