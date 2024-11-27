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
            elementObjects = new GameObject[elementDataArray.Length];
            InitializePeriodicTable();
        }

        public void InitializePeriodicTable()
        {
            if (elementDataArray.Length > 0)
            {
                for (int i = 0; i < elementDataArray.Length; i++)
                {
                    // Instantiate the element prefab and set up its data
                    elementObjects[i] = Instantiate(elementPrefab);

                    // Get the element script and assign the data
                    Element elementScript = elementObjects[i].GetComponent<Element>();
                    if (elementScript!= null)
                    {
                        elementScript.elementData = elementDataArray[i];
                    }
                }
            }

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
