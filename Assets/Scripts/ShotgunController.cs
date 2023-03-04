using System.Collections;
using UnityEngine;

public class ShotgunController : WeaponControllerBase
{
    [SerializeField] AudioClip[] _clips;
    protected override void Reload()
    {
        _audioSource.clip = _reloadSound;
        _coroutin = StartCoroutine(ReloadRoutine());
    }
    IEnumerator ReloadRoutine()
    {
        while (_currentAmmo < _magSize)
        {
            _audioSource.Play();
            _audioSource.volume = 0.2f;
            _remainingAmmo--;
            _currentAmmo++;
            _ammoText.text = $"{_currentAmmo} / {_remainingAmmo}";

            yield return new WaitForSeconds(_reloadSpeed);
        }

        _audioSource.clip = _clips[0];
        _audioSource.Play();
    }
}
