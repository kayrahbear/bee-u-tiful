using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : MonoBehaviour {

  public GameObject collect_effect;
  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Player") {

      if (collect_effect != null) {
        Instantiate(collect_effect, transform.position, transform.rotation);
      }
      LevelManager.instance.GetPollen();

      Destroy(gameObject);
    }
  }
}
