using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Education_Project {
    public class Element : MonoBehaviour
    {
        private GameManager gameManager;
        [HideInInspector] public ElementData elementData;

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        void OnMouseDown() // Replace with VR interaction later
        {
            gameManager.OnElementClicked(elementData);
        }
    }
}
