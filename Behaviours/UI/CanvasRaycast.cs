using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasRaycast : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Code to be place in a MonoBehaviour with a GraphicRaycaster component
        GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();
        //Create the PointerEventData with null for the EventSystem
        PointerEventData ped = new PointerEventData(null);
        //Set required parameters, in this case, mouse position
        ped.position = Input.mousePosition;
        //Create list to receive all results
        List<RaycastResult> results = new List<RaycastResult>();
        //Raycast it
        gr.Raycast(ped, results);

        for (int i = results.Count - 1; i >= 0; i--)
        {
            Hoverable draggable = results[i].gameObject.GetComponent<Hoverable>();

            if (draggable != null)
            {
                draggable.OnHover(i);
            }
        }
    }
}
