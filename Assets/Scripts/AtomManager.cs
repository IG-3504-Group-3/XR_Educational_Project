using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/Textures/" + elementData.atomicSymbol + ".png", typeof(Texture2D));
        if (texture == null) 
        {
            Debug.LogError($"Texture for symbol '{elementData.atomicSymbol}' not found in Resources/Textures/");
        }
        gameObject.GetComponent<Renderer>().material.mainTexture = texture;
        isFilled = true;
    }

    public static void  AddAtom(GameObject newAtom) {
        instantiatedAtoms.Add(newAtom);
        Debug.Log($"Atom created. Total Atoms: {instantiatedAtoms.Count}");
    }

    public static void RemoveAtom(GameObject currentAtom)
    {
        if (instantiatedAtoms.Contains(currentAtom))
        {
            instantiatedAtoms.Remove(currentAtom);
            GameObject.Destroy(currentAtom);
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
