using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RaycastWeapon))]
public class RaycastWeaponEditor : Editor
{
    private SerializedProperty _debug;
    private SerializedProperty _weaponName;
    private SerializedProperty _weaponSlot;
    private SerializedProperty _holsterSocket;
    private SerializedProperty _layerMask;
    private SerializedProperty _isFiring;
    private SerializedProperty _isCloseWeapon;
    private SerializedProperty _fireRate;
    private SerializedProperty _bulletSpeed;
    private SerializedProperty _bulletDrop;
    private SerializedProperty _maxBounces;
    private SerializedProperty _ammoCount;
    private SerializedProperty _clipSize;
    private SerializedProperty _clipCount;
    private SerializedProperty _damage;
    private SerializedProperty _animator;
    private SerializedProperty _muzzleFlash;
    private SerializedProperty _hitEffect;
    private SerializedProperty _hitEffectBlood;
    private SerializedProperty _tracerEffect;
    private SerializedProperty _raycastOrigin;
    private SerializedProperty _recoil;
    private SerializedProperty _magazine;

    private void OnEnable()
    {
        _debug = serializedObject.FindProperty("_debug");
        _weaponName = serializedObject.FindProperty("_weaponName");
        _weaponSlot = serializedObject.FindProperty("_weaponSlot");
        _holsterSocket = serializedObject.FindProperty("_holsterSocket");
        _layerMask = serializedObject.FindProperty("_layerMask");
        _isFiring = serializedObject.FindProperty("_isFiring");
        _isCloseWeapon = serializedObject.FindProperty("_isCloseWeapon");
        _fireRate = serializedObject.FindProperty("_fireRate");
        _bulletSpeed = serializedObject.FindProperty("_bulletSpeed");
        _bulletDrop = serializedObject.FindProperty("_bulletDrop");
        _maxBounces = serializedObject.FindProperty("_maxBounces");
        _ammoCount = serializedObject.FindProperty("_ammoCount");
        _clipSize = serializedObject.FindProperty("_clipSize");
        _clipCount = serializedObject.FindProperty("_clipCount");
        _damage = serializedObject.FindProperty("_damage");
        _animator = serializedObject.FindProperty("_animator");
        _raycastOrigin = serializedObject.FindProperty("_raycastOrigin");
        _muzzleFlash = serializedObject.FindProperty("_muzzleFlash");
        _hitEffect = serializedObject.FindProperty("_hitEffect");
        _hitEffectBlood = serializedObject.FindProperty("_hitEffectBlood");
        _tracerEffect = serializedObject.FindProperty("_tracerEffect");
        _magazine = serializedObject.FindProperty("_magazine");
        _recoil = serializedObject.FindProperty("_recoil");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_debug);
        EditorGUILayout.PropertyField(_weaponName);
        EditorGUILayout.PropertyField(_weaponSlot);
        EditorGUILayout.PropertyField(_holsterSocket);
        EditorGUILayout.PropertyField(_layerMask);
        EditorGUILayout.PropertyField(_damage);
        EditorGUILayout.PropertyField(_animator);
        EditorGUILayout.PropertyField(_hitEffectBlood);

        EditorGUILayout.PropertyField(_isCloseWeapon, new GUIContent("Is Close Weapon"));

        if (!_isCloseWeapon.boolValue)
        {
            EditorGUILayout.PropertyField(_isFiring);
            EditorGUILayout.PropertyField(_fireRate);
            EditorGUILayout.PropertyField(_bulletSpeed);
            EditorGUILayout.PropertyField(_bulletDrop);
            EditorGUILayout.PropertyField(_maxBounces);
            EditorGUILayout.PropertyField(_ammoCount);
            EditorGUILayout.PropertyField(_clipSize);
            EditorGUILayout.PropertyField(_clipCount);
            EditorGUILayout.PropertyField(_muzzleFlash);
            EditorGUILayout.PropertyField(_hitEffect);
            EditorGUILayout.PropertyField(_raycastOrigin);
            EditorGUILayout.PropertyField(_tracerEffect);
            EditorGUILayout.PropertyField(_recoil);
            EditorGUILayout.PropertyField(_magazine);
        }
        serializedObject.ApplyModifiedProperties();
    }
}

