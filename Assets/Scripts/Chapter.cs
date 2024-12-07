using System.Collections;
using System.Linq;
using UnityEngine;
using XR_Education_Project;

public static class ChapterSystem // This class since its static can be accessed from everywhere
{
    private static ArrayList goalMoleculesData;
    public static float ?startTime;

    public static void StartChapter(ElementData element) // Initializes the chapet
    {
        goalMoleculesData = new ArrayList();
        SetGoalMolecules(element);
        startTime = Time.time;
    }

    public static void SetGoalMolecules(ElementData element) // Adds all molecules that contain the element to the goal molecules of the chapter
    {
        var molecules = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().allMoleculeData;
        foreach (var molecule in molecules)
        {
            if (molecule.elements.Contains(element))
            {
                goalMoleculesData.Add(molecule);
            }
        }
    }

    public static float GetScore() // Returns the time since the start of the chapter
    {
        return Time.time - (float) startTime;
    }

    public static void EndChapter() // Ends the chapter
    {
        startTime = null;
        goalMoleculesData.Clear();
    }

    

}
