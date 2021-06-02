using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 19.0f;
    [SerializeField]
    private GameObject _explosion;

    
    private SpawnManager _spawnManager;

    //Audio
    [SerializeField]
    private AudioSource _explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("Spawn_Manager not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Laser")
        {
            Instantiate(_explosion, transform.position,Quaternion.identity);
            _explosionSound.Play();
            _spawnManager.StartCoroutine();
            Destroy(other.gameObject);
            Destroy(this.gameObject,0.2f);
            
        }
         
    }
}
