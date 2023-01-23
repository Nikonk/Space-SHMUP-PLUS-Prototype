using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private BoundsCheck     bndCheck;
    private Renderer        rend;

    [Header("Set Dynamically")]
    public Rigidbody        rigid;
    [SerializeField]
    private WeaponType  _type;

    public WeaponType type {
        get {
            return (_type);
        }
        set {
            SetType(value);
        }
    }

    private void Awake() {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (bndCheck.offUp) {
            Destroy(gameObject);
        }
    }

    /// <summary>
    ///     Изменяет скрытое поле _type и устанавливает цвет этого снаряда,
    ///     как определно в WeaponDefinition.
    /// </summary>
    /// <param name="eType"> Тип WeaponType исользуемого оружия. </param>
    public void SetType (WeaponType eType) {
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        rend.material.color = def.projectileColor;
    }
}
