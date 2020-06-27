using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy")]
public class Enemy_Base : ScriptableObject
{
    public string name;
    public int maxHealth;
    public Sprite art;
    public List<Effect_Base> actions = new List<Effect_Base>();
    public GameObject item;
}
