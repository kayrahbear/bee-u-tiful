using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

  public static UIController instance;

  private void Awake() {
    instance = this;
  }

  public Image fadeImage;
  private bool isFadingToBlack, isFadingFromBlack;
  public float fadeSpeed = 0.8f;

  public Slider healthSlider;
  public TMP_Text healthText, timerText, pollenText;

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {

    if (isFadingToBlack) {
      fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.MoveTowards(fadeImage.color.a, 1f, fadeSpeed * Time.deltaTime));
      if (fadeImage.color.a == 1f) {
        isFadingToBlack = false;
      }
    }

    if (isFadingFromBlack) {
      fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.MoveTowards(fadeImage.color.a, 0f, fadeSpeed * Time.deltaTime));
      if (fadeImage.color.a == 0f) {
        isFadingFromBlack = false;
      }
    }
  }

  public void FadeToBlack() {
    isFadingToBlack = true;
    isFadingFromBlack = false;
  }

  public void FadeFromBlack() {
    isFadingToBlack = false;
    isFadingFromBlack = true;
  }

  public void UpdateHealth(int currentHealth) {
    healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
    healthSlider.value = currentHealth;
    healthText.text = "Health: " + currentHealth + "/" + PlayerHealthController.instance.maxHealth;
  }
}
