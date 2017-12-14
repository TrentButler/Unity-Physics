using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Trent
{
    public class UIBehaviour : MonoBehaviour
    {
        private GameObject panel;
        private GameObject controlsPanel;

        private Slider springConstant;
        private Slider dampingFactor;
        private Slider damperTearFactor;
        private Slider windDragFactor;
        private Slider airDensity;

        private Button toggle;
        private Button gravityButton;
        private Button clothActiveButton;
        private Button windButton;
        private Button restartButton;

        private ClothBehaviour cloth;
        private bool active = true;
        private bool controlActive = true;

        public void RestartScene()
        {
            SceneManager.LoadScene("99.debug");
        }

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
        public void ToggleControls()
        {

            if (controlActive == true)
            {
                controlActive = false;
            }

            else
            {
                controlActive = true;
            }
        }

        void Start()
        {
            panel = GameObject.Find("ControlPanel");
            controlsPanel = GameObject.Find("InstructionPanel");
            springConstant = GameObject.Find("SpringConstantSlider").GetComponent<Slider>();
            dampingFactor = GameObject.Find("DampingFactorSlider").GetComponent<Slider>();
            damperTearFactor = GameObject.Find("TearCoefficientSlider").GetComponent<Slider>();
            windDragFactor = GameObject.Find("WindDragCoefficientSlider").GetComponent<Slider>();
            airDensity = GameObject.Find("AirDensitySlider").GetComponent<Slider>();

            toggle = GameObject.Find("UIToggle").GetComponent<Button>();
            gravityButton = GameObject.Find("ToggleGravityButton").GetComponent<Button>();
            clothActiveButton = GameObject.Find("ToggleClothActiveButton").GetComponent<Button>();
            windButton = GameObject.Find("ToggleWindButton").GetComponent<Button>();
            restartButton = GameObject.Find("RestartButton").GetComponent<Button>();

            cloth = GameObject.FindObjectOfType<ClothBehaviour>();

            springConstant.maxValue = 5.0f;
            dampingFactor.maxValue = 50.0f;
            damperTearFactor.maxValue = 100.0f;
            windDragFactor.maxValue = 100.0f;
            airDensity.maxValue = 100.0f;

            springConstant.minValue = 0.1f;
            dampingFactor.minValue = 0.1f;
            damperTearFactor.minValue = 1.0f;
            windDragFactor.minValue = 0.1f;
            airDensity.minValue = 0.1f;

            springConstant.value = 1.0f;
            dampingFactor.value = 1.0f;
            damperTearFactor.value = 1.0f;
            windDragFactor.value = 1.0f;
            airDensity.value = 1.0f;
        }

        private void Update()
        {
            panel.SetActive(active);
            controlsPanel.SetActive(controlActive);
            gravityButton.gameObject.SetActive(active);
            clothActiveButton.gameObject.SetActive(active);
            windButton.gameObject.SetActive(active);
            restartButton.gameObject.SetActive(active);
            springConstant.gameObject.SetActive(active);

            if (active == true)
            {
                toggle.GetComponentInChildren<Text>().text = "UI OFF";
            }

            else
            {
                toggle.GetComponentInChildren<Text>().text = "UI ON";
            }

            if (cloth.useGravity == false)
            {
                gravityButton.GetComponentInChildren<Text>().text = "Gravity OFF";
            }
            if (cloth.useGravity == true)
            {
                gravityButton.GetComponentInChildren<Text>().text = "Gravity ON";
            }

            if (cloth.windActive == false)
            {
                windButton.GetComponentInChildren<Text>().text = "Wind OFF";
            }
            if (cloth.windActive == true)
            {
                windButton.GetComponentInChildren<Text>().text = "Wind ON";
            }

            if (cloth.isActive == false)
            {
                clothActiveButton.GetComponentInChildren<Text>().text = "Cloth OFF";
            }
            if (cloth.isActive == true)
            {
                clothActiveButton.GetComponentInChildren<Text>().text = "Cloth ON";
            }
        }

        void FixedUpdate()
        {
            cloth.springConstant = springConstant.value;
            cloth.dampingFactor = dampingFactor.value;
            cloth.tearCoefficient = damperTearFactor.value;
            cloth.windDragCoefficient = windDragFactor.value;
            cloth.airDensity = airDensity.value;
        }
    }
}