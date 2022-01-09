using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour {
  public static LevelManager instance;

  private void Awake() {
    instance = this;

  }

  public float waitToRespawn;

  private CameraController theCamera;

  private PlayerController thePlayer;

  public Vector3 respawnPoint;

  [HideInInspector]
  public bool respawning;

  [HideInInspector]
  public float levelTimer;

  public int currentPollenCount;
  // Start is called before the first frame update
  void Start() {
    thePlayer = FindObjectOfType<PlayerController>();

    respawnPoint = thePlayer.transform.position;

    theCamera = FindObjectOfType<CameraController>();
  }

  // Update is called once per frame
  void Update() {

    levelTimer += Time.deltaTime;
    UIController.instance.timerText.text = "Time: " + Mathf.Round(levelTimer);

  }

  public void RespawnPlayer() {
    if (!respawning) {
      respawning = true;
      StartCoroutine("RespawnPlayerCo");
    }
  }

  public IEnumerator RespawnPlayerCo() {
    yield return new WaitForSeconds(2f);
    thePlayer.gameObject.SetActive(false);
    UIController.instance.FadeToBlack();
    yield return new WaitForSeconds(waitToRespawn);
    thePlayer.transform.position = respawnPoint;
    theCamera.SnapToTarget();
    thePlayer.gameObject.SetActive(true);

    respawning = false;

    UIController.instance.FadeFromBlack();

    PlayerHealthController.instance.FillHealth();
  }

  public void GetPollen() {
    currentPollenCount++;

    UIController.instance.pollenText.text = currentPollenCount.ToString();
  }
}
