using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    //public List<Enemy> enemies = new List<Enemy>(); 리스트 + 딕셔너리 혹은 이넘 혹은 SO로 관리 예정 
    public Enemy testEnemy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
