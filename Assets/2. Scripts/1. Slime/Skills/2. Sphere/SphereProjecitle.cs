using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereProjecitle : MonoBehaviour
{
    private float sphereSpeed = 0.6f;
    private float pierce = 10;

    Slime slime;

    private void Awake()
    {
        slime = FindObjectOfType<Slime>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * sphereSpeed * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Monster>(out Monster monster))
        {
            float damage = slime.damage * 1.2f;
            monster.TakeDamage(damage);
            pierce--;
            if (pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
