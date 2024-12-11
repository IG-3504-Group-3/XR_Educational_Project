using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Education_Project{
    public class AtomDrag : MonoBehaviour
    {
        private Camera mainCamera;
        private GameObject draggingObject;
        private bool isDragging = false;

        private float smoothSpeed = 15f;

        private void Start()
        {
            mainCamera = Camera.main;
        }


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject) // If atom clicked
                    {
                        isDragging = true;
                        draggingObject = hit.collider.gameObject;
                    }
                }
            }

            if (isDragging && Input.GetMouseButton(0)) // Drag the atom
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 targetPosition = hit.point;
                    draggingObject.transform.position = Vector3.Lerp(draggingObject.transform.position, targetPosition, Time.deltaTime * smoothSpeed);
                }
            }

            if (Input.GetMouseButtonUp(0)) // Release to stop dragging
            {
                isDragging = false;
                draggingObject = null;
            }
            
        }
    }
}
