using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullethole : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private float timer;
    public void OnObjectSpawn(){
        StartCoroutine(DelayedDeactivate(timer));
    }
    IEnumerator DelayedDeactivate(float timer){
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}