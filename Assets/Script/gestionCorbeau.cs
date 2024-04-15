using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class gestionCorbeau : MonoBehaviour
{
    //VARIABLE DEPLACEMENT
    public float vitesseDeplacement;
    public float forceSaut;

    //VARIABLE CAM�RA
    public GameObject cameraPrincipale;
    public GameObject cameraChateau;
    public GameObject cameraCaverne;

    //VARIABLE OBJETS
    public TextMeshProUGUI nombreMonnaie;

    int compteur = 0;

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
        if (Input.GetKeyDown(KeyCode.W) && Physics2D.OverlapCircle(transform.position, 0.5f) && GetComponent<Animator>().GetBool("saut") == false)
        {
            velocitePerso.y = forceSaut;
            GetComponent<Animator>().SetBool("saut", true);
        }
        else
        {
            velocitePerso.y = GetComponent<Rigidbody2D>().velocity.y;
        }
        if (velocitePerso.y < 1)
        {
            GetComponent<Animator>().SetBool("saut", false);

        }

        //On applique les forces sur le rigidbody
        GetComponent<Rigidbody2D>().velocity = velocitePerso;

        //GESTION CAMERA

        if (transform.position.x >= 71f && transform.position.y >= -6f)

        {
            cameraPrincipale.gameObject.SetActive(false);
            cameraCaverne.gameObject.SetActive(false);
            cameraChateau.gameObject.SetActive(true);
        }
        else if (transform.position.y <= -6f) {
            cameraPrincipale.gameObject.SetActive(false);
            cameraCaverne.gameObject.SetActive(true);
            cameraChateau.gameObject.SetActive(false);
        }
        else
        {
            cameraPrincipale.gameObject.SetActive(true);
            cameraChateau.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D infoCollision)
    {
        
        if (infoCollision.gameObject.name == "Monnaie")
        {
            Destroy(infoCollision.gameObject);
            compteur++;
            nombreMonnaie.text = compteur.ToString();
        }
    }
}

