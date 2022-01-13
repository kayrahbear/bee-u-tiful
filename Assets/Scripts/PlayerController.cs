using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  public static PlayerController instance;
  public float moveSpeed;
  public float jumpHeight, gravityScale;
  private float yStore;

  public CharacterController charCon;

  private CameraController camCon;

  private Vector3 moveAmount;

  public float rotateSpeed = 10f;

  public Animator anim;

  // Start is called before the first frame update
  void Start() {
    camCon = FindObjectOfType<CameraController>();
    charCon = GetComponent<CharacterController>();
    anim.SetBool("isGrounded", true);
  }

  private void FixedUpdate() {
    if (!charCon.isGrounded) {
      moveAmount.y = moveAmount.y + (Physics.gravity.y * gravityScale * Time.fixedDeltaTime);
    } else {
      moveAmount.y = (Physics.gravity.y * Time.fixedDeltaTime);
    }
  }

  // Update is called once per frame
  void Update() {
    yStore = moveAmount.y;

    moveAmount = (camCon.transform.forward * Input.GetAxis("Vertical")) + (camCon.transform.right * Input.GetAxis("Horizontal"));
    moveAmount.y = 0f;
    moveAmount = moveAmount.normalized;

    if (moveAmount.magnitude > .1f) {
      if (moveAmount != Vector3.zero) {
        Quaternion newRot = Quaternion.LookRotation(moveAmount);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, rotateSpeed * Time.deltaTime);
      }
    }

    moveAmount.y = yStore;

    if (Input.GetButtonDown("Jump")) {
      Jump();
    }


    charCon.Move(new Vector3(moveAmount.x * moveSpeed, moveAmount.y, moveAmount.z * moveSpeed) * Time.deltaTime);

    float moveVel = new Vector3(moveAmount.x, 0f, moveAmount.z).magnitude * moveSpeed;

    anim.SetFloat("speed", moveVel);
    anim.SetBool("isGrounded", charCon.isGrounded);
    anim.SetFloat("yVel", yStore);
  }

  private void Jump() {
    if (charCon.isGrounded) {
      anim.SetTrigger("jump");
      moveAmount.y = jumpHeight;
    }
  }
}