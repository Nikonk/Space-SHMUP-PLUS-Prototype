using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    [Header("Set in Inspector")]
    public float        rotationsPerSecond = .1f;

    [Header("Set Dynamically")]
    public int          levelShown = 0;

    Material            mat;

    private void Start() {
        mat = GetComponent<Renderer>().material;
    }

    private void Update() {
        int currLevel = Mathf.FloorToInt(Hero.S.shieldLevel);
        if (levelShown != currLevel) {
            levelShown = currLevel;
            mat.mainTextureOffset = new Vector2(.2f * levelShown, 0);
        }
        float rZ = -(rotationsPerSecond * Time.time * 360);
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
