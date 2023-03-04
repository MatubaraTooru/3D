using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent(typeof(HPManager)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(BoxCollider))]

public class EnemyController : MonoBehaviour
{
    [SerializeField] HPManager _hpmanager;
    [SerializeField] float _moveSpeed;
    [SerializeField] int _attackDamage;
    [SerializeField, TagField] string _searchTag;
    [SerializeField, TagField] string _attackTag;
    Animator _animator;
    [SerializeField] Vector3 _attackRangeCenter;
    [SerializeField] float _attackRangeRadius = 1f;
    [SerializeField] float _stopdistance;
    Rigidbody _rb;
    bool _attack = false;
    GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Search();
    }
    private void LateUpdate()
    {
        if (_animator)
        {
            _animator.SetBool("Attack", _attack);
            _animator.SetFloat("Move", _rb.velocity.magnitude);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetAttackRangeCenter(), _attackRangeRadius);
    }
    Vector3 GetAttackRangeCenter()
    {
        Vector3 center = this.transform.position + this.transform.forward * _attackRangeCenter.z
            + this.transform.up * _attackRangeCenter.y
            + this.transform.right * _attackRangeCenter.x;
        return center;
    }
    private void Search()
    {
        _target = GameObject.FindGameObjectWithTag(_searchTag);

        if (_target)
        {
            var dis = Vector3.Distance(_target.transform.position, transform.position);
            var dir = _target.transform.position - transform.position;
            dir.y = 0;
            transform.forward = dir;
            if (dis > _stopdistance)
            {
                _attack = false;
                _rb.velocity = dir.normalized * _moveSpeed;
            }
            else
            {
                _rb.velocity = Vector3.zero;
                _attack = true;
            }
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_attackTag))
        {
            _attack = true;
        }
    }
    private void Attack()
    {
        var cols = Physics.OverlapSphere(GetAttackRangeCenter(), _attackRangeRadius);

        foreach (var c in cols)
        {
            if (c.CompareTag(_attackTag))
            {
                c.GetComponent<HPManager>().CurrentHP -= _attackDamage;
                Debug.Log(c.GetComponent<HPManager>().CurrentHP);
            }
        }
    }
}
