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

        public ChapterManager chapterManager;

        private GameObject[] elementObjects;
        [HideInInspector] public string gameState;

        void Start()
        {
            chapterManager = FindObjectOfType<ChapterManager>();
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
            chapterManager.StartChapter(test);

            chapterManager.EndChapter();

        }
    }
}
