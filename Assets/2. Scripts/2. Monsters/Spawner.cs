using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private int monsterCount = 10; // 한 번에 스폰될 적의 수

    public GameObject monsterPrefab; // 적 프리팹
    public float spawnInterval = 3f; 
    private float baseHp = 1f; 
    private float baseDamage = 1f;
    private float plusHp = 1f;
    private float plusDamage = 2f;
    private int waveCount = 0;  

    private float spawnStartX = 2.8f; // 첫 번째 몬스터 스폰 위치
    public float monsterSpacing = 0.5f; // 몬스터 간 간격

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

            if (monsters.Length == 0)
            {
                waveCount++;
                Spawn(monsterCount); // 현재 몬스터 수만큼 스폰
            }

            yield return new WaitForSeconds(spawnInterval); // 스폰 간격
        }
    }

    private void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float y = Random.Range(0.45f, 0.5f);
            float x = spawnStartX + (i * monsterSpacing);

            Vector2 spawnPos = new Vector2(x, y);
            GameObject monsterObject = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

            Monster monster = monsterObject.GetComponent<Monster>();
            if (monster != null)
            {
                float currentHp = baseHp;
                float currentDamage = baseDamage;

                if (waveCount > 1)
                {
                    currentHp = baseHp + (plusHp * (waveCount - 1));
                    currentDamage = baseDamage + (plusDamage * (waveCount - 1));
                }

                monster.SetHp(currentHp);
                monster.SetDamage(currentDamage);
                float hp = baseHp * (1 + waveCount * 0.1f); // 웨이브마다 체력 증가
                float damage = baseDamage * (1 + waveCount * 1f); // 웨이브마다 데미지 증가
                monster.SetHp(hp);
                monster.SetDamage(damage);
            }
        }
    }

    public void ResetMonsterStats()
    {
        waveCount = 0;
    }
}
