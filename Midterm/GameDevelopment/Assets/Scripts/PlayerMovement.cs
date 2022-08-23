using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [Header ("Coyote Time")]
    [SerializeField] private float coyotaTime;
    private float coyotaCounter;
    [Header ("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;
    [Header ("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [Header ("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Animator animator;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider2D;
    private float horizontalInput;
    private float wallJumpCoolDown;
    private bool justJump;
    [Header ("Sound")]
    [SerializeField] private AudioClip jumpSound;
    private void Awake(){
        justJump = false;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        wallJumpCoolDown = Mathf.Infinity;
    }
    private void Update(){
        if(body.velocity.y == 0) justJump = false;
        else justJump = true;

        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGround());

        if(wallJumpCoolDown > 0.2f){
            if(Input.GetKeyUp(KeyCode.S) && body.velocity.y > 0)
                body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
            if(onWall()){
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else{
                body.gravityScale = 2.5f;
                body.velocity = new Vector2(speed * horizontalInput, body.velocity.y);

                if((isGround() && !justJump) || (body.velocity.y == 0)){
                    coyotaCounter = coyotaTime;
                    jumpCounter = extraJumps;
                }
                else
                    coyotaCounter -= Time.deltaTime;
            }

            if(Input.GetKeyDown(KeyCode.S))
                Jump();
        }
        else
            wallJumpCoolDown += Time.deltaTime;
        
    }  
    private void Jump(){
        if(coyotaCounter < 0 && !onWall() && jumpCounter <= 0) return; 
        SoundManager.instance.PlaySound(jumpSound);
        if(onWall())
            WallJump();
        else{
            if(isGround()){
                body.velocity = new Vector2(body.velocity.x, jumpForce);
            }
            else{
                if(coyotaCounter > 0 ){
                    body.velocity = new Vector2(body.velocity.x, jumpForce);
                }
                else {
                    if(jumpCounter > 0){
                        body.velocity = new Vector2(body.velocity.x, jumpForce);
                        jumpCounter--;
                    }
                }
            }
            coyotaCounter = 0;
            justJump = true;
        }
    }
    private void WallJump(){
        wallJumpCoolDown = 0;
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
    }
    private bool isGround(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack(){
        return !onWall(); // horizontalInput == 0 && isGround()
    }
}
