using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Animator animator;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider2D;
    private float horizontalInput;
    private float wallJumpCoolDown;
    private void Awake(){
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Update(){
        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGround());

        if(wallJumpCoolDown > 0.2f){
            body.velocity = new Vector2(speed * horizontalInput, body.velocity.y);
            
            if(onWall() && !isGround()){
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else{
                body.gravityScale = 2.5f;
            }
            if(Input.GetKey(KeyCode.Space))
                Jump();

        }else{
            wallJumpCoolDown += Time.deltaTime;
        }
    }  
    private void Jump(){
        if(isGround()){
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            animator.SetTrigger("jump");
        }else if(onWall() && !isGround()){
            if(horizontalInput == 0){
                body.velocity = new Vector2(-Mathf.Sign(transform.lossyScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x) * Mathf.Abs(transform.localScale.x), 
                    transform.localScale.y, transform.localScale.z);
            }
            else{
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCoolDown = 0;
        }
        
    }
    private bool isGround(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
