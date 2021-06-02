using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerFire_Trigger : MonoBehaviour
{
    private Enemy_groundFire _groundFire;
    // Start is called before the first frame update
    void Start()
    {
        _groundFire = GameObject.Find("MoonBaseBunker").GetComponent<Enemy_groundFire>();
        if(_groundFire ==null)
        {
            Debug.Log("moonbase bunker not foudn");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        _groundFire.BunkerFire();
    }
}
