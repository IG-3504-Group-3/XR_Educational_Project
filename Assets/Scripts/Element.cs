using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XR_Education_Project {
    public class Element : MonoBehaviour
    {
        private GameManager gameManager;
        private UIManager uiManager;
        private String action = null;
        
        [HideInInspector] public ElementData elementData;

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            uiManager = FindObjectOfType<UIManager>();
        }

        void OnMouseDown() // Replace with VR interaction later
        {
            switch (action)
            {
                case "MainMenu":
                    this.OnMainMenu();
                    break;

                case ("Instantiate"):
                    this.InstantiateElement();
                    break;

                case (null): 
                    break;
            }
            
        }

        private void OnMainMenu()
        {
            if (gameManager.gameState == "menu" && elementData != null)
            {
                uiManager.DisplayElementInfoPanel(elementData);
            }
            else if (elementData == null)
            {
                Debug.LogError("Element not found.");
            }
        }

        private void InstantiateElement() {
            Vector3 self_pos = gameObject.transform.position;
            Vector3 pos = new Vector3(self_pos.x, self_pos.y, self_pos.z - 1);

            var newElement = Instantiate(gameObject, pos, Quaternion.identity); // TODO: Change to a base prefab to fill with data
        }

        public void SetAction(String action)
        {
            this.action = action;
        }

    }
}
