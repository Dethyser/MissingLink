using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Entity {
    [SerializeField] private float movementSpeed;

    private bool canMove = true;

    private EntityController controller;
    private Vector3 direction;

    private enum ViewDirection {
        up,
        down,
        left,
        right,
    }

    private ViewDirection viewDirection = ViewDirection.down;

    protected override void Awake() {
        base.Awake();
        controller = GetComponent<EntityController>();
    }

    private void Update() {
        direction = Vector2.zero;
        if (canMove && !knockbacked) {
            direction = Vector3.up * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");
            ChangeViewDirection();
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (!Interact()) {
                    Attack();
                }
            }
        }
        controller.MoveEntity(direction, movementSpeed);
    }

    private void ChangeViewDirection() {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            if (direction.x > 0.0f) {
                viewDirection = ViewDirection.right;
            }
            else if (direction.x < 0.0f) {
                viewDirection = ViewDirection.left;
            }
        }
        else {
            if (direction.y > 0.0f) {
                viewDirection = ViewDirection.up;
            }
            else if (direction.y < 0.0f) {
                viewDirection = ViewDirection.down;
            }
        }
    }

    private void Attack() {
        Collider2D targetCollider = FindCollider(1.5f);
        Entity target = null;
        if (targetCollider != null) {
            target = targetCollider.gameObject.GetComponent<Entity>();
        }
        StartCoroutine(SwingSword());
        if (target != null) {
            target.TakeDamage(damageAmount, knockbackStrength, transform.position);
            //return true;
        }
        //return false;
    }

    private bool Interact() {
        Collider2D interactableCollider = FindCollider(0.25f);
        IInteractable interactable = null;
        if (interactableCollider != null) {
            interactable = interactableCollider.gameObject.GetComponent<IInteractable>();
        }
        if (interactable != null) {
            interactable.Interact();
            return true;
        }
        return false;
    }

    private Collider2D FindCollider(float searchSize) {
        Vector2 interactionLocation = Vector2.zero;
        int layermask =~ LayerMask.GetMask("Player");
        switch (viewDirection) {
            case ViewDirection.right:
                interactionLocation = (Vector2)transform.position + Vector2.right;
                break;
            case ViewDirection.left:
                interactionLocation = (Vector2)transform.position + Vector2.left;
                break;
            case ViewDirection.up:
                interactionLocation = (Vector2)transform.position + Vector2.up;
                break;
            case ViewDirection.down:
                interactionLocation = (Vector2)transform.position + Vector2.down;
                break;
            default:
                break;
        }
        return Physics2D.OverlapBox(interactionLocation, Vector2.one * searchSize, 0.0f, layermask);
    }

    private IEnumerator SwingSword() {

        canMove = false;

        for (float t = 0; t < 0.25f; t += Time.deltaTime) {
            yield return null;
        }
        canMove = true;
    }

    protected override void Die() {
        Debug.Log("PlayerMovement died");
    }
}
