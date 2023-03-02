using System.Collections;
using UnityEngine;

public class ShotgunController : WeaponControllerBase
{
    float _rateTimer = 0;
    int _currentAmmo = 0;
    Transform _hitTarget;
    bool _active = false;
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

        if (Input.GetButtonDown("Fire1") && _rateTimer > _firerate && _currentAmmo > 0)
        {
            Fire();
        }
        else if (Input.GetButtonDown("Fire1") && _currentAmmo <= 0)
        {

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    protected override void Fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);

        if (Physics.Raycast(ray, out RaycastHit hit, _range, _hitLayer))
        {
            _hitTarget = hit.transform;
        }
        if (_hitTarget && _currentAmmo > 0)
        {
            var dis = Vector3.Distance(gameObject.transform.position, _hitTarget.transform.position);
            var enemyHP = _hitTarget.GetComponent<HPManager>();

            if (dis < 10)
            {
                enemyHP.CurrentHP -= 65;
            }
            else if (dis < 20)
            {
                enemyHP.CurrentHP -= 45;
            }
            else
            {
                enemyHP.CurrentHP -= 20;
            }
            Debug.Log(enemyHP);
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
        _active = true;
        StartCoroutine(ReloadRoutine());
    }
    IEnumerator ReloadRoutine()
    {
        while (_currentAmmo < _maxAmmo)
        {
            _currentAmmo++;
            _ammoText.text = _currentAmmo.ToString();

            yield return new WaitForSeconds(_reloadSpeed);
        }

        _active = false;
    }
}
