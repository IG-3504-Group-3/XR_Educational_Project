using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR_Education_Project
{
    public class TrashBin : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // Check object is tagged as "Atom"
            if (other.CompareTag("Atom"))
            {
                AtomManager.RemoveAtom(other.gameObject); // Destroy the atom
                Debug.Log("Atom trashed.");
            }
        }
    }
}
