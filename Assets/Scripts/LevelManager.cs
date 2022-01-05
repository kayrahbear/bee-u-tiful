using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour {
  public static LevelManager instance;

  private void Awake() {
    instance = this;

  }

  public float waitToRespawn;

  private PlayerController thePlayer;

  public Vector3 respawnPoint;

  [HideInInspector]
  public bool respawning;
  // Start is called before the first frame update
  void Start() {
    thePlayer = FindObjectOfType<PlayerController>();

    respawnPoint = thePlayer.transform.position + Vector3.up;
  }

  // Update is called once per frame
  void Update() {

  }

  public void RespawnPlayer() {
    if (!respawning) {
      respawning = true;
      StartCoroutine("RespawnPlayerCo");
    }
  }

  public IEnumerator RespawnPlayerCo() {
    thePlayer.gameObject.SetActive(false);
    UIController.instance.FadeToBlack();
    yield return new WaitForSeconds(waitToRespawn);
    thePlayer.transform.position = respawnPoint;
    thePlayer.gameObject.SetActive(true);

    respawning = false;

    UIController.instance.FadeFromBlack();
  }
}
