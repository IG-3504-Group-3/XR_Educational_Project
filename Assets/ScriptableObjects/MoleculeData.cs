using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XR_Education_Project;

[CreateAssetMenu(fileName = "New Molecule", menuName = "Create Molecule")] 

public class MoleculeData : ScriptableObject
{
    public string moleculeName;
    public int numberOfAtoms;
    public ElementData[] elements;
}
