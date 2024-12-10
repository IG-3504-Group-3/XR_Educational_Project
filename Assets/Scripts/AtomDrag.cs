using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Education_Project{
    public class AtomDrag : MonoBehaviour
    {
        private Camera mainCamera;
        private bool isDragging = false;
        private Vector3 dragOffset;
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
                        //dragOffset = transform.position - hit.point;
                    }
                }
            }

            if (isDragging && Input.GetMouseButton(0)) // Drag the atom
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z); // Update position
                    Vector3 targetPosition = hit.point;
                    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
                }
            }

            if (Input.GetMouseButtonUp(0)) // Release to stop dragging
            {
                isDragging = false;
            }
            
        }
    }
}
