using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XR_Education_Project {
    public class UIManager : MonoBehaviour
    {
        public GameObject elementInfoPanelPrefab;
        public GameObject mainMenu;
        private GameObject infoPanel;

        private Button backToMenuButton;
        private Button startChapterButton;


        public void EnableMenuUI()
        {
            mainMenu.SetActive(true);
        }

        public void DisableMenuUI()
        {
            mainMenu.SetActive(false);
        }

        public void DisplayElement(GameObject element)
        {
            ElementData elementData = element.GetComponent<Element>().elementData;

            TextMeshProUGUI[] textFields = element.GetComponentsInChildren<TextMeshProUGUI>();
            Dictionary<string, string> elementInfoMap = new Dictionary<string, string>
            {
                { "Symbol", elementData.atomicSymbol },
                { "AtomicNumber", elementData.atomicNumber.ToString() },
                { "AtomicMass", elementData.atomicMass.ToString() },
                { "Name", elementData.elementName },
            };
            
            foreach (TextMeshProUGUI textField in textFields)
            {
                if (elementInfoMap.ContainsKey(textField.name))
                {
                    textField.text = elementInfoMap[textField.name];
                }
            }
            
        }

        public void DisplayElementInfoPanel(ElementData elementData)
        {   
            DisableMenuUI();
            
            // Instantiate panel
            infoPanel = Instantiate(elementInfoPanelPrefab);

            // Atttach main camera to canvas
            Canvas infoCanvas = infoPanel.transform.Find("Canvas").GetComponent<Canvas>();
            if (infoCanvas)
            {
                infoCanvas.worldCamera = Camera.main;
            }

            // Get all panel text fields
            TextMeshProUGUI[] textFields = infoPanel.GetComponentsInChildren<TextMeshProUGUI>();

             // Dictionary to map textField names to elementData properties
            Dictionary<string, string> elementInfoMap = new Dictionary<string, string>
            {
                { "atomicSymbol", elementData.atomicSymbol },
                { "atomicNumber", elementData.atomicNumber.ToString() },
                { "atomicMass", elementData.atomicMass.ToString() },
                { "elementName", elementData.elementName },
                { "numberElementPanel", elementData.atomicNumber.ToString() },
                { "massElementPanel", elementData.atomicMass.ToString() },
                { "classElementPanel", elementData.elementClass },
                { "groupElementPanel", elementData.group.ToString() },
                { "groupNameElementPanel", elementData.groupName },
                { "meltingBoilingElementPanel", $"{elementData.meltingPoint.ToString()}, {elementData.boilingPoint.ToString()}" },
                { "affinityElementPanel", elementData.electronAffinity.ToString() },
                { "electronElementPanel", elementData.electronConfiguration },
                { "isotopesElementPanel", elementData.keyIsotopes }
            };
            
            foreach (TextMeshProUGUI textField in textFields)
            {
                if (elementInfoMap.ContainsKey(textField.name))
                {
                    textField.text = elementInfoMap[textField.name];
                }
            }

            infoPanel.SetActive(true);
            
            // Get panel buttons
            backToMenuButton = infoPanel.transform.Find("Canvas/background/returnMenuButton")?.GetComponent<Button>();
            startChapterButton= infoPanel.transform.Find("Canvas/background/startChapterButton")?.GetComponent<Button>();

            if (backToMenuButton != null)
            {
                backToMenuButton.onClick.AddListener(backToMenuClicked);
            }
            // TODO: startChapterButton.onClick.AddListener(startChapterClicked);
        }

        public void backToMenuClicked()
        {
            Debug.Log("Returning to Menu...");
            Destroy(infoPanel);
            EnableMenuUI();
        }

    }
}
