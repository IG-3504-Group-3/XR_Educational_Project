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
        private ChapterManager chapterManager;

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
            chapterManager = FindObjectOfType<ChapterManager>();
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
            string bestTime = FormatTime(130.00f); // TODO: add best time 

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
                { "recordTime", $"Best Record (mm:ss): {bestTime}"},
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

            GameObject panel = infoPanel.transform.Find("Canvas/background/infoPanel").gameObject;
            GameObject card = infoPanel.transform.Find("Canvas/background/elementCard").gameObject;
            DisableRaycastTargets(panel);
            DisableRaycastTargets(card);
            
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

            // Instantiate 
            chapterUI = Instantiate(chapterViewPrefab);

            //Attach camera view
            Canvas chapterCanvas = chapterUI.transform.Find("Canvas").GetComponent<Canvas>();
            if (chapterCanvas)
            {
                chapterCanvas.worldCamera = Camera.main;
            }

            chapterUI.SetActive(true);

            // Set game state
            gameManager.stateChapter(currentElementData);

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
            chapterManager.RemoveMolecule();

            // Set game state
            gameManager.stateInfo();
        }

        public void displayEndChapter(float currentTime) //TODO: Add best time 
        {
            Destroy(chapterUI);
            gameManager.stateEndChapter(currentTime);
            endChapterUI = Instantiate(endChapterPrefab);
            endChapterUI.SetActive(true);

            TextMeshProUGUI chapterName = endChapterUI.transform.Find("Canvas/summaryPanel/chapterName")?.GetComponent<TextMeshProUGUI>();
            if (chapterName != null)
            {
                chapterName.text = currentElementData.elementName;
            }

            TextMeshProUGUI summaryCurrentTime = endChapterUI.transform.Find("Canvas/summaryPanel/currentScore")?.GetComponent<TextMeshProUGUI>();
            if (summaryCurrentTime != null)
            {
                string formattedTime = FormatTime(currentTime);
                summaryCurrentTime.text = formattedTime;
            }

            TextMeshProUGUI summaryBestTime = endChapterUI.transform.Find("Canvas/summaryPanel/bestScore")?.GetComponent<TextMeshProUGUI>();
            if (summaryBestTime != null)
            {
                string formattedTime = FormatTime(currentTime); //TODO: Change to best time
                summaryBestTime.text = formattedTime;
            }

            GameObject summary = endChapterUI.transform.Find("Canvas/summaryPanel").gameObject;
            DisableRaycastTargets(summary);

            Graphic button = summary.transform.Find("backButton")?.GetComponent<Graphic>();
            button.raycastTarget = true;


            //Get finish chapter button
            finishChapterButton = endChapterUI.transform.Find("Canvas/summaryPanel/backButton")?.GetComponent<Button>();
            if (finishChapterButton != null)
            {
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

        public void setMoleculeData(MoleculeData moleculeData)
        {
            TextMeshProUGUI moleculeName = chapterUI.transform.Find("Canvas/chapterBar/moleculeName").GetComponent<TextMeshProUGUI>();
            if (moleculeName != null)
            {
                moleculeName.text = moleculeData.moleculeName;
            }
        }

        public void setChapterProgress(int curr, int total)
        {
            TextMeshProUGUI chapterProgress = chapterUI.transform.Find("Canvas/chapterBar/progress").GetComponent<TextMeshProUGUI>();
            if (chapterProgress != null)
            {
            chapterProgress.text = $"Progress: {curr}/{total}";
            }
        }

        public void setChapterTime(float time)
        {
            if (chapterUI != null){
                TextMeshProUGUI chapterTimer = chapterUI.transform.Find("Canvas/chapterBar/timer")?.GetComponent<TextMeshProUGUI>();
                if (chapterTimer != null)
                {
                    string formattedTime = FormatTime(time);
                    chapterTimer.text = formattedTime;
                }
            }
        
        }

        public string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);  // Get minutes
            int seconds = Mathf.FloorToInt(time % 60); // Get remaining seconds
            return string.Format("{0}:{1:D2}", minutes, seconds); // Format as mm:ss
        }

        public void DisableRaycastTargets(GameObject parent)
        {
            // Get all components in the parent and its children
            Graphic[] graphics = parent.GetComponentsInChildren<Graphic>();

            // disable raycasting target
            foreach (var graphic in graphics)
            {
                graphic.raycastTarget = false;
            }
        }

    }
}
