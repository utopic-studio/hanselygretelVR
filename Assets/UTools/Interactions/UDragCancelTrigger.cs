using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[AddComponentMenu("UTools/Interactions/UDragCancelTrigger")]
public class UDragCancelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UDraggable CurrentDraggable = other.gameObject.GetComponent<UDraggable>();
        if (CurrentDraggable)
        {
            CurrentDraggable.CancelDragOperation();
        }
    }
}
