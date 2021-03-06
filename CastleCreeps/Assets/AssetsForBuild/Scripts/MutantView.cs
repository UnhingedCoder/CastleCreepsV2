using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantView : MonoBehaviour
{
    [SerializeField] private GameObject infectedMutant;
    [SerializeField] private GameObject healedMutant;
    [SerializeField] private GameObject healFX;
    [SerializeField] private GameObject hitFX;

    private Animator anim;

    private Transform destinationPosition;

    private float moveSpeed;

    private bool canMove = false;

    private float minIdleWaitTime = 5;
    private float maxIdleWaitTime = 7;

    private float idleWaitTime;
    private float idleWaitTimer;


    private float minWalkWaitTime = 15;
    private float maxWalkWaitTime = 25;

    private float walkWaitTimer;
    private float walkWaitTime;

    private bool hasReachedDestination = false;

    private LifeManager mLifeManager;

    private GameManagerView mGameManager;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        mLifeManager = FindObjectOfType<LifeManager>();
        mGameManager = FindObjectOfType<GameManagerView>();
    }

    public void Init(Transform destPos, float speed)
    {
        destinationPosition = destPos;
        moveSpeed = speed;
        DisableMovement();

        Reset();

        infectedMutant.SetActive(true);
        healedMutant.SetActive(false);
    }

    private void Update()
    {
        HandleWalking();

        HandleReachingDestination();
    }

    private void HandleWalking()
    {

        if (this.idleWaitTimer < this.idleWaitTime)
        {
            this.idleWaitTimer += Time.deltaTime;

            if (this.idleWaitTimer >= this.idleWaitTime)
            {
                EnableMovement();
                this.walkWaitTimer = 0;
                this.walkWaitTime = Random.Range(minWalkWaitTime, maxWalkWaitTime);
                anim.SetTrigger("WALK");
            }
        }
        else
        {
            if (this.walkWaitTimer < this.walkWaitTime)
            {
                this.walkWaitTimer += Time.deltaTime;

                if (this.walkWaitTimer >= this.walkWaitTime)
                {
                    DisableMovement();
                    this.idleWaitTimer = 0;
                    this.idleWaitTime = Random.Range(minIdleWaitTime, maxIdleWaitTime);
                    anim.SetTrigger("IDLE");
                }
            }
        }


        if (canMove)
        {
            if (this.transform.position.z > destinationPosition.position.z)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, destinationPosition.position, Time.deltaTime * moveSpeed);
            }
        }
    }

    private void HandleReachingDestination()
    {
        if (this.transform.position.z <= destinationPosition.position.z)
        {
            if (!hasReachedDestination)
            {
                DisableMovement();
                anim.SetTrigger("LOSELIFE");
                mLifeManager.LoseALife();
            }

            hasReachedDestination = true;
        }
    }

    public void DisableMutant()
    {
        this.gameObject.SetActive(false);
    }

    public void TakeAHit()
    {
        DisableMovement();
        anim.SetTrigger("HIT");
        hitFX.SetActive(true);
    }

    public void Heal()
    {
        DisableMovement();
        anim.SetTrigger("HEAL");
        healFX.SetActive(true);
        hitFX.SetActive(true);
        StartCoroutine(EnableHealedMutant());
    }

    IEnumerator EnableHealedMutant()
    {
        yield return new WaitForSeconds(2.0f);

        infectedMutant.SetActive(false);
        healedMutant.SetActive(true);

    }

    public void Reset()
    {
        idleWaitTime = Random.Range(minIdleWaitTime, maxIdleWaitTime);
        walkWaitTime = Random.Range(minWalkWaitTime, maxWalkWaitTime);

        idleWaitTimer = 0;
        walkWaitTimer = 99;
    }

    private void DisableMovement()
    {
        canMove = false;
    }

    private void EnableMovement()
    {
        canMove = true;
    }
}
