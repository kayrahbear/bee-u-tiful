using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nectar : MonoBehaviour {
  // Start is called before the first frame update
  public GameObject collect_effect;
  public Transform collect_effect_pos;

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Player") {

      if (collect_effect != null) {
        Instantiate(collect_effect, collect_effect_pos.position, collect_effect_pos.rotation);
      }

      PlayerHealthController.instance.FillHealth();

      Destroy(gameObject);
    }
  }
}
