using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Education_Project {
    [CreateAssetMenu(fileName = "NewElement", menuName = "CreateNewElement")] //Create new element by right-clicking in elements folder: Create > CreateNewElement
    public class ElementData : ScriptableObject
    {
        public int rowIdx; 
        public int colIdx;
        public int atomicNumber;
        public string elementName;
        public string atomicSymbol;
        public float atomicMass;
        public string elementClass;
        public int group;
        public string groupName;
        public float boilingPoint;
        public float meltingPoint;
        public float electronAffinity;
        public string electronConfiguration;
        public string keyIsotopes;
    }
}
