using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunController : MonoBehaviour
{
    [SerializeField] Image _crosshair;
    BoxCollider _boxcol;
    GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        _boxcol = GetComponent<BoxCollider>();
        _crosshair.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_target)
            {
                var dis = Vector3.Distance(gameObject.transform.position, _target.transform.position);
                var enemymaterial = _target.GetComponent<MeshRenderer>().material;
                var enemyHP = _target.GetComponent<HPManager>();

                if (dis < 10)
                {
                    enemymaterial.color = Color.red;
                    enemyHP.CurrentHP -= 65;
                }
                else if (dis < 20)
                {
                    enemymaterial.color = Color.green;
                    enemyHP.CurrentHP -= 45;
                }
                else
                {
                    enemymaterial.color = Color.blue;
                    enemyHP.CurrentHP -= 20;
                }
                Debug.Log(enemyHP);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _crosshair.color = Color.red;
            _target = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_target)
        {
            _crosshair.color = Color.black;
            _target = null;
        }
    }
}
