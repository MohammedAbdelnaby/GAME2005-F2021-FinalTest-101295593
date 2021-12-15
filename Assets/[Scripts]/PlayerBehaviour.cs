using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;


    public BulletManager bulletManager;

    [Header("Movement")]
    public float speed;
    public bool isGrounded;


    public RigidBody3D body;
    public CubeBehaviour cube;
    public Camera playerCam;
    public CubeBehaviour GunBarral;

    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _Fire();
        _Move();
        _Push();
    }

    private void _Move()
    {
        if (isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                // move right
                body.velocity = playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                // move left
                body.velocity = -playerCam.transform.right * speed * Time.deltaTime;
            }
            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                // move forward
                body.velocity = playerCam.transform.forward * speed * Time.deltaTime;
            }
            if (Input.GetAxisRaw("Vertical") < 0.0f) 
            {
                // move Back
                body.velocity = -playerCam.transform.forward * speed * Time.deltaTime;
            }

            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.9f);
            body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z); // remove y

            if (Input.GetAxisRaw("Jump") > 0.0f)
            {
                body.velocity = (transform.up * speed * 0.1f * Time.deltaTime);
            }

            if (Input.GetKey("f"))
            {
                body.velocity = ((playerCam.transform.forward / 15) - transform.up * speed * 0.1f * Time.deltaTime) / 3;
            }


            transform.position += body.velocity;
        }
    }

    void _Push()
    {

        if (Input.GetKey("e"))
        {
            if (GunBarral.isColliding)
            {
                for (int i = 0; i < GunBarral.contacts.Count; i++)
                {
                    CubeBehaviour contacts = GunBarral.contacts[i].cube;
                    if (!(contacts.gameObject.name == "Player"))
                    {
                        if (contacts.GetComponent<RigidBody3D>().bodyType == BodyType.DYNAMIC)
                        {
                            Debug.Log(contacts.gameObject.name);
                            contacts.transform.position += (playerCam.transform.forward * speed * Time.deltaTime) * 10;
                        }
                    }
                }
            } 
        }
    }
    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {
                var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
                tempBullet.transform.SetParent(bulletManager.gameObject.transform);
            }
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        isGrounded = cube.isGrounded;
    }

}
