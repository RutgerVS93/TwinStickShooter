using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePatterns : MonoBehaviour {

    public static FirePatterns script;

    public enum Pattern
    {
        Spread,
        SineWave,
        Shotgun,
        MachineGun,
        Homing
    }

    public Pattern pattern;
    private string[] currentPattern = System.Enum.GetNames(typeof(Pattern));

    private float fireRate, numberOfBullets;

    [SerializeField]
    private GameObject firePoint, player;

    [SerializeField]
    private GameObject[] bullet; //TO DO add multiple different bullets for every fire mode
    private int bulletVal;       //Assign different bullets a value corresponding to the firemode that fires them

	void Start ()
    {
        script = this;	
	}
	
	void Update ()
    {
        UpdateFireMode();
        SwitchWeapon();
	}

    void UpdateFireMode()
    {
        switch (pattern)
        {
            case Pattern.Spread:
                bulletVal = 0;
                numberOfBullets = 3;
                fireRate = .2f;
                break;
            case Pattern.SineWave:
                bulletVal = 1;
                numberOfBullets = 4;
                fireRate = .2f;
                break;
            case Pattern.Shotgun:
                bulletVal = 2;
                numberOfBullets = 4;
                fireRate = .3f;
                break;
            case Pattern.MachineGun:
                bulletVal = 3;
                fireRate = .1f;
                break;
        }
    }

    void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("YButton"))
        {
            if (pattern == Pattern.MachineGun)
            {
                pattern = Pattern.Spread;
            }
            else
            {
                pattern++;
            }
        }
    }

    public IEnumerator SpreadShot()
    {
        float bulletRotation = Mathf.Atan2(-Input.GetAxis("RightHorizontal"), -Input.GetAxis("RightVertical")) * Mathf.Rad2Deg - 30;
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject obj = Instantiate(bullet[bulletVal], firePoint.transform.position, Quaternion.Euler(firePoint.transform.rotation.x, firePoint.transform.rotation.y, firePoint.transform.localRotation.z + bulletRotation));
            bulletRotation += 30;
        }
        yield return new WaitForSeconds(fireRate);
        PlayerController.canFire = true;
    }

    public IEnumerator SineWaveShot()
    {
        float bulletRotation = Mathf.Atan2(-Input.GetAxis("RightHorizontal"), -Input.GetAxis("RightVertical")) * Mathf.Rad2Deg;
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject obj = Instantiate(bullet[bulletVal], firePoint.transform.position, Quaternion.Euler(firePoint.transform.rotation.x, firePoint.transform.rotation.y, firePoint.transform.rotation.z + bulletRotation));
            bulletRotation += 90;
        }
        yield return new WaitForSeconds(fireRate);
        PlayerController.canFire = true;
    }

    public IEnumerator Shotgun()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            float bulletRotation = Mathf.Atan2(-Input.GetAxis("RightHorizontal"), -Input.GetAxis("RightVertical")) * Mathf.Rad2Deg + Random.Range(-15f, 15f);
            GameObject obj = Instantiate(bullet[bulletVal], firePoint.transform.position, Quaternion.Euler(firePoint.transform.rotation.x, firePoint.transform.rotation.y, firePoint.transform.rotation.z + bulletRotation));
        }
        yield return new WaitForSeconds(fireRate);
        PlayerController.canFire = true;
    }

    public IEnumerator MachineGun()
    {
        float bulletRotation = Mathf.Atan2(-Input.GetAxis("RightHorizontal"), -Input.GetAxis("RightVertical")) * Mathf.Rad2Deg + Random.Range(-5f, 5f);
        GameObject obj = Instantiate(bullet[bulletVal], firePoint.transform.position, Quaternion.Euler(firePoint.transform.rotation.x, firePoint.transform.rotation.y, firePoint.transform.rotation.z + bulletRotation));
        yield return new WaitForSeconds(fireRate);
        PlayerController.canFire = true;
    }

    public IEnumerator Homing()
    {
        float bulletRotation = Mathf.Atan2(-Input.GetAxis("RightHorizontal"), -Input.GetAxis("RightVertical")) * Mathf.Rad2Deg;
        GameObject obj = Instantiate(bullet[4], firePoint.transform.position, Quaternion.Euler(firePoint.transform.rotation.x, firePoint.transform.rotation.y, firePoint.transform.rotation.z + bulletRotation));
        obj.GetComponent<Rigidbody2D>().AddForce(player.transform.up * 50f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f / PlayerHealth.script.upgradeLevel);
        PlayerController.canFireHoming = true;
    }
}
