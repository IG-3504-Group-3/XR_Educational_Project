using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace XR_Education_Project {
    public class GameManager : MonoBehaviour
    {
        public MoleculeData[] allMoleculeData;
        public ElementData[] elementDataArray;

        public UIManager uiManager;
        public GameObject elementPrefab;
        public GameObject atomPrefab;
        public GameObject periodicTablePrefab;
        [HideInInspector] public  GameObject periodicTable;

        public ChapterManager chapterManager;

        private GameObject[] elementObjects;
        [HideInInspector] public string gameState;

        public GameObject interactionManager;
        public XRRayInteractor rayInteractor;
        private XRSimpleInteractable[] allInteractables; 

        void Start()
        {
            chapterManager = FindObjectOfType<ChapterManager>();
            uiManager = FindObjectOfType<UIManager>();
            periodicTable = Instantiate(periodicTablePrefab);
            stateMenu();
        }

        void Update()
        {   

        }


        public void stateMenu()
        {
            gameState = "menu";
            periodicTable.GetComponent<PeriodicTable>().SetElementActions("MainMenu");
            allInteractables = FindObjectsOfType<XRSimpleInteractable>();
            foreach (var interactable in allInteractables)
            {
                interactable.enabled = true;
            }
        }

        public void stateInfo()
        {
            gameState = "info";
            allInteractables = FindObjectsOfType<XRSimpleInteractable>();
            if (allInteractables is not null){
                foreach (var interactable in allInteractables)
                {
                    interactable.enabled = false;
                }
            }
        }

        public void stateChapter(ElementData element)
        {
            gameState = "chapter";
            Debug.Log("State chapter");
            periodicTable.GetComponent<PeriodicTable>().SetElementActions("Chapter");
            chapterManager.StartChapter(element);
            if (allInteractables is not null){
                foreach (var interactable in allInteractables)
                {
                    interactable.enabled = true;
                }
            }
        }

        public void stateEndChapter(float finalTime)
        {
            gameState = "endChapter";
            Debug.Log("State endchapter");
            allInteractables = FindObjectsOfType<XRSimpleInteractable>();
            if (allInteractables is not null){
                foreach (var interactable in allInteractables)
                {
                    interactable.enabled = false;
                }
            }
        }
    }
}
