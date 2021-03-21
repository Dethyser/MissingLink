using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : Entity {
    [SerializeField] private float movementSpeed;
    [SerializeField] private float followDistance;
    [SerializeField] private ViewRange viewRange;
    [SerializeField] private WallCheck wallCheck;
    [SerializeField] private ViewDirection startdirection;

    private EntityController controller;
    private Vector3 direction;
    private GameObject player;
    private float CheckForPlayer;

    private enum ViewDirection {
        up,
        down,
        left,
        right,
    }

    private ViewDirection viewDirection;


    protected override void Awake() {
        base.Awake();
        controller = GetComponent<EntityController>();
        viewDirection = startdirection;
        ChangeViewDirection();
    }
    void Update()
    {
        
        if(CheckForPlayer > 0.0f) {
            CheckForPlayer -= Time.deltaTime;
        }
        else {
            direction = Vector3.zero;
            if (viewRange.PlayerInViewRange() != null) {
                CheckForPlayer = 1.0f;
                player = viewRange.PlayerInViewRange();
                direction = player.transform.position - transform.position;
            }
            else if (player != null) {
                if ((player.transform.position - transform.position).magnitude > followDistance) {
                    CheckForPlayer = 1.0f;
                    player = null;
                }
                else {
                    CheckForPlayer = 1.0f;
                    direction = player.transform.position - transform.position;
                    ChangeViewDirection();
                }
            }
            else {
                if (wallCheck.FoundWall()) {
                    TurnAround();
                }
                switch (viewDirection) {

                    case ViewDirection.up:
                        direction = Vector3.up;
                        break;
                    case ViewDirection.down:
                        direction = Vector3.down;
                        break;
                    case ViewDirection.right:
                        direction = Vector3.right;
                        break;
                    case ViewDirection.left:
                        direction = Vector3.left;
                        break;
                    default:
                        break;
                }
            }
        }
        controller.MoveEntity(direction, movementSpeed);
    }

    private void ChangeViewDirection() {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            if (direction.x > 0.0f) {
                viewRange.transform.rotation = Quaternion.Euler(0, 0, 270);
                viewDirection = ViewDirection.right;
            }
            else if (direction.x < 0.0f) {
                viewRange.transform.rotation = Quaternion.Euler(0, 0, 90);
                viewDirection = ViewDirection.left;
            }
        }
        else {
            if (direction.y > 0.0f) {
                viewRange.transform.rotation = Quaternion.Euler(0, 0, 0);
                viewDirection = ViewDirection.up;
            }
            else if (direction.y < 0.0f) {
                viewRange.transform.rotation = Quaternion.Euler(0, 0, 180);
                viewDirection = ViewDirection.down;
            }
        }
    }

    private void TurnAround() {
        switch (viewDirection) {
            case ViewDirection.up:
                viewRange.transform.rotation = Quaternion.Euler(0, 0, 180);
                viewDirection = ViewDirection.down;
                break;
            case ViewDirection.down:
                viewRange.transform.rotation = Quaternion.Euler(0, 0, 0);
                viewDirection = ViewDirection.up;
                break;
            case ViewDirection.right:
                viewRange.transform.rotation = Quaternion.Euler(0, 0, 90);
                viewDirection = ViewDirection.left;
                break;
            case ViewDirection.left:
                viewRange.transform.rotation = Quaternion.Euler(0, 0, 270);
                viewDirection = ViewDirection.right;
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<Entity>().TakeDamage(damageAmount, knockbackStrength, transform.position);
        }
    }

    protected override void Die() {
        DropItem();
        base.Die();
    }

    private void DropItem() {

    }
}
