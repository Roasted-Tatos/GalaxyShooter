using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    public GameObject _enemyPrefab;
    [SerializeField]
    public GameObject _enemyPrefab2;
    [SerializeField]
    public GameObject[] SecondaryEnemies;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private List<GameObject> powerUps;

    public int[] table_powerUps = { 80, 40, 20, 10, 8, 5, 3 };
    public int total;
    public int randomNumber;
   
    [SerializeField]
    private bool _stopSpawning = false;
    [SerializeField]
    private bool _bossBattle = true;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        BossBattle();
         StartCoroutine();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void StartCoroutine()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnEnemy2Routine());
        StartCoroutine(SpawnMisslesRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BalancedSpawn()
    {
        total = 0;
        foreach(var item in table_powerUps)
        {
            total += item;
        }

        randomNumber = Random.Range(0, total);

        for (int i = 0; i < table_powerUps.Length; i++)
        {
            if (randomNumber<= table_powerUps[i])
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                Instantiate(powerUps[i], posToSpawn, Quaternion.identity);
                return;
            }
            else
            {
                randomNumber -= table_powerUps[i];
            }
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(20f);
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);

        }
    }

    IEnumerator SpawnEnemy2Routine()
    {
        yield return new WaitForSeconds(20f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8,8), 10, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab2, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(10f);

        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(30f);
        while (_bossBattle ==true )
        {
            BalancedSpawn();
            yield return new WaitForSeconds(Random.Range(3f,8f));
        }
    }

    IEnumerator SpawnMisslesRoutine()
    {
        yield return new WaitForSeconds(60f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(SecondaryEnemies[0], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    public void Respawned()
    {
        _stopSpawning = false;
    }
    public void BossBattle()
    {
        _bossBattle = true;
    }

}
