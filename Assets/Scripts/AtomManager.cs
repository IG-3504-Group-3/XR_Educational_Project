using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XR_Education_Project;

public class AtomManager : MonoBehaviour
{
    private static List<GameObject> instantiatedAtoms = new List<GameObject>();
    public ElementData elementData;
    public bool isFilled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fill()
    {
        // Replaces the texture of the atom with one found in the specific path
        // Textures should be named just like their atomic symbol

        Texture2D texture = Resources.Load<Texture2D>(Path.Join("Textures", elementData.atomicSymbol));
        if (texture == null) 
        {
            Debug.LogError($"Texture for symbol '{elementData.atomicSymbol}' not found in Resources/Textures/");
        }
        gameObject.GetComponent<Renderer>().material.mainTexture = texture;
        isFilled = true;
    }

    public static void  AddAtom(GameObject newAtom) {
        Collider newAtomCollider = newAtom.GetComponent<Collider>();
        
        foreach (GameObject existingAtom in instantiatedAtoms)
        {
            Collider existingAtomCollider = existingAtom.GetComponent<Collider>();
            Physics.IgnoreCollision(newAtomCollider, existingAtomCollider);
        }

        instantiatedAtoms.Add(newAtom);
        Debug.Log($"Atom created. Total Atoms: {instantiatedAtoms.Count}");
    }

    public static void RemoveAtom(GameObject currentAtom)
    {
        if (instantiatedAtoms.Contains(currentAtom))
        {
            for (int i = instantiatedAtoms.Count - 1; i >= 0; i--)
            {
                if (instantiatedAtoms[i] == currentAtom)
                {
                    instantiatedAtoms.RemoveAt(i);
                    GameObject.Destroy(currentAtom);
                    break;
                }
            }
        }
    }

    public static void RemoveAllAtoms()
    {
        for (int i = instantiatedAtoms.Count - 1; i >= 0; i--)
        {
            GameObject atom = instantiatedAtoms[i];
            instantiatedAtoms.RemoveAt(i); // Remove atom by index
            GameObject.Destroy(atom); // Destroy the atom
        }
    }

}
