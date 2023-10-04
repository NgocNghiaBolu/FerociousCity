using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    public float playerHealth = 240f;
    public float presentHealth;
    public HealthBarPlayer healthBarPlayer;

    private void Start()
    {
        presentHealth = playerHealth;
        healthBarPlayer.FullHealth(playerHealth);
    }

    public void TakeDamage(float damage)
    {
        presentHealth -= damage;
        healthBarPlayer.SetHealth(presentHealth);

        if(presentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Object.Destroy(gameObject, 1f);
        Cursor.lockState = CursorLockMode.None;
    }

}
