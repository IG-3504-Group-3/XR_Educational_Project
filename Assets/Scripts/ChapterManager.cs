using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using XR_Education_Project;

namespace XR_Education_Project {
    public class ChapterManager: MonoBehaviour
    {
        private ArrayList goalMoleculesData;
        public float ?startTime;
        public ArrayList completedMolecules;

        private GameManager gameManager;
        private UIManager uiManager;
        private float finalTime;
        private int totalGoalMolecules;

        public GameObject moleculeShape2;
        public GameObject moleculeShape3;
        public GameObject moleculeShape4;

        private GameObject goalMolecule;

        void Start()
        {
            if (gameManager == null)
            {
                gameManager = FindObjectOfType<GameManager>();
            }
            if (uiManager == null)
            {
                uiManager = FindObjectOfType<UIManager>();
            }
        }

        void Update()
        {
            if (gameManager.gameState == "chapter")
            {
                float currentTime = GetScore();
                uiManager.setChapterTime(currentTime);
            }
        }

        public void StartChapter(ElementData element) // Initializes the chapter
        {
            goalMoleculesData = new ArrayList();
            completedMolecules = new ArrayList();
            SetGoalMoleculesData(element);
            startTime = Time.time;
            Debug.Log($"Setting startTime to: {Time.time}");
            SetNextGoal();
        }

        public void SetGoalMoleculesData(ElementData element) // Adds all molecules that contain the element to the goal molecules of the chapter
        {
            var molecules = gameManager.allMoleculeData;
            foreach (var molecule in molecules)
            {
                if (molecule.elements.Contains(element))
                {
                    goalMoleculesData.Add(molecule);
                }
            }
            Debug.Log($"Goal Molecules to complete: {goalMoleculesData.Count}");
            totalGoalMolecules = goalMoleculesData.Count;
        }

        public void SetNextGoal()
        {
            if (goalMoleculesData.Count == 0)
            {
                StartCoroutine(EndChapterCoroutine());
                return;
            }

            MoleculeData goalData = (MoleculeData) goalMoleculesData[0];
            goalMoleculesData.RemoveAt(0);

            Vector3 pos = gameObject.transform.position;
            Quaternion quaternion = gameObject.transform.rotation;

            uiManager.setMoleculeData(goalData);
            uiManager.setChapterProgress(totalGoalMolecules - goalMoleculesData.Count, totalGoalMolecules);

            switch (goalData.numberOfAtoms)
            {
                case 2:
                    goalMolecule = Instantiate(moleculeShape2, pos, quaternion);
                    goalMolecule.GetComponent<MoleculeManager>().moleculeData = goalData;
                    goalMolecule.GetComponent<MoleculeManager>().chapterManager = gameObject.GetComponent<ChapterManager>();
                    break;
                case 3:
                    goalMolecule = Instantiate(moleculeShape3, pos, quaternion);
                    goalMolecule.GetComponent<MoleculeManager>().moleculeData = goalData;
                    goalMolecule.GetComponent<MoleculeManager>().chapterManager = gameObject.GetComponent<ChapterManager>();
                    break;
                case 4:
                    goalMolecule = Instantiate(moleculeShape4, pos, quaternion);
                    goalMolecule.GetComponent<MoleculeManager>().moleculeData = goalData;
                    goalMolecule.GetComponent<MoleculeManager>().chapterManager = gameObject.GetComponent<ChapterManager>();
                    break;
            }

        }

        public float GetScore() // Returns the time since the start of the chapter
        {
            if (startTime.HasValue)
            {
                return Time.time - startTime.Value;
            }
            else
            {
                // Debug.Log("startTime is null.");
                return 0f; 
            }
        }

        private IEnumerator EndChapterCoroutine()
        {
            finalTime = GetScore();
            yield return new WaitForSeconds(0.1f);
            EndChapter(finalTime);
        }

        public void EndChapter(float finalTime) // Ends the chapter
        {
            startTime = null;
            goalMoleculesData.Clear();

            // Remove any stray atoms
            AtomManager.RemoveAllAtoms();
            gameManager.stateEndChapter(finalTime);
        }

        public void RemoveMolecule() 
        {
            Destroy(goalMolecule);
        }

    }
}
