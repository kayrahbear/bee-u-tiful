using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public CharacterController charCon;

    private CameraController camCon;

    private Vector3 moveAmount;

    // Start is called before the first frame update
    void Start()
    {
        camCon = FindObjectOfType<CameraController>();
        charCon = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveAmount = (camCon.transform.forward * Input.GetAxis("Vertical")) + (camCon.transform.right * Input.GetAxis("Horizontal"));

        moveAmount.y = 0f;

        moveAmount = moveAmount.normalized;

        charCon.Move(moveAmount * moveSpeed * Time.deltaTime);
    }
}