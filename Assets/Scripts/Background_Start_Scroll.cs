using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Start_Scroll : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _anim = GetComponent<Animator>();
        _anim.Play("Default");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAnimation()
    {
        _anim.Play("Background_Scroll");
    }
}
