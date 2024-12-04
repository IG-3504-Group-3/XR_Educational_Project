using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

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
            if (gameManager.gameState == "menu" && elementData != null)
            {
                uiManager.DisplayElementInfoPanel(elementData);
            }
            else if (elementData == null )
            {
                Debug.LogError("Element not found.");
            }
        }
    }
}
