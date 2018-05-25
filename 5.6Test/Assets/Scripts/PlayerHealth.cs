using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public static PlayerHealth script;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject deadParticles;
    private bool particlesPlayed = false;

    [SerializeField]
    private Image[] bars;

    [SerializeField]
    private float totalHealth, currentHealth;
    public float totalSpecial, currentSpecial;
    public float totalUpgrade, currentUpgrade;
    public int upgradeLevel;

    public static bool hitDelayActive;

	void Start ()
    {
        script = this;
        currentHealth = totalHealth;
	}
	
	void Update ()
    {
        UpdateBar();
	}

    void UpdateBar()
    {
        bars[0].fillAmount = currentHealth/totalHealth;
        bars[1].fillAmount = currentSpecial / totalSpecial;
        bars[2].fillAmount = currentUpgrade / totalUpgrade;

        if (currentHealth <= 0 && !particlesPlayed)
        {
            Destroy(gameObject);
            GameObject obj = Instantiate(deadParticles, transform.position, transform.rotation);
            Destroy(obj, 3f);
            particlesPlayed = true;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy") || coll.gameObject.CompareTag("EnemyBullet"))
        {
            currentHealth -= 10; //Enemy Damage
            anim.SetTrigger("Hit");
            StartCoroutine(HitDelay());
        }
        if (coll.gameObject.CompareTag("Boss"))
        {

        }

        if (coll.gameObject.CompareTag("PickUp"))
        {
            switch(PickUpScript.script.effect)
            {
                case (PickUpScript.Effect.Health):
                    currentHealth += 5;
                    if (currentHealth >= totalHealth)
                    {
                        currentHealth = totalHealth;
                    }
                    break;
                case (PickUpScript.Effect.Special):
                    currentSpecial += 1;
                    if (currentSpecial >= totalSpecial)
                    {
                        currentSpecial = totalSpecial;
                    }
                    break;
                case (PickUpScript.Effect.Currency):
                    currentUpgrade += 1;
                    if (currentUpgrade >= totalUpgrade)
                    {
                        currentUpgrade = 0;
                        upgradeLevel += 1;
                    }
                    break;
            }
        }
    }

    IEnumerator HitDelay()
    {
        Time.timeScale = .1f;
        yield return new WaitForSeconds(.05f);
        Time.timeScale = 1;
    }
}
