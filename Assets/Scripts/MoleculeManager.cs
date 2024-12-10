using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XR_Education_Project;

public class MoleculeManager : MonoBehaviour
{

    public MoleculeData moleculeData;
    // Dictionary<AtomicSymbol, GameObject> of atoms in the molecule that haven't been filled
    public Dictionary<string, ArrayList> elementsToFill = new Dictionary<string, ArrayList>();
    public bool isComplete = false;
    public ChapterManager chapterManager;


    void Start()
    {
        int atomIdx = 0; // Because children can also be cylinders we have an idx that reference just atoms
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform go = gameObject.transform.GetChild(i); // Gets the children of the molecule prefab (cylinders and QuestionMarks)
            if (go.name.Contains("Q")) // All atom prefab names begin with Q
            {
                // Atom data are set according to hirearchy
                AtomManager am = go.GetComponent<AtomManager>();
                ElementData elementData = moleculeData.elements[atomIdx];
                am.elementData = elementData; // Sets the data of the atom
                atomIdx++;
                if (!elementsToFill.ContainsKey(elementData.atomicSymbol))
                {
                    elementsToFill[elementData.atomicSymbol] = new ArrayList();
                }

                elementsToFill[elementData.atomicSymbol].Add(go.gameObject);

            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handles the collision between an atom and the molecule
        if (other.name.Contains("Atom"))
        {
            AtomManager other_am = other.GetComponent<AtomManager>();

            if (elementsToFill.ContainsKey(other_am.elementData.atomicSymbol))
            {
                GameObject toFill = (GameObject) elementsToFill[other_am.elementData.atomicSymbol][0];
                
                toFill.GetComponent<AtomManager>().fill();

                elementsToFill[other_am.elementData.atomicSymbol].RemoveAt(0);
                Destroy(other.gameObject);
                checkCompletion();
            }
        }
    }

    public bool checkCompletion()
    {
        bool completeTest = true;
        foreach (var elementIdx in elementsToFill.Keys)
        {
            if (elementsToFill[elementIdx].Count != 0)
            {
                completeTest = false; 
                break;
            }
        }

        isComplete = completeTest;
        if (isComplete) 
        {
            // Remove completed molecule
            Destroy(gameObject);
            // Move to next goal
            chapterManager.SetNextGoal();
        }
        return isComplete;
    }
}
