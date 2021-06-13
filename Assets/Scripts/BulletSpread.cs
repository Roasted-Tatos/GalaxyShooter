using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpread : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y > 7.8f || transform.position.y < -7.8f || transform.position.x < -10f || transform.position.x > 10f)
        {
            Destroy(gameObject);
        }
        
        if (_player == null)
        {
            Debug.Log("Ya dead boi");
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(_player != null)
            {
                _player.Damage();
            }
            Destroy(this.gameObject,0.1f);
        }
    }
}
