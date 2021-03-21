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

    protected bool knockbacked = false;

    protected virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount, float knockbackStrength, Vector3 attackerPosition) {
        float currentKnockback = knockbackStrength - knockbackResistance;
        if (currentKnockback > 0.0f) {
            Vector2 newPosition = (transform.position - attackerPosition).normalized * currentKnockback;
            StartCoroutine(KnockbackCoroutine(transform.position, (Vector2)transform.position + newPosition, currentKnockback));
        }
        if (!isInvincible) {
            currentHealth -= damageAmount;
            StartCoroutine(InvincibilityCoroutine(0.25f));
            if(currentHealth <= 0) {
                Die();
            }
        }
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }
    private IEnumerator InvincibilityCoroutine(float duration) {
        isInvincible = true;

        for (float t = 0; t < duration; t += Time.deltaTime) {
            yield return null;
        }

        isInvincible = false;
    }
    private IEnumerator KnockbackCoroutine(Vector2 startPosition, Vector2 endPosition, float currentKnockback) {
        knockbacked = true;

        for (float t = 0; t < 1.0f; t += Time.deltaTime * currentKnockback * 5 ){
            transform.position = Vector2.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        knockbacked = false;
    }
}
