using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy Type")]
public class EnemyTypeSO : ScriptableObject
{
    public Sprite sprite;
    public int health;
    public int speed;
}