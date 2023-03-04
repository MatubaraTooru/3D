using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField] int _maxHP = 100;
    public int CurrentHP { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = _maxHP;
    }
}
