using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {
  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Player") {
      PlayerHealthController.instance.TakeDamage(1);
    }
  }
}
