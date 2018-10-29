﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCaster : MonoBehaviour {


    public List<Ability> abilities = new List<Ability>();
    public Vector3 castOffset;
    public int index = 0;
    private bool casting = false;
    private Ability currentAbility = null;
    private CharacterManager manager;
    private float castTime = 0;

    // Use this for initialization
    void Start() {
        manager = transform.GetComponent<CharacterManager>();
    }

    // Update is called once per frame
    void Update() {
        if (casting) {
            if (castTime > 1) {
                casting = false;
                castTime = 0;
            }
            castTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// starts the cast for the spell
    /// </summary>
    /// <returns></returns>
    public bool CastSpell() {
        if (casting)
            return false;

        currentAbility = abilities[index];
        manager.anim.CrossFade(currentAbility.animation, 0.2f);
        
        GameObject go = ObjectPool.pool.PullObject(currentAbility.gameObject);

        go.transform.position = transform.position + castOffset;
        go.transform.rotation = transform.rotation;
        casting = true;

        return true;
    }

    /// <summary>
    /// increment the index, looping back to 0
    /// </summary>
    public void IncrementIndex() {
        index++;
        index %= abilities.Count;
    }

    [System.Serializable]
    public class Ability {
        public string name;
        public string animation;
        public GameObject gameObject;
        public float length;
    }
}
