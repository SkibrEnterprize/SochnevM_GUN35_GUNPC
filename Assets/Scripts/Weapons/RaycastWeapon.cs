using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[RequireComponent(typeof(AudioPlayShoot))]
public partial class RaycastWeapon : MonoBehaviour
{
    [SerializeField] private string _weaponName;
    [SerializeField] private bool _debug = false;
    [SerializeField] private ActiveWeapon.WeaponSlot _weaponSlot;
    [SerializeField] private MeshSockets.SocketId _holsterSocket;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private bool _isFiring = false;
    [SerializeField] private bool _isCloseWeapon;
    [SerializeField] private int _fireRate = 25;
    [SerializeField] private float _bulletSpeed = 1000.0f;
    [SerializeField] private float _bulletDrop = 0.0f;
    [SerializeField] private int _maxBounces = 0;
    [SerializeField] private int _ammoCount = 30;
    [SerializeField] private int _clipSize = 30;
    [SerializeField] private int _clipCount = 2;
    [SerializeField] private float _damage = 10;
    [SerializeField] private RuntimeAnimatorController _animator;
    [SerializeField] private ParticleSystem[] _muzzleFlash;
    [SerializeField] private ParticleSystem _hitEffectBlood;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private TrailRenderer _tracerEffect;
    [SerializeField] private Transform _raycastOrigin;
    [SerializeField] private WeaponRecoil _recoil;
    [SerializeField] private GameObject _magazine;

    public string WeaponName => _weaponName;
    public bool IsCloseWeapon => _isCloseWeapon;
    public ActiveWeapon.WeaponSlot WeaponSlot => _weaponSlot;
    public MeshSockets.SocketId HolsterSocket => _holsterSocket;
    public LayerMask LayerMask => _layerMask;
    public bool IsFiring => _isFiring;
    public bool Debug => _debug;
    public int FireRate => _fireRate;
    public float BulletSpeed => _bulletSpeed;
    public float BulletDrop => _bulletDrop;
    public int MaxBounces => _maxBounces;
    public int AmmoCount => _ammoCount;
    public int ClipSize => _clipSize;
    public int ClipCount => _clipCount;
    public float Damage => _damage;
    public RuntimeAnimatorController Animator => _animator;
    public ParticleSystem[] MuzzleFlash => _muzzleFlash;
    public ParticleSystem HitEffect => _hitEffect;
    public TrailRenderer TracerEffect => _tracerEffect;
    public Transform RaycastOrigin => _raycastOrigin;
    public WeaponRecoil Recoil => _recoil;
    public GameObject Magazine => _magazine;

    private Ray _ray;
    private RaycastHit _hitInfo;
    private float _accumulatedTime;
    private List<Bullet> _bullets = new List<Bullet>();
    private float _maxLifetime = 3.0f;
    private AudioPlayShoot _audioPlayShoot;

    private void Awake()
    {
        _recoil = GetComponent<WeaponRecoil>();
        _audioPlayShoot = GetComponent<AudioPlayShoot>();

    }

    Vector3 GetPosition(Bullet bullet)
    {
        // p + v*t + 0.5*g*t*t
        Vector3 gravity = Vector3.down * BulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(TracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        bullet.bounce = MaxBounces;

        return bullet;
    }

    public void StartFiring()
    {
        _isFiring = true;
        if (_accumulatedTime > 0.0f)
        {
            _accumulatedTime = 0.0f;
        }
        Recoil.Reset();
    }

    public void UpdateWeapon(float deltaTime, Vector3 target)
    {
        if (IsFiring)
        {
            UpdateFiring(deltaTime, target);
        }

        // Need to keep track of cooldown even when not firing to prevent click spam.
        _accumulatedTime += deltaTime;

        UpdateBullets(deltaTime);
    }

    public void UpdateFiring(float deltaTime, Vector3 target)
    {
        float fireInterval = 1.0f / FireRate;
        while (_accumulatedTime >= 0.0f)
        {
            FireBullet(target);
            _accumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    void SimulateBullets(float deltaTime)
    {
        _bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }

    void DestroyBullets()
    {
        _bullets.RemoveAll(bullet => bullet.time >= _maxLifetime);
    }

    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        _ray.origin = start;
        _ray.direction = direction;

        Color debugColor = Color.green;


        if (Physics.Raycast(_ray, out _hitInfo, distance, LayerMask))
        {
            if (_hitInfo.collider.TryGetComponent(out Ragdoll ragdoll))
            {
                PlayHitEffect(_hitEffectBlood);
            }
            else if (_hitInfo.collider.TryGetComponent(out DestroingObject destroingObject))
            {
                destroingObject.Exploded();
            }
            else
            {
                PlayHitEffect(_hitEffect);
            }

            bullet.time = _maxLifetime;
            end = _hitInfo.point;
            debugColor = Color.red;

            // Bullet ricochet
            if (bullet.bounce > 0)
            {
                bullet.time = 0;
                bullet.initialPosition = _hitInfo.point;
                bullet.initialVelocity = Vector3.Reflect(bullet.initialVelocity, _hitInfo.normal);
                bullet.bounce--;
            }

            var rb2d = _hitInfo.collider.GetComponent<Rigidbody>();
            if (rb2d)
            {
                rb2d.AddForceAtPosition(_ray.direction * 20, _hitInfo.point, ForceMode.Impulse);
            }

            var hitBox = _hitInfo.collider.GetComponent<HitBox>();
            if (hitBox)
            {
                hitBox.OnRaycastHit(this, _ray.direction);
            }
        }

        if (bullet.tracer)
        {
            bullet.tracer.transform.position = end;
        }

        if (Debug)
        {
            UnityEngine.Debug.DrawLine(start, end, debugColor, 1.0f);
        }
    }

    private void FireBullet(Vector3 target)
    {
        if (IsCloseWeapon)
        {
            return;
        }
        else
        {
            if (AmmoCount <= 0)
            {
                return;
            }
            _ammoCount--;

            foreach (var particle in MuzzleFlash)
            {
                particle.Emit(1);
            }

            Vector3 velocity = (target - RaycastOrigin.position).normalized * BulletSpeed;
            var bullet = CreateBullet(RaycastOrigin.position, velocity);
            _bullets.Add(bullet);
            _audioPlayShoot.PlayShootSound();
            Recoil.GenerateRecoil(_weaponName);

        }
    }

    public void PlayHitEffect(ParticleSystem effect)
    {
        effect.transform.position = _hitInfo.point;
        effect.transform.forward = _hitInfo.normal;
        effect.Emit(1);
    }

    public void StopFiring()
    {
        _isFiring = false;
    }

    public bool ShouldReload()
    {
        return AmmoCount == 0 && ClipCount > 0;
    }

    public bool IsLowAmmo()
    {
        return AmmoCount == 0 && ClipCount == 0;
    }

    public void RefillAmmo()
    {
        _ammoCount = ClipSize;
        _clipCount--;
    }

    public void ClipAdd(int clipCount)
    {
        _clipCount += clipCount;
    }
}
