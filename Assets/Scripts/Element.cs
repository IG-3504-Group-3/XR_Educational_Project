using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

namespace XR_Education_Project {
    public class Element : MonoBehaviour
    {
        private GameManager gameManager;
        private UIManager uiManager;
        private String action = null;

        private GameObject atomPrefab;
        private GameObject currentAtom;
        private static GameObject atomContainer;
        
        [HideInInspector] public ElementData elementData;

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            uiManager = FindObjectOfType<UIManager>();
            atomPrefab = gameManager.atomPrefab;

            gameObject.GetComponent<XRBaseInteractable>().interactionManager = gameManager.interactionManager.GetComponent<XRInteractionManager>();
        }

        void onClick() // Replace with VR interaction later
        {
            switch (action)
            {
                case "MainMenu":
                    this.OnMainMenu();
                    break;

                case ("Chapter"):
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
            if (atomContainer == null)
            {
                atomContainer = new GameObject("AtomContainer");
            }
            Vector3 self_pos = gameObject.transform.position;
            Vector3 pos = new Vector3(self_pos.x, self_pos.y, self_pos.z - 1);
            Quaternion rotation = Quaternion.Euler(0, 90, 0);

            currentAtom = Instantiate(atomPrefab, pos, rotation); // Instantiate atom gameobject
            currentAtom.transform.SetParent(atomContainer.transform);
            currentAtom.tag = "Atom";

            // Assign correct texture
            AtomManager am = currentAtom.GetComponent<AtomManager>();
            am.elementData = elementData;
            am.fill();
            AtomManager.AddAtom(currentAtom);

            // Attach drag and drop script
            currentAtom.AddComponent<AtomDrag>();
        }

        public void SetAction(String action)
        {
            this.action = action;
        }

    }
}
