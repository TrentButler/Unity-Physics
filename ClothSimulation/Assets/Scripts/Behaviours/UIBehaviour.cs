using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Trent
{
    public class UIBehaviour : MonoBehaviour
    {
        private GameObject panel;
        private Slider speed;
        private Slider cohesion;
        private Slider dispersion;
        private Slider alignment;
        private Slider offset;

        private Button toggle;
        private Button cameraButton;
        private Button addButton;
        private Button removeButton;
        private Button prevButton;
        private Button nextButton;

        private ClothBehaviour cloth;
        private bool active = true;

        public void ToggleUI()
        {

            if (active == true)
            {
                active = false;
            }

            else
            {
                active = true;
            }
        }

        void Start()
        {
            panel = GameObject.Find("ControlPanel");
            speed = GameObject.Find("FlockSpeedSlider").GetComponent<Slider>();
            cohesion = GameObject.Find("CohesionSlider").GetComponent<Slider>();
            dispersion = GameObject.Find("DispersionSlider").GetComponent<Slider>();
            alignment = GameObject.Find("AlignmentSlider").GetComponent<Slider>();
            offset = GameObject.Find("BoidOffsetSlider").GetComponent<Slider>();

            toggle = GameObject.Find("UIToggle").GetComponent<Button>();
            cameraButton = GameObject.Find("ToggleCamera").GetComponent<Button>();
            addButton = GameObject.Find("AddBoids").GetComponent<Button>();
            removeButton = GameObject.Find("DestroyBoids").GetComponent<Button>();
            prevButton = GameObject.Find("PreviousBoid").GetComponent<Button>();
            nextButton = GameObject.Find("NextBoid").GetComponent<Button>();

            cloth = GameObject.FindObjectOfType<ClothBehaviour>();

            speed.maxValue = 1.0f;
            //speed.maxValue = 0.0001f;
            cohesion.maxValue = 100.0f;
            dispersion.maxValue = 100.0f;
            alignment.maxValue = 100.0f;
            offset.maxValue = 100.0f;

            speed.minValue = 0.0f;
            cohesion.minValue = 0.0f;
            dispersion.minValue = 0.0f;
            alignment.minValue = 0.0f;
            offset.minValue = 1.0f;

            speed.value = 0.0f;
            cohesion.value = 1.0f;
            dispersion.value = 1.0f;
            alignment.value = 1.0f;
            offset.value = 1.0f;
        }

        private void Update()
        {
            panel.SetActive(active);
            cameraButton.gameObject.SetActive(active);
            addButton.gameObject.SetActive(active);
            removeButton.gameObject.SetActive(active);
            prevButton.gameObject.SetActive(active);
            nextButton.gameObject.SetActive(active);
            speed.gameObject.SetActive(active);
            
            if(active == true)
            {
                toggle.GetComponentInChildren<Text>().text = "UI OFF";
            }

            else
            {
                toggle.GetComponentInChildren<Text>().text = "UI ON";
            }
        }

        void FixedUpdate()
        {
            //flock.FlockMovementSpeed = speed.value;
            //flock.CohesionForce = cohesion.value;
            //flock.DispersionForce = dispersion.value;
            //flock.AlignmentForce = alignment.value;
            //flock.AgentOffset = offset.value;
        }
    }
}