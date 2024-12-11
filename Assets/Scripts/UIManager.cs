using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XR_Education_Project {
    public class UIManager : MonoBehaviour
    {
        public GameObject elementInfoPanelPrefab;
        public GameObject chapterViewPrefab;
        public GameObject mainMenu;
        public GameObject endChapterPrefab;

        private GameManager gameManager;

        private GameObject infoPanel;
        private GameObject chapterUI;
        private GameObject endChapterUI;

        private Button backToMenuButton;
        private Button startChapterButton;
        private Button leaveChapterButton;
        private Button finishChapterButton;

        private ElementData currentElementData;

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

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
            currentElementData = elementData;

            DisableMenuUI();

            // Set game state
            gameManager.stateInfo();
            
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

            if (startChapterButton != null)
            {
                startChapterButton.onClick.AddListener(startChapterClicked);
            }
        }

        public void backToMenuClicked()
        {
            Debug.Log("Returning to menu...");
            Destroy(infoPanel);
            EnableMenuUI();

            // Set game state
            gameManager.stateMenu();
        }

        public void startChapterClicked()
        {
            Debug.Log($"Starting chapter for {currentElementData.elementName}");
            infoPanel.SetActive(false);

            // Set game state
            gameManager.stateChapter(currentElementData);

            // Instantiate 
            chapterUI = Instantiate(chapterViewPrefab);

            //Attach camera view
            Canvas chapterCanvas = chapterUI.transform.Find("Canvas").GetComponent<Canvas>();
            if (chapterCanvas)
            {
                chapterCanvas.worldCamera = Camera.main;
            }

            chapterUI.SetActive(true);

            // Get exit button
            leaveChapterButton = chapterUI.transform.Find("Canvas/exitChapter")?.GetComponent<Button>();
            if (leaveChapterButton != null)
            {
                leaveChapterButton.onClick.AddListener(leaveChapterClicked);
            }
        }

        private void leaveChapterClicked()
        {
            Destroy(chapterUI);
            infoPanel.SetActive(true);
            AtomManager.RemoveAllAtoms();

            // Set game state
            gameManager.stateInfo();
        }

        public void displayEndChapter()
        {
            // Set game state
            gameManager.stateEndChapter();

            Destroy(chapterUI);
            endChapterUI = Instantiate(endChapterPrefab);
            endChapterUI.SetActive(true);

            //Get finish chapter button
            finishChapterButton = endChapterUI.transform.Find("Canvas/summaryPanel/backButton")?.GetComponent<Button>();
            if (finishChapterButton != null)
            {
                Debug.Log("Finish Chapter Button Found.");
                finishChapterButton.onClick.AddListener(finishChapterClicked);
            }
        }

        private void finishChapterClicked()
        {
            Destroy(endChapterUI);
            Destroy(infoPanel);

            EnableMenuUI();
            // Set game state
            gameManager.stateMenu();
        }

    }
}
