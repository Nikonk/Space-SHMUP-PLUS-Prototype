using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : MonoBehaviour {
    [Header("Set in Inspector: Enemy_0")]
    public float        speed = 10f;
    public float        fireRate = .3f;
    public float        health = 10f;
    public int          score = 100;
    public float        showDamageDuration = .1f;
    public float        powerUpDropChance = 1f;

    [Header("Set Dynamically: Enemy_0")]
    public Color[]      originalColors;
    public Material[]   materials;
    public bool         showingDamage = false;
    public float        damageDoneTime;
    public bool         notifiedOfDestruction = false;

    protected BoundsCheck bndCheck;

    private void Awake() {
        bndCheck = GetComponent<BoundsCheck>();
        materials = Utils.GetAllMaterial(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++) {
            originalColors[i] = materials[i].color;
        }
    }

    public Vector3 pos {
        get {
            return this.transform.position;
        }
        set {
            this.transform.position = value;
        }
    }

    private void Update() {
        Move();

        if (showingDamage && Time.time > damageDoneTime) {
            UnShowDamage();
        }

        if (bndCheck != null && bndCheck.offDown) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        GameObject otherGO = other.gameObject;
        switch (otherGO.tag) {
            case "ProjectileHero":
                Projectile p = otherGO.GetComponent<Projectile>();
                if (!bndCheck.isOnScreen) {
                    Destroy(otherGO);
                    break;
                }

                ShowDamage();

                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if (health <= 0) {
                    if (!notifiedOfDestruction) {
                        Main.S.ShipDestroyed(this);
                    }
                    notifiedOfDestruction = true;
                    Destroy(this.gameObject);
                }
                Destroy(otherGO);
                break;
            default:
                print("Enemy hit by non-projectileHero: " + otherGO.name);
                break;
        }
    }

    private void ShowDamage() {
        foreach (Material m in materials) {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }

    private void UnShowDamage() {
        for (int i = 0; i < materials.Length; i++) {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }

    public virtual void Move() {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
}
