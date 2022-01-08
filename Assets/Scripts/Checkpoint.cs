using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
  public GameObject effect;
  public Renderer checkpoint_flag;
  public Material active_flag;
  public Material inactive_flag;
  private Material[] flag_mats;
  public Transform effectPoint;

  private void OnTriggerEnter(Collider other) {
    flag_mats = checkpoint_flag.materials;

    if (other.tag == "Player") {
      if (LevelManager.instance.respawnPoint != transform.position) {
        LevelManager.instance.respawnPoint = transform.position;

        if (effect != null) {
          Instantiate(effect, effectPoint.position, effectPoint.rotation);
        }

        Checkpoint[] all_checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint cp in all_checkpoints) {
          flag_mats[1] = cp.inactive_flag;
          cp.checkpoint_flag.materials = flag_mats;
        }

        flag_mats[1] = active_flag;
        checkpoint_flag.materials = flag_mats;
      }



    }
  }

}
