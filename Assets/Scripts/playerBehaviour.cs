using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public enum SIDE { Left,Mid,Right}
public class playerBehaviour : MonoBehaviour
{
    public SIDE char_Side = SIDE.Mid;
    
    Animator animator;
    gameBehaviour gameBeh;

    float nextPos = 0f;
    public float XValue;
    private CharacterController myChar;
    private float x;
    private float y;
    //private bool isJumping;
    
    public float transitionSpeed = 10f;
    public float jumpPower = 8f;

    public float playerSpeed = 10f;

    private float cooldownTime = 4f;
    private bool controllOff;

    private bool attack = true;

    

    // Start is called before the first frame update
    void Start()
    {
        gameBeh = FindObjectOfType<gameBehaviour>().GetComponent<gameBehaviour>();
        animator = GetComponent<Animator>();
        myChar = GetComponent<CharacterController>();
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!controllOff) { 
        //movePlayer(playerSpeed);

        if (SwipeManager.swipeRight)
        {
            //pomeranje desno
            if (char_Side == SIDE.Mid)
            {
                nextPos = XValue;
                char_Side = SIDE.Right;
                animator.Play("RunningRight");
            }
            else if(char_Side ==SIDE.Left){
                nextPos = 0f;
                char_Side = SIDE.Mid;
                animator.Play("RunningRight");
            }

        }else if (SwipeManager.swipeLeft)
        {
            //okretanje levo
            if (char_Side == SIDE.Mid)
            {
                nextPos = -XValue;
                char_Side = SIDE.Left;
                
                animator.Play("RunningLeft");
            }
            else if (char_Side == SIDE.Right)
            {
                nextPos = 0f;
                char_Side = SIDE.Mid;
                animator.Play("RunningLeft");
            }

         }
		}
        x = Mathf.Lerp(x, nextPos, Time.deltaTime * transitionSpeed);
        Vector3 moveVector = new Vector3(x - transform.position.x, y*Time.deltaTime, playerSpeed*Time.deltaTime);
        //
        myChar.Move(moveVector);
        Jump();
        
    }

    public void Jump()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
        {
            //
            //isJumping = false;
        }
        if (myChar.isGrounded)
        {
            if (SwipeManager.swipeUp && !controllOff)
            {
                y = jumpPower;
                //isJumping = true;
                //jump animacija ovde
            }
        }
        else
        {
            y -= jumpPower * 2 * Time.deltaTime;
            if (myChar.velocity.y < -0.1f)
                Debug.Log("pada");//ovde animator umesto db.log
        }
    }

    public void wallHit()
    {
        playerSpeed = 0f;
        controllOff = true;
        animator.Play("WallHit");
        StartCoroutine(waitTime(cooldownTime,false));
        //
    }

    private IEnumerator waitTime(float cooldownTime, bool end)
    {
        
        yield return new WaitForSeconds(cooldownTime);
        if (end)
        {
            gameBeh.showEndScreen();
        }
        else { gameBeh.showFailScreen(); }
        
    }
    public void endOfLevel()
    {
        controllOff = true;
        nextPos = 0f;
        
        myChar.Move((nextPos-transform.position.x)*Vector3.right);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            wallHit();
        }
        if (other.gameObject.tag == "Item")
        {
            gameBeh.collectItem();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "finish")
        {
            endOfLevel();
        }
        if (other.gameObject.tag == "Fall")
        {
            gameBeh.retryLevel();
        }
        if (other.gameObject.tag == "Kick")
        {
            if (gameBeh.getItemsCollected() > 0)
            {
                gameBeh.strikeCustomer(other.GetComponentInParent<Rigidbody>());
                if (attack)
                {
                    animator.Play("Attack");

                }
                else
                {
                    animator.Play("Attack2");
                }
                attack = !attack;
            }
            else
            {
                playerSpeed = 0f;
                controllOff = true;
                animator.Play("Defeat");
                StartCoroutine(waitTime(cooldownTime,false));
            }
        }
        if(other.gameObject.tag == "Victory")
        {
            playerSpeed = 0f;
            controllOff = true;
            animator.Play("Victory");
            StartCoroutine(waitTime(cooldownTime,true));

        }
    }

}
