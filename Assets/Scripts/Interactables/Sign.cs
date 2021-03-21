using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour, IInteractable {
    [SerializeField] private string[] text;
    public void Interact() {
        if(text == null) {
            return;
        }
        for(int i = 0; i < text.Length; i++) {
            Debug.Log(text[i]);
        }
    }
}
