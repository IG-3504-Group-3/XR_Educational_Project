using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeManager : MonoBehaviour
{

    public MoleculeData moleculeData;


    void Start()
    {
        int atomIdx = 0; // Because children can also be cylinders we have an idx that reference just atoms
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var go = gameObject.transform.GetChild(i); // Gets the children of the molecule prefab (cylinders and QuestionMarks)
            if (go.name.Contains("Q")) // All atom prefab names begin with Q
            {
                // Atom data are set according to hirearchy
                AtomManager am = go.GetComponent<AtomManager>();
                am.elementData = moleculeData.elements[atomIdx]; // Sets the data of the atom
                atomIdx++;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
