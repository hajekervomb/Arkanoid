using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RecalculatePath : MonoBehaviour
{
    // Временная реализация для теста
    // TODO - добавить в класс Enemy или EnemyAI, вызывать ивентом
    void Update()
    {
        AstarPath.active.Scan();
        Debug.Log("scanning...");
    }
}
