using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  // Start is called before the first frame update
  public float moveSpeed, turnSpeed;
  public Transform[] patrolPoints;
  private int currentPatrolPoint;
  public Rigidbody theRB;
  private Vector3 moveDirection;
  private Vector3 lookTarget;
  private float yStore;
  public Animator anim;

  private PlayerController thePlayer;

  public enum EnemyState {
    idle,
    patrolling,
    chasing,
    attacking,
    returning,
    stagger
  }

  public EnemyState currentState;

  public float waitTime, waitChance;
  private float waitCounter;

  public float chaseDistance, chaseSpeed, loseChaseDistance;
  public float waitBeforeReturn;
  private float returnCounter;
  public float hopForce, waitToChase;
  private float chaseWaitCounter;

  void Start() {

    foreach (Transform patrolPoint in patrolPoints) {
      patrolPoint.parent = null;
    }

    waitCounter = waitTime;

    thePlayer = FindObjectOfType<PlayerController>();

    currentState = EnemyState.idle;
  }

  // Update is called once per frame
  void Update() {


    switch (currentState) {
      case EnemyState.idle:
        anim.SetBool("isRunning", false);
        yStore = theRB.velocity.y;
        theRB.velocity = new Vector3(0f, yStore, 0f);

        waitCounter -= Time.deltaTime;

        if (waitCounter <= 0) {
          currentState = EnemyState.patrolling;
          NextPatrolPoint();
        }
        break;
      case EnemyState.patrolling:
        anim.SetBool("isRunning", true);
        yStore = theRB.velocity.y;
        moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;

        moveDirection.y = 0f;
        moveDirection.Normalize();

        theRB.velocity = moveDirection * moveSpeed;
        theRB.velocity = new Vector3(theRB.velocity.x, yStore, theRB.velocity.z);

        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) <= 1f) {
          NextPatrolPoint();
        } else {
          lookTarget = patrolPoints[currentPatrolPoint].position;
        }
        break;
      case EnemyState.chasing:
        anim.SetBool("isRunning", false);

        lookTarget = thePlayer.transform.position;
        anim.SetBool("isRunning", true);
        if (chaseWaitCounter > 0) {
          chaseWaitCounter -= Time.deltaTime;
        } else {
          yStore = theRB.velocity.y;
          moveDirection = lookTarget - transform.position;

          moveDirection.y = 0f;
          moveDirection.Normalize();

          theRB.velocity = moveDirection * chaseSpeed;
          theRB.velocity = new Vector3(theRB.velocity.x, yStore, theRB.velocity.z);
        }

        if (Vector3.Distance(transform.position, lookTarget) > loseChaseDistance) {
          currentState = EnemyState.returning;
          returnCounter = waitBeforeReturn;
        }
        break;
      case EnemyState.returning:
        returnCounter -= Time.deltaTime;

        if (returnCounter <= 0) {
          currentState = EnemyState.patrolling;
        }
        break;
      case EnemyState.attacking:

        break;

    }

    if (currentState != EnemyState.chasing) {
      if (Vector3.Distance(thePlayer.transform.position, transform.position) <= chaseDistance) {
        currentState = EnemyState.chasing;

        theRB.velocity = Vector3.up * hopForce;
        chaseWaitCounter = waitToChase;
      }
    }

    lookTarget.y = transform.position.y;
    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookTarget - transform.position), turnSpeed * Time.deltaTime);

  }

  public void NextPatrolPoint() {
    if (Random.Range(0f, 100f) < waitChance) {
      waitCounter = waitTime;
      currentState = EnemyState.idle;
    } else {
      currentPatrolPoint++;
      if (currentPatrolPoint >= patrolPoints.Length) {
        currentPatrolPoint = 0;
      }
    }
  }

  public void DoDamage() {
    anim.SetTrigger("Attack");
    PlayerHealthController.instance.TakeDamage(1);
    chaseWaitCounter = waitToChase;
  }

  private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.tag == "Player") {
      DoDamage();
    }
  }

  private void OnCollisionStay(Collision collision) {
    if (collision.gameObject.tag == "Player") {
      DoDamage();
    }
  }
}
