using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XR_Education_Project;

namespace XR_Education_Project {
    public class PeriodicTable : MonoBehaviour
    {
        private UIManager uiManager;
        public Material metalMaterial;
        public Material nonmetalMaterial;
        public Material metalloidMaterial;

        private int columns = 18;
        private int rows = 7; // Size of the table
        private float spacing = 1.25f; // Spacing between elements
        public GameObject element; // Element prfab to instantiate
        private List<List<int>> elementIdxs = new List<List<int>>(); // 2D Array
        public ElementData[] elementDataArray; // Element data objects added as a list on the inspector
        public Dictionary<(int,int), GameObject> elementDict = new Dictionary<(int,int), GameObject>();
        private List<(int, int)> emptyPos = new List<(int, int)>
        {
            // First Line
            (0,1),
            (0,2),
            (0,3),
            (0,4),
            (0,5),
            (0,6),
            (0,7),
            (0,8),
            (0,9),
            (0,10),
            (0,11),
            (0,12),
            (0,13),
            (0,14),
            (0,15),
            (0,16),
            // Second Line
            (1,2),
            (1,3),
            (1,4),
            (1,5),
            (1,6),
            (1,7),
            (1,8),
            (1,9),
            (1,10),
            (1,11),
            // Third Line
            (2,2),
            (2,3),
            (2,4),
            (2,5),
            (2,6),
            (2,7),
            (2,8),
            (2,9),
            (2,10),
            (2,11),
        };

        void Start()
        {
            uiManager = FindObjectOfType<UIManager>();

            var elementHash = elementDataArray.ToDictionary(
                x => (x.rowIdx, x.colIdx),
                x => x
            );

            for (int rowIdx = 0; rowIdx < rows; rowIdx++)
            {
                elementIdxs.Add(new List<int>());

                for (int colIdx = 0; colIdx < columns; colIdx++)
                {
                    elementIdxs[rowIdx].Add(colIdx);
                    if (!emptyPos.Contains((rowIdx, colIdx)))
                    {
                        Vector3 pos = new Vector3(colIdx, -rowIdx, 0) * spacing;
                        var newElement = Instantiate(element, pos, Quaternion.identity);
                        // Change the element properties
                        newElement.GetComponent<Element>().elementData = elementHash[(rowIdx, colIdx)];
                        uiManager.DisplayElement(newElement);

                        // Change material depending on group
                        var renderer = newElement.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.material = GetMaterial(newElement.GetComponent<Element>().elementData.elementClass);
                        }
                        newElement.transform.parent = gameObject.transform;

                        // Set the element action
                        newElement.GetComponent<Element>().SetAction("MainMenu");
                        elementDict[(rowIdx, colIdx)] = newElement;
                    }
                }
            }

        }

        void Update()
        {

        }

        private Material GetMaterial(string elementClass)
        {
            switch (elementClass)
            {
                case "Non-Metals":
                    return nonmetalMaterial;
                case "Metals":
                    return metalMaterial;
                case "Metalloids":
                    return metalloidMaterial;
                default:
                    return metalMaterial;
            }
        }

        private void SetElementActions(string action)
        {
            foreach (GameObject element in elementDict.Values) {
                element.GetComponent<Element>().SetAction(action);
            }
        }

    }

}
