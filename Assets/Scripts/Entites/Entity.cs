using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable {

    [SerializeField] protected int maxHealth;
    [SerializeField] protected int damageAmount;
    [SerializeField] protected float knockbackResistance;
    [SerializeField] protected float knockbackStrength;
    [SerializeField] protected bool isInvincible;

    private int currentHealth;
    private Rigidbody2D rb;

    protected virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount, float knockbackStrength, Vector3 attackerPosition) {
        if ((knockbackResistance - knockbackStrength) > 0.0f) {
            Vector2 newPosition = (transform.position - attackerPosition).normalized * (knockbackResistance - knockbackStrength);
            transform.position = newPosition;
        }
        if (!isInvincible) {
            currentHealth -= damageAmount;
            Debug.Log(currentHealth);
            if(currentHealth <= 0) {
                Die();
            }
        }
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }
}
