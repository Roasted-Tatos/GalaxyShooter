using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStats : MonoBehaviour
{
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Container").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetEverything()
    {
        _player.PlayerScoreReset();
    }
}
