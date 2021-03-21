using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    private bool foundWall = false;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.gameObject.CompareTag("Player")){
            foundWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (!collision.gameObject.CompareTag("Player")) {
            foundWall = false;
        }
    }

    public bool FoundWall() {
        return foundWall;
    }
}
