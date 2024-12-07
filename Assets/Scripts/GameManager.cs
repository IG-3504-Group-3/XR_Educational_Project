using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Education_Project {
    public class GameManager : MonoBehaviour
    {
        public MoleculeData[] allMoleculeData;
        public ElementData[] elementDataArray;

        public ElementData test;

        public UIManager uiManager;
        public GameObject elementPrefab;

        private GameObject[] elementObjects;
        [HideInInspector] public string gameState;

        void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
            stateMenu();
        }

        public void stateMenu()
        {
            gameState = "menu";
        }

        public void stateInfo()
        {
            gameState = "info";
        }

        public void stateChapter()
        {
            gameState = "chapter";
            ChapterSystem.StartChapter(test);

            ChapterSystem.EndChapter();

        }
    }
}
