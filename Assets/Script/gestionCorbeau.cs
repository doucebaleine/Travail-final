using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestionCorbeau : MonoBehaviour
{
    public float vitesseDeplacement;
    public float forceSaut;

    private Vector2 velocitePerso;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //MARCHE
        if (Input.GetKey(KeyCode.D))
        {
            velocitePerso.x = vitesseDeplacement;
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Animator>().SetBool("marche", true);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            velocitePerso.x = -vitesseDeplacement;
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Animator>().SetBool("marche", true);
        }
        else
        {
            velocitePerso.x = 0f;
            GetComponent<Animator>().SetBool("marche", false);
        }


        //SAUT
        if (Input.GetKeyDown(KeyCode.W) && Physics2D.OverlapCircle(transform.position, 0.5f))
        {
            velocitePerso.y = forceSaut;
            GetComponent<Animator>().SetBool("saut", true);
        }
        else
        {
            velocitePerso.y = GetComponent<Rigidbody2D>().velocity.y;
        }
        if (velocitePerso.y == 0)
        {
            GetComponent<Animator>().SetBool("saut", false);
        }

        //On applique les forces sur le rigidbody
        GetComponent<Rigidbody2D>().velocity = velocitePerso;
        
        }
    }

