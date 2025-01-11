using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public SaveManager saveManager;
        public Dictionary<string, List<float>> elementTimes;


        private GameObject[] elementObjects;
        [HideInInspector] public string gameState;

        public GameObject interactionManager;

        void Start()
        {
            chapterManager = FindObjectOfType<ChapterManager>();
            uiManager = FindObjectOfType<UIManager>();
            periodicTable = Instantiate(periodicTablePrefab);
            saveManager = FindObjectOfType<SaveManager>();
            elementTimes = saveManager.Load();
            
            stateMenu();
        }

        public void stateMenu()
        {
            gameState = "menu";
            periodicTable.GetComponent<PeriodicTable>().SetElementActions("MainMenu");
        }

        public void stateInfo()
        {
            gameState = "info";
            periodicTable.GetComponent<PeriodicTable>().SetElementActions("MainMenu");
        }

        public void stateChapter(ElementData element)
        {
            chapterManager.StartChapter(element);
            gameState = "chapter";
            periodicTable.GetComponent<PeriodicTable>().SetElementActions("Chapter");
        }

        public void stateEndChapter(float finalTime, ElementData finishedElement)
        {
            gameState = "endChapter";
            periodicTable.GetComponent<PeriodicTable>().SetElementActions("MainMenu");


            if (!elementTimes.ContainsKey(finishedElement.atomicSymbol))
            {
                elementTimes[finishedElement.atomicSymbol] = new List<float>();
            }
            elementTimes[finishedElement.atomicSymbol].Sort();
            elementTimes[finishedElement.atomicSymbol].Add(finalTime);

            saveManager.Save(elementTimes);
            uiManager.displayEndChapter(finalTime, elementTimes[finishedElement.atomicSymbol][0]);
        }

        public float getBestTime(ElementData element)
        {
            if (elementTimes.ContainsKey(element.atomicSymbol)) {
                return elementTimes[element.atomicSymbol][0];
            }

            return -1f;
            
        }
    }
}
