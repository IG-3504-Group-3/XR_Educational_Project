using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XR_Education_Project;

public class PeriodicTable : MonoBehaviour
{

    private int columns = 18;
    private int rows = 7; // Size of the table
    private float spacing = 1.25f; // Spacing between elements
    public GameObject element; // Element prfab to instantiate
    private List<List<int>> elementIdxs = new List<List<int>>(); // 2D Array
    public ElementData[] elementDataArray; // Element data objects added as a list on the inspector
    private List<(int, int)> emptyPos = new List<(int, int)>
    {
        // First Line
        (0,1),
        (0,2),
        (0,3),
        (0,4),
        (0,5),
        (0,6),
        (0,7),
        (0,8),
        (0,9),
        (0,10),
        (0,11),
        (0,12),
        (0,13),
        (0,14),
        (0,15),
        (0,16),
        // Second Line
        (1,2),
        (1,3),
        (1,4),
        (1,5),
        (1,6),
        (1,7),
        (1,8),
        (1,9),
        (1,10),
        (1,11),
        // Third Line
        (2,2),
        (2,3),
        (2,4),
        (2,5),
        (2,6),
        (2,7),
        (2,8),
        (2,9),
        (2,10),
        (2,11),
    };

    void Start()
    {
        var elementHash = elementDataArray.ToDictionary(
            x => (x.rowIdx, x.colIdx),
            x => x
        );

        for (int rowIdx = 0; rowIdx < rows; rowIdx++)
        {
            elementIdxs.Add(new List<int>());

            for (int colIdx = 0; colIdx < columns; colIdx++)
            {
                elementIdxs[rowIdx].Add(colIdx);
                if (!emptyPos.Contains((rowIdx, colIdx)))
                {
                    Vector3 pos = new Vector3(colIdx, -rowIdx, 0) * spacing;
                    var newElement = Instantiate(element, pos, Quaternion.identity);
                    // Change the element properties
                    newElement.GetComponent<Element>().elementData = elementHash[(0, 0)]; // Change (0,0) to rowIdx and colIdx to get dynamically
                    // Change the colour depending on their group
                    newElement.transform.parent = gameObject.transform;
                }
            }
        }



    }

    void Update()
    {

    }

}
