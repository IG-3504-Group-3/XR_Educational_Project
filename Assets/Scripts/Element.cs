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
        private GameObject atomPrefab;
        private GameObject currentAtom;
        
        [HideInInspector] public ElementData elementData;

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            uiManager = FindObjectOfType<UIManager>();

            atomPrefab = gameManager.atomPrefab;
        }

        void OnMouseDown() // Replace with VR interaction later
        {
            switch (gameManager.gameState)
            {
                case "menu":
                    this.OnMainMenu();
                    break;

                case ("chapter"):
                    this.InstantiateAtom();
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

        private void InstantiateAtom() {
            Vector3 self_pos = gameObject.transform.position;
            Vector3 pos = new Vector3(self_pos.x, self_pos.y, self_pos.z - 1);
            Quaternion rotation = Quaternion.Euler(0, 90, 0);

            currentAtom = Instantiate(atomPrefab, pos, rotation); // Instantiate atom gameobject

             // Assign correct texture
            Renderer atomRenderer = currentAtom.GetComponent<Renderer>();
            string symbol = elementData.atomicSymbol;
            Texture newTexture = Resources.Load<Texture>($"Textures/{symbol}");
            
            if (newTexture != null)
            {
                atomRenderer.material.mainTexture = newTexture;
            }
            else
            {
                Debug.LogError($"Texture for symbol '{symbol}' not found in Resources/Textures/");
            }

            // Attach drag and drop script
            currentAtom.AddComponent<AtomDrag>();
        }

        public void SetAction(String action)
        {
            this.action = action;
        }

    }
}
