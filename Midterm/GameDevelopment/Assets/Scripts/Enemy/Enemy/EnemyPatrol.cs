using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;

    [Header ("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header ("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header ("Enemy Animator")]
    [SerializeField] private Animator animator;


    private void Awake(){
        movingLeft = true;
        initScale = enemy.localScale;
    }
    private void Update(){
        if(movingLeft){
            if(enemy.position.x <= leftEdge.position.x)
                DirectionOnChange();
            else
                MoveInDirection(-1);
        }
        else{
            if(enemy.position.x >= rightEdge.position.x)
                DirectionOnChange();
            else
                MoveInDirection(1);
        }
    }
    private void DirectionOnChange(){
        if(animator != null)
            animator.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }
    private void MoveInDirection(int _direction){
        idleTimer = 0;
        if(animator != null)
            animator.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * speed * _direction,
        enemy.position.y, enemy.position.z);
    }
    private void OnDisable(){
        if(animator != null)
            animator.SetBool("moving", false);
    }
}
