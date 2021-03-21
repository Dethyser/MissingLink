using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionField : MonoBehaviour
{
    [SerializeField]private IInteractable interactable;
    private void OnTriggerEnter2D(Collider2D collision) {
        interactable = collision.gameObject.GetComponent<IInteractable>();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<IInteractable>() == interactable) {
            interactable = null;
        }
    }

    public bool StartInteraction() {
        if(interactable != null) {
            interactable.Interact();
            return true;
        }
        return false;
    }
}
