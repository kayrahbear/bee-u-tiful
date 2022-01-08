using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour {
  public static PlayerHealthController instance;

  private void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(this);
    }
  }

  private int currentHealth;
  public int maxHealth = 5;

  public float invincibilityLength = 1f;
  private float invincibilityCounter;

  public GameObject[] modelParts;
  private float flashCounter;
  public float flashLength = 0.1f;
  void Start() {

    FillHealth();

  }

  // Update is called once per frame
  void Update() {

    if (invincibilityCounter > 0) {
      invincibilityCounter -= Time.deltaTime;

      flashCounter -= Time.deltaTime;

      if (flashCounter <= 0) {
        flashCounter = flashLength;

        foreach (GameObject part in modelParts) {
          part.SetActive(!part.activeSelf);
        }
      }


    } else {
      invincibilityCounter = 0;
    }
    if (invincibilityCounter <= 0) {
      foreach (GameObject part in modelParts) {
        part.SetActive(true);
      }
    }

    UIController.instance.UpdateHealth(currentHealth);
  }

  public void TakeDamage(int damage) {

    if (invincibilityCounter <= 0) {
      currentHealth -= damage;
      invincibilityCounter = invincibilityLength;
    }

    if (currentHealth <= 0) {
      currentHealth = 0;
      LevelManager.instance.RespawnPlayer();
    }
  }

  public void FillHealth() {
    currentHealth = maxHealth;

    UIController.instance.UpdateHealth(currentHealth);
  }
}
