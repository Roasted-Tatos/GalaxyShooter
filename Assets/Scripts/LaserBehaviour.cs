using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{

    [SerializeField]
    private int _speed = 8;
    private Player _player;
   

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if(transform.position.y > 8)
        {
            if (transform.parent !=null)
            {
                Destroy(transform.parent.gameObject);
            }
            _player.playerLaser.Remove(this.gameObject);

            Destroy(this.gameObject,0.1f);
        }
    }
    
}
