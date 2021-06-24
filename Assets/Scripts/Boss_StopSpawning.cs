using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_StopSpawning : MonoBehaviour
{
    [SerializeField]
    private Boss_Behavior _bossBehavior;

    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _bossBehavior = GameObject.Find("EnemyMotherShip").GetComponent<Boss_Behavior>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            _spawnManager.OnPlayerDeath();
            _bossBehavior.BossStart();
        }
       
    }
}
