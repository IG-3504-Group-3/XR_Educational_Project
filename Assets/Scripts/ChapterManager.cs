using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using XR_Education_Project;

public class ChapterManager: MonoBehaviour
{
    private ArrayList goalMoleculesData;
    public float ?startTime;
    public GameObject currentGoal;
    public ArrayList completedMolecules;
    private GameManager gameManager;

    public GameObject moleculeShape2;
    public GameObject moleculeShape3;
    public GameObject moleculeShape4;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    public void StartChapter(ElementData element) // Initializes the chapter
    {
        goalMoleculesData = new ArrayList();
        completedMolecules = new ArrayList();
        SetGoalMoleculesData(element);
        SetNextGoal();
        startTime = Time.time;

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
        Debug.Log(goalMoleculesData.Count);
    }

    public void SetNextGoal()
    {

        if (goalMoleculesData.Count == 0)
        {
            // TODO: Handle ending of chapter
            EndChapter();
            return;
        }

        MoleculeData goalData = (MoleculeData) goalMoleculesData[0];
        goalMoleculesData.RemoveAt(0);
        GameObject goalMolecule;

        Vector3 pos = gameObject.transform.position;
        Quaternion quaternion = gameObject.transform.rotation;

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
        return Time.time - (float) startTime;
    }

    public void EndChapter() // Ends the chapter
    {
        startTime = null;
        goalMoleculesData.Clear();
    }

    

}
