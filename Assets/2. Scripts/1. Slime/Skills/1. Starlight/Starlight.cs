using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Starlight : MonoBehaviour
{
    public GameObject starlightPrefab;
    public Image cooldownImage;
    private float cooldown = 7f;
    private Slime slime;

    private float projectileSpeed = 5f;
    private int projectileCount = 10;
    private float spawnInterval = 0.5f;

    private void Update()
    {
        slime = FindObjectOfType<Slime>();
    }

    public void StartSkill()
    {
        StartCoroutine(StarlightCoroutine());
    }

    private IEnumerator StarlightCoroutine()
    {
        while (true)
        {
            cooldownImage.fillAmount = 1f;

            for (int i = 0; i < projectileCount; i++)
            {
                Monster target = FindNearestMonster();
                if (target != null)
                {

                    GameObject starlight = Instantiate(starlightPrefab, starlightPrefab.transform.position, Quaternion.identity);
                    StartCoroutine(MoveProjectile(starlight, target));

                    Destroy(starlight, 2f);
                }

                yield return new WaitForSeconds(spawnInterval);
            }

            yield return StartCoroutine(Cooldown());

        }
    }


    private IEnumerator MoveProjectile(GameObject projectile, Monster target)
    {
        while (projectile != null && target != null)
        {
            Vector3 direction = (target.transform.position - projectile.transform.position).normalized;
            projectile.transform.position += direction * projectileSpeed * Time.deltaTime;

            yield return null;
        }


    }

    private IEnumerator Cooldown()
    {
        float elapsed = 0f;
        while (elapsed < cooldown)
        {
            elapsed += Time.deltaTime;
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = 1f - (elapsed / cooldown);
            }
            yield return null;
        }
    }


    private Monster FindNearestMonster()
    {
        Monster nearestMonster = null;
        float nearestDistance = float.MaxValue;

        foreach (Monster monster in FindObjectsOfType<Monster>())
        {
            float distance = Vector2.Distance(slime.transform.position, monster.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestMonster = monster;
            }
        }

        return nearestMonster;
    }

}


