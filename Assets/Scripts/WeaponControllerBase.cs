using UnityEngine;
using UnityEngine.UI;

public abstract class WeaponControllerBase : MonoBehaviour
{
    [SerializeField] protected int _maxAmmo;
    [SerializeField] protected float _firerate;
    [SerializeField] protected float _reloadSpeed;
    [SerializeField] protected Image _crosshair;
    [SerializeField] protected Text _ammoText;
    [SerializeField] protected float _range;
    [SerializeField] protected LayerMask _hitLayer;
    protected abstract void Fire();
    protected abstract void Reload();
}
