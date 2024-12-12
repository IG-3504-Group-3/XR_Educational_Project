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

        private GameObject[] elementObjects;
        [HideInInspector] public string gameState;

        void Start()
        {
            chapterManager = FindObjectOfType<ChapterManager>();
            uiManager = FindObjectOfType<UIManager>();
            periodicTable = Instantiate(periodicTablePrefab);
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

        public void stateEndChapter(float finalTime)
        {
            gameState = "endChapter";
            periodicTable.GetComponent<PeriodicTable>().SetElementActions("MainMenu");
            uiManager.displayEndChapter(finalTime);
        }
    }
}
