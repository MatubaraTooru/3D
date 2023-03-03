using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShotgunController : WeaponControllerBase
{
    float _rateTimer = 0;
    int _currentAmmo = 0;
    Transform _hitTarget;
    Coroutine _coroutin = null;
    // Start is called before the first frame update
    void Start()
    {
        _rateTimer = _firerate;
        _currentAmmo = _maxAmmo;
        _ammoText.text = _currentAmmo.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        _rateTimer += Time.deltaTime;

        if (_coroutin != null && Input.GetButtonDown("Fire1"))
        {
            StopCoroutine(_coroutin);
        }
        if (Input.GetButtonDown("Fire1") && _rateTimer > _firerate && _currentAmmo > 0)
        {
            Fire();
        }
        else if (Input.GetButtonDown("Fire1") && _currentAmmo <= 0)
        {
            _audioSource.clip = _emptySound;
            _audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    protected override void Fire()
    {
        _audioSource.clip = _fireSound;
        _audioSource.volume = 1;
        _audioSource.Play();
        Ray ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);

        if (Physics.Raycast(ray, out RaycastHit hit, _range, _hitLayer))
        {
            _hitTarget = hit.transform;
        }
        if (_hitTarget && _currentAmmo > 0)
        {
            var dis = Vector3.Distance(gameObject.transform.position, _hitTarget.transform.position);
            var enemyHP = _hitTarget.GetComponent<HPManager>();
            var d = dis / _range * 100;
            enemyHP.CurrentHP -= _maxdamage - (int)d;
        }
        if (_currentAmmo < 1)
        {
            _currentAmmo = 0;
        }
        else
        {
            _currentAmmo--;
        }

        _ammoText.text = _currentAmmo.ToString();
        _rateTimer = 0;
    }
    protected override void Reload()
    {
        _audioSource.clip = _reloadSound;
        _coroutin = StartCoroutine(ReloadRoutine());
    }
    IEnumerator ReloadRoutine()
    {
        while (_currentAmmo < _maxAmmo)
        {
            _audioSource.Play();
            _audioSource.volume = 0.2f;
            _currentAmmo++;
            _ammoText.text = _currentAmmo.ToString();

            yield return new WaitForSeconds(_reloadSpeed);
        }
    }
}
