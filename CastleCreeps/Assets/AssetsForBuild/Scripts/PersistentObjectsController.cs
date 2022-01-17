using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectsController : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
