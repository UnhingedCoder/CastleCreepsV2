using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MutantManager : MonoBehaviour
{
    public static MutantManager Instance { get; private set; }

    [SerializeField] List<int> occupiedLanes = new List<int>();
    [SerializeField] List<Transform> mutantSpawnPoints;
    [SerializeField] List<Transform> mutantEndPoints;

    [SerializeField] List<string> mutantTags;

    public List<Transform> mutantInLanes = new List<Transform>();

    [SerializeField] private float spawnInterval;
    [SerializeField] private float mutantMoveSpeed;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        InvokeRepeating("SpawnMutant", 1.3f, 3f);
    }

    private int GetFreeLane()
    {
        if (occupiedLanes.Count >= 3)
        {
            Debug.Log("ALL LANES OCCUPIED");
            return -1;
        }

        List<int> freeLanes = new List<int> { 0, 1, 2 };

        for (int i = 0; i < occupiedLanes.Count; i++)
        {
            freeLanes.Remove(occupiedLanes[i]);
        }

        int index = Random.Range(0, freeLanes.Count);
        occupiedLanes.Add(freeLanes[index]);

        return freeLanes[index];
    }

    private void FreeALane()
    {
        for (int i = 0; i < mutantInLanes.Count; i++)
        {
            if (mutantInLanes[i] != null)
            {
                Mutant m = mutantInLanes[i].GetComponent<Mutant>();
                if (m.Health == 0 && !m.gameObject.activeInHierarchy)
                {
                    mutantInLanes[i] = null;
                    occupiedLanes.Remove(i);
                }
            }
        }
    }

    private int SetMutantHP()
    {
        int hp = Random.Range(1, 999);
        return hp;
    }

    private void SpawnMutant()
    {
        FreeALane();

        int freeLaneIndex = GetFreeLane();

        if (freeLaneIndex < 0)
            return;

        string mTag = mutantTags[Random.Range(0, mutantTags.Count)];
        GameObject mObj = ObjectPooler.Instance.GetPooledObject(mTag);

        Debug.Log(mTag);
        Mutant m = mObj.GetComponent<Mutant>();

        mObj.transform.position = mutantSpawnPoints[freeLaneIndex].position;
        mObj.transform.rotation = mutantSpawnPoints[freeLaneIndex].rotation;

        mutantInLanes[freeLaneIndex] = mObj.transform;

        m.Init(mutantEndPoints[freeLaneIndex], mutantMoveSpeed, SetMutantHP());

        mObj.SetActive(true);
    }
}
