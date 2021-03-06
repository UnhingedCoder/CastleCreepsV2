using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CannonShootController : MonoBehaviour
{
    public static CannonShootController Instance { get; private set; }

    [SerializeField] private float projectileSpeed;

    [Header("LANE")]
    [SerializeField] Transform cannonTransform = default;
    [SerializeField] List<Transform> cannonPositionsTransform = default;

    [Header("PROJECTILE")]
    [SerializeField] List<Transform> cannonFirePositionsTransform = default;
    [SerializeField] GameObject cannonFireGlowFX;

    [Header("ABACUS OVERHEAD DISPLAY")]
    [SerializeField] private CannonAbacusDisplayView abacus100sView;
    [SerializeField] private CannonAbacusDisplayView abacus10sView;
    [SerializeField] private CannonAbacusDisplayView abacus1sView;

    private bool inTween = false;

    private int currLaneIndex = 0;
    public int CurrentLaneIndex { get => currLaneIndex; }

    public bool canFireProjectile { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        canFireProjectile = true;
    }


    #region CANNON POSITION
    public void SetCannonPosition(int laneIndex)
    {
        if (laneIndex == -1)
            return;

        currLaneIndex = laneIndex;

        if (!inTween)
        {
            cannonTransform.DOKill();
            inTween = true;


            Vector3 pos = cannonPositionsTransform[currLaneIndex].position;

            cannonTransform.DOMoveX(pos.x, 0.25f).OnComplete(() =>
            {
                inTween = false;
            });
        }
    }

    public Vector3 GetCannonFirePosition()
    {
        return cannonFirePositionsTransform[currLaneIndex].position;
    }

    #endregion

    #region PROJECTILE FIRING
    public void FireInLane(int val)
    {
        //Fire projectile only if mutants are present in lane and cannon not in cooldown and abacus value is not 0
        if (canFireProjectile && MutantManager.Instance.mutantInLanes[currLaneIndex] != null && val > 0)
        {
            Vector3 destPos = MutantManager.Instance.mutantInLanes[currLaneIndex].position;
            Mutant mutantInLane = MutantManager.Instance.mutantInLanes[currLaneIndex].GetComponent<Mutant>();
            StartCoroutine(FireCo(val, destPos, mutantInLane));
            StartCoroutine(FireCooldownCo());
        }
    }

    IEnumerator FireCo(int val, Vector3 destinationPos, Mutant m)
    {
        canFireProjectile = false;

        FireProjectileFX();

        Vector3 spawnPos = GetCannonFirePosition();

        GameObject p = ObjectPooler.Instance.GetPooledObject("Projectile");

        p.transform.position = spawnPos;

        yield return new WaitForSeconds(0.5f);
        p.SetActive(true);


        while (p.transform.position.z < destinationPos.z)
        {
            p.transform.position = Vector3.MoveTowards(p.transform.position, destinationPos, Time.deltaTime * projectileSpeed);
            yield return new WaitForEndOfFrame();
        }

        m.OnTakingHit(val);

        yield return new WaitForSeconds(0.1f);

        p.SetActive(false);

    }

    IEnumerator FireCooldownCo()
    {
        yield return new WaitForSeconds(2f);
        canFireProjectile = true;
    }


    public void FireProjectileFX()
    {
        cannonFireGlowFX.SetActive(true);
    }
    #endregion

    #region ABACUS OVERHEAD DISPLAY
    public void SetCannonOverheadAbacusValue(int newValue, int oldValue)
    {
        int initOnes = (oldValue % 10);
        int initTens = (oldValue % 100 - initOnes) / 10;
        int initHundreds = (oldValue % 1000 - initTens - initOnes) / 100;


        int newOnes = (newValue % 10);
        int newTens = (newValue % 100 - newOnes) / 10;
        int newHundreds = (newValue % 1000 - newTens - newOnes) / 100;

        abacus100sView.SetCannonAbacusValue(newHundreds, initHundreds);
        abacus10sView.SetCannonAbacusValue(newTens, initTens);
        abacus1sView.SetCannonAbacusValue(newOnes, initOnes);
    }
    #endregion
}
