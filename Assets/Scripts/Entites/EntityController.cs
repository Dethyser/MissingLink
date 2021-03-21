using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour {
    private Rigidbody2D rb;
    private void Awake(){

        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveEntity(Vector3 direction, float movementSpeed) {
        rb.velocity = direction.normalized * movementSpeed;
    }
}
