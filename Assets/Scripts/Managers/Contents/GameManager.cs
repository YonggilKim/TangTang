using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController _player;
    public PlayerController Player
    {
        get
        {
            if(_player == null)
                return FindObjectOfType<PlayerController>();
            return _player;

        }
        set 
        {
            _player = value; 
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
