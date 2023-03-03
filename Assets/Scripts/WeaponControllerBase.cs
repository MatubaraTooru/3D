using UnityEngine;
using UnityEngine.UI;

public abstract class WeaponControllerBase : MonoBehaviour
{
    [SerializeField] protected int _maxdamage;
    [SerializeField] protected int _maxAmmo;
    [SerializeField] protected float _firerate;
    [SerializeField] protected float _reloadSpeed;
    [SerializeField] protected Image _crosshair;
    [SerializeField] protected Text _ammoText;
    [SerializeField] protected float _range;
    [SerializeField] protected LayerMask _hitLayer;
    [SerializeField] protected AudioClip _fireSound;
    [SerializeField] protected AudioClip _reloadSound;
    [SerializeField] protected AudioClip _emptySound;
    [SerializeField] protected AudioSource _audioSource;
    protected abstract void Fire();
    protected abstract void Reload();
}
