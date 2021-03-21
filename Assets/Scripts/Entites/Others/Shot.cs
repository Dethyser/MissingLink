using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
    [SerializeField] private float movementSpeed;
    [SerializeField] protected int damageAmount;
    [SerializeField] protected float knockbackStrength;

    private EntityController controller;

    private bool canFly = false;
    private Vector2 flyDirection;

    private void Awake() {
        controller = GetComponent<EntityController>();
    }

    private void Update() {
        controller.MoveEntity(flyDirection, movementSpeed);
    }

    public void SetDirection(Vector2 direction) {
        flyDirection = direction;
        canFly = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Entity target = collision.gameObject.GetComponent<Entity>();
        if(target != null) {
            target.TakeDamage(damageAmount, knockbackStrength, (Vector2)collision.gameObject.transform.position - flyDirection);
        }
        Destroy(gameObject);
    }
}
