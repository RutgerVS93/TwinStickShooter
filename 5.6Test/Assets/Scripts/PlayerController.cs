using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private AudioClip[] clips;
    private AudioSource source;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float dashPower;

    [SerializeField]
    public Transform firePoint;

    [SerializeField]
    private GameObject bullet, special;

    [SerializeField]
    private GameObject playerObj;
    private Rigidbody2D rb;

    public static bool canFire = true;
    public static bool canFireHoming = true;
    private bool canDash = true;
    private bool canMove = true;
    private Quaternion currentRotation;

    float horizontal;
    float vertical;

    void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

	void Update ()
    {
        if (Input.GetKey(KeyCode.Space) && canFire || Input.GetAxis("RightHorizontal") != 0 && canFire || Input.GetAxis("RightVertical") != 0 && canFire)
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.Q) && PlayerHealth.script.currentSpecial >= PlayerHealth.script.totalSpecial || Input.GetButtonDown("RightBumper") && PlayerHealth.script.currentSpecial >= PlayerHealth.script.totalSpecial)
        {
            Special();
            PlayerHealth.script.currentSpecial = 0;
        }
        else
        {
            //TO DO:
            //Fail sound
            //Special Bar Anim
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Movement();
        if (canDash && Input.GetButtonDown("BButton"))
        {
            StartCoroutine(Dash());
        }
    }

    void Movement()
    {
        transform.position = playerObj.transform.position;

        if (Input.GetAxis("Horizontal") != 0 && canMove)
        {
            //transform.Translate(Vector2.right * speed * Time.deltaTime * horizontal);
            rb.AddForce(Vector2.right * speed * horizontal);
        }
        if (Input.GetAxis("Vertical") != 0 && canMove)
        {
            //transform.Translate(Vector2.up * speed * Time.deltaTime * vertical);
            rb.AddForce(Vector2.up * speed * vertical);
        }
    }

    void Fire()
    {
        canFire = false;
        if (FirePatterns.script.pattern == FirePatterns.Pattern.Spread)
        {
            FirePatterns.script.StartCoroutine(FirePatterns.script.SpreadShot());
        }
        else if (FirePatterns.script.pattern == FirePatterns.Pattern.SineWave)
        {
            FirePatterns.script.StartCoroutine(FirePatterns.script.SineWaveShot());
        }
        else if(FirePatterns.script.pattern == FirePatterns.Pattern.Shotgun)
        {
            FirePatterns.script.StartCoroutine(FirePatterns.script.Shotgun());
        }
        else if (FirePatterns.script.pattern == FirePatterns.Pattern.MachineGun)
        {
            FirePatterns.script.StartCoroutine(FirePatterns.script.MachineGun());
        }

        if (PlayerHealth.script.upgradeLevel >= 1 && canFireHoming)
        {
            FirePatterns.script.StartCoroutine(FirePatterns.script.Homing());
            canFireHoming = false;
        }
    }

    void Special()
    {
        GameObject specialObj = Instantiate(special, transform.position, transform.rotation);
        Destroy(specialObj, 5f);
    }

    IEnumerator Dash()
    {
        canMove = false;
        canDash = false;
        rb.AddForce(new Vector2(horizontal * dashPower, vertical * dashPower), ForceMode2D.Impulse);
        canMove = true;
        yield return new WaitForSeconds(.5f);
        canDash = true;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("PickUp"))
        {
            source.Stop();
            source.PlayOneShot(clips[0]);
        }       
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Wall"))
        {
            if (coll.gameObject.name == "Top")
            {
                rb.AddForce(new Vector2(0, -5), ForceMode2D.Impulse);
            }
            else if (coll.gameObject.name == "Bottom")
            {
                rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            }
            else if (coll.gameObject.name == "Right")
            {
                rb.AddForce(new Vector2(-5, 0), ForceMode2D.Impulse);
            }
            else if (coll.gameObject.name == "Left")
            {
                rb.AddForce(new Vector2(5, 0), ForceMode2D.Impulse);
            }
        }
    }
}
