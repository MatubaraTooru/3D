using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField] int _maxHP = 100;
    [SerializeField] Transform[] _spawnPoints;
    public int CurrentHP { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = _maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHP < 0)
        {
            var r = Random.Range(0, _spawnPoints.Length);
            Instantiate(this.gameObject, _spawnPoints[r]);
            Destroy(this.gameObject);
        }
    }
}
