using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Education_Project {
    public class GameManager : MonoBehaviour
    {
        public UIManager uiManager;
        public ElementData[] elementDataArray;
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
        }
    }
}
