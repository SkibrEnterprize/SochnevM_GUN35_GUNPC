using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeapons : MonoBehaviour
{
    public enum WeaponState {
        Holstering,
        Holstered,
        Activating,
        Active,
        Reloading
    }

    public enum WeaponSlot {
        Primary,
        Secondary
    }

    public RaycastWeapon currentWeapon {
        get {
            return weapons[current];
        }
    }

    public WeaponSlot currentWeaponSlot {
        get {
            return (WeaponSlot)current;
        }
    }
    RaycastWeapon[] weapons = new RaycastWeapon[2];
    int current = 0;
    public Animator animator;
    MeshSockets sockets;
    WeaponIk weaponIk;
    Transform currentTarget;
    WeaponState weaponState = WeaponState.Holstered;
    public float inaccuracy = 0.0f;
    public float dropForce = 1.5f;
    GameObject magazineHand;
    private bool isCloseAttack;

    public bool IsActive() {
        return weaponState == WeaponState.Active;        
    }

    public bool IsHolstered() {
        return weaponState == WeaponState.Holstered;
    }

    public bool IsReloading() {
        return weaponState == WeaponState.Reloading;
    }

    private void Awake() {
        animator = GetComponent<Animator>();
        sockets = GetComponent<MeshSockets>();
        weaponIk = GetComponent<WeaponIk>();
    }

    private void Update() {
        if (currentTarget && currentWeapon && IsActive()) {
            Vector3 target = currentTarget.position + weaponIk.targetOffset;
            target += Random.insideUnitSphere * inaccuracy;
            currentWeapon.UpdateWeapon(Time.deltaTime, target);
        }
    }

    public void SetFiring(bool enabled) {
        if (enabled) {
            if(currentWeapon.IsCloseWeapon) CloseAttack();
            currentWeapon.StartFiring();
        } else {
            currentWeapon.StopFiring();
        }
    }

    public void DropWeapon() {
        if (currentWeapon) {
            currentWeapon.transform.SetParent(null);
            currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.gameObject.AddComponent<Rigidbody>();
            weapons[current] = null;
        }
    }

    public bool HasWeapon() {
        return currentWeapon != null;
    }

    public void SetTarget(Transform target) {
        weaponIk.SetTargetTransform(target);
        currentTarget = target;
    }

    public void Equip(RaycastWeapon weapon) {
        current = (int)weapon.WeaponSlot;
        weapons[current] = weapon;
        sockets.Attach(weapon.transform, weapon.HolsterSocket);
    }

    public void ActivateWeapon() {
        StartCoroutine(EquipWeaponAnimation());
    }

    public void DeactivateWeapon() {
        SetTarget(null);
        SetFiring(false);
        StartCoroutine(HolsterWeaponAnimation());
    }

    public void ReloadWeapon() {
        if (IsActive()) {
            StartCoroutine(ReloadWeaponAnimation());
        }
    }

    public void SwitchWeapon(WeaponSlot slot) {
        if (weapons[(int)slot] == null) {
            return;
        }

        if (IsHolstered()) {
            current = (int)slot;
            ActivateWeapon();
            return;
        }

        int equipIndex = (int)slot;
        if (IsActive() && current != equipIndex) {
            StartCoroutine(SwitchWeaponAnimation(equipIndex));
        }
    }

    public int Count() {
        int count = 0;
        foreach (var weapon in weapons) {
            if (weapon != null) {
                count++;
            }
        }
        return count;
    }

    IEnumerator EquipWeaponAnimation() {
        weaponState = WeaponState.Activating;
        animator.runtimeAnimatorController = currentWeapon.Animator;
        animator.SetBool("equip", true);
        yield return new WaitForSeconds(0.5f);
        while(animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f) {
            yield return null;
        }

        weaponIk.enabled = true;
        weaponIk.SetAimTransform(currentWeapon.RaycastOrigin);
        weaponState = WeaponState.Active;
    }

    IEnumerator HolsterWeaponAnimation() {
        weaponState = WeaponState.Holstering;
        animator.SetBool("equip", false);
        weaponIk.enabled = false;
        yield return new WaitForSeconds(0.5f);
        while (animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f) {
            yield return null;
        }

        weaponState = WeaponState.Holstered;
    }

    IEnumerator ReloadWeaponAnimation() {
        weaponState = WeaponState.Reloading;
        animator.SetTrigger("reload_weapon");
        weaponIk.enabled = false;
        yield return new WaitForSeconds(0.5f);
        while (animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f) {
            yield return null;
        }

        weaponIk.enabled = true;
        weaponState = WeaponState.Active;
    }

    IEnumerator SwitchWeaponAnimation(int index) {
        yield return StartCoroutine(HolsterWeaponAnimation());
        current = index;
        yield return StartCoroutine(EquipWeaponAnimation());
    }

    private void PlayCloseAttack()
    {
        isCloseAttack = true;

        // ѕр€мой вызов клипа (можно через Layer/LayerWeight)
        animator.SetTrigger("closeAttack");
        //_animator.Play(_attackState, 0, 0f);   // 0 Ц слой, 0f Ц начало

        //// ∆дЄм пока анимаци€ закончитс€
        //yield return new WaitUntil(() =>
        //    !animator.IsInTransition(0) &&
        //    animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        isCloseAttack = false;
    }

    public void OnAnimationEvent(string eventName) {
        switch (eventName) {
            case "attach_weapon":
                AttachWeapon();
                break;
            case "detach_magazine":
                DetachMagazine();
                break;
            case "drop_magazine":
                DropMagazine();
                break;
            case "refill_magazine":
                RefillMagazine();
                break;
            case "attach_magazine":
                AttachMagazine();
                break;
        }
    }

    void CloseAttack()
    {
        if (isCloseAttack) return;
        PlayCloseAttack();
        animator.SetTrigger("closeAttack");
    }

    void AttachWeapon() {
        bool equipping = animator.GetBool("equip");
        if (equipping) {
            sockets.Attach(currentWeapon.transform, MeshSockets.SocketId.RightHand);
        } else {
            sockets.Attach(currentWeapon.transform, currentWeapon.HolsterSocket);
        }
    }

    void DetachMagazine() {
        var leftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
        RaycastWeapon weapon = currentWeapon;
        magazineHand = Instantiate(weapon.Magazine, leftHand, true);
        weapon.Magazine.SetActive(false);
    }

    void DropMagazine() {
        GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.SetActive(true);
        Rigidbody body = droppedMagazine.AddComponent<Rigidbody>();

        Vector3 dropDirection = -gameObject.transform.right;
        dropDirection += Vector3.down;

        body.AddForce(dropDirection * dropForce, ForceMode.Impulse);
        droppedMagazine.AddComponent<BoxCollider>();
        magazineHand.SetActive(false);
    }

    void RefillMagazine() {
        magazineHand.SetActive(true);
    }

    void AttachMagazine() {
        RaycastWeapon weapon = currentWeapon;
        weapon.Magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.RefillAmmo();
        animator.ResetTrigger("reload_weapon");
    }

    public void RefillAmmo(int clipCount) {
        var weapon = currentWeapon;
        if (weapon) {
            weapon.ClipAdd(clipCount);
        }
    }

    public bool IsLowAmmo() {
        var weapon = currentWeapon;
        if (weapon) {
            return weapon.IsLowAmmo();
        }
        return false;
    }
}
