using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace XR_Education_Project {
    public class Element : MonoBehaviour
    {
        // Element class for "squares" of the periodic table
        private GameManager gameManager;
        private UIManager uiManager;
        private String action = null;

        private GameObject atomPrefab;
        private GameObject currentAtom;
        private static GameObject atomContainer;
        
        [HideInInspector] public ElementData elementData;

        private XRSimpleInteractable interactable;

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            uiManager = FindObjectOfType<UIManager>();
            atomPrefab = gameManager.atomPrefab;

            interactable = gameObject.GetComponent<XRSimpleInteractable>();
            if (interactable != null)
            {
                interactable.interactionManager = gameManager.interactionManager.GetComponent<XRInteractionManager>();
                interactable.selectEntered.AddListener(OnRaycastClick);
            }
        }

        private void OnRaycastClick(SelectEnterEventArgs args)
        {
            // Handles what should happen when an element is clicked
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
                uiManager.DisplayElementInfoPanel(elementData, gameManager.getBestTime(elementData));
            }
            else if (elementData == null)
            {
                Debug.LogError("Element not found.");
            }
        }

        private void InstantiateAtom() {
            // Creates an atom in front of an element
            if (atomContainer == null)
            {
                atomContainer = new GameObject("AtomContainer");
            }
            Vector3 self_pos = gameObject.transform.position;
            Vector3 pos = new Vector3(self_pos.x, self_pos.y, self_pos.z - 0.1f);
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
