using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour {
    [SerializeField] private Shot shotPrefab;
    [SerializeField] private float shootCooldown;

    private enum ShootDirection {
        right,
        left,
        up,
        down,
    }

    [SerializeField] private ShootDirection shootDirection;

    private Vector2 shotOutputPosition;
    private float shotRotation;

    private void Awake() {
        switch (shootDirection) {
            case ShootDirection.right:
                shotOutputPosition = Vector2.right;
                shotRotation = 270.0f;
                break;
            case ShootDirection.left:
                shotOutputPosition = Vector2.left;
                shotRotation = 90.0f;
                break;
            case ShootDirection.up:
                shotOutputPosition = Vector2.up;
                shotRotation = 0.0f;
                break;
            case ShootDirection.down:
                shotOutputPosition = Vector2.down;
                shotRotation = 180.0f;
                break;
            default:
                shotOutputPosition = Vector2.zero;
                shotRotation = 0.0f;
                break;
        }
    }

    private void Start() {
        StartCoroutine(ShotArrow());
    }

    private IEnumerator ShotArrow() {

        for (float t = 0; t < shootCooldown; t += Time.deltaTime) {
            yield return null;
        }

        Shot newShot = Instantiate(shotPrefab, (Vector2)transform.position + shotOutputPosition, Quaternion.Euler(0.0f,0.0f, shotRotation));
        newShot.SetDirection(shotOutputPosition);
        StartCoroutine(ShotArrow());
    }
}
