using UnityEngine;
using UnityEngine.UI;

public abstract class WeaponControllerBase : MonoBehaviour
{
    [SerializeField] protected Transform _muzzle;
    [SerializeField] protected int _maxdamage;
    [SerializeField] protected int _maxAmmo;
    [SerializeField] protected int _magSize;
    [SerializeField] protected float _firerate;
    [SerializeField] protected float _reloadSpeed;
    [SerializeField] protected Image _crosshair;
    [SerializeField] protected Text _ammoText;
    [SerializeField] protected float _range;
    [SerializeField] protected LayerMask _hitLayer;
    [SerializeField] protected AudioClip _firingSound;
    [SerializeField] protected AudioClip _reloadSound;
    [SerializeField] protected AudioClip _emptySound;
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected GameObject _gunshot;
    [SerializeField] protected float _gunshotDestroyTime = 2f;
    float _rateTimer = 0;
    protected int _currentAmmo = 0;
    protected int _remainingAmmo = 0;
    Transform _hitTarget;
    protected Coroutine _coroutin = null;
    // Start is called before the first frame update
    void Start()
    {
        _rateTimer = _firerate;
        _remainingAmmo = _maxAmmo;
        _currentAmmo = _magSize;
        _ammoText.text = $"{_currentAmmo} / {_remainingAmmo}";
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
    protected void Fire()
    {
        var go = Instantiate(_gunshot, transform.position, transform.rotation);
        var audio = go.GetComponent<AudioSource>();
        audio.clip = _firingSound; audio.Play();
        Destroy(go, _gunshotDestroyTime);
        Ray ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward, Color.red);

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
        if (_currentAmmo <= 0)
        {
            _currentAmmo = 0;
        }
        else
        {
            _currentAmmo--;
        }

        _ammoText.text = $"{_currentAmmo} / {_remainingAmmo}";
        _rateTimer = 0;
    }
    protected abstract void Reload();
}
