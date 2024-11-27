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

        void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public void OnElementClicked(ElementData elementData)
        {
            if (elementData != null)
            {
                uiManager.DisplayElementInfoPanel(elementData); 
            }
            else
            {
                Debug.LogError("Element not found.");
            }
        }
    }
}
