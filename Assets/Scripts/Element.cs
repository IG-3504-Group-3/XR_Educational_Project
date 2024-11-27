using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Education_Project {
    public class Element : MonoBehaviour
    {
        private GameManager gameManager;
        private UIManager uiManager;
        [HideInInspector] public ElementData elementData;

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            uiManager = FindObjectOfType<UIManager>();
        }

        void OnMouseDown() // Replace with VR interaction later
        {
            if (elementData != null)
            {
                uiManager.DisplayElementInfoPanel(elementData);
            }
            else
            {
                Debug.LogError("Element not found.");
            }
        }
    }
}
