using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gestionCorbeau : MonoBehaviour
{
    //VARIABLE DEPLACEMENT
    public float vitesseDeplacement;
    public float forceSaut;
    public float monter;
    
    private float nbSaut = 0;

    private float vie = 1;

    private float vieBoss = 3;

    private bool tpOk = true;

    public GameObject coffreOutline;

    //VARIABLE CAM�RA
    public GameObject cameraPrincipale;
    public GameObject cameraChateau;
    public GameObject cameraCaverne;

    public GameObject boss;
    public GameObject monnaieBoss;

    public GameObject bouteilleInventaire;
    public GameObject cleInventaire;
    public GameObject pierreInventaire;
    public GameObject pierre;
    public GameObject coffre;

    public GameObject tp1;
    public GameObject tp2;

    private bool coffreOk = false;

    public AudioClip sonMonnaie;
    public AudioClip sonCorbeau;
    public AudioClip sonBouteille;
    


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
        if ((Input.GetKeyDown(KeyCode.W) && Physics2D.OverlapCircle(transform.position, 0.5f) && GetComponent<Animator>().GetBool("saut") == false) || nbSaut == 1 && Input.GetKeyDown(KeyCode.W))
        {
            nbSaut = nbSaut+1;
            print(nbSaut);
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
            nbSaut = 0;

        }

        //CRI (Attaque)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<AudioSource>().PlayOneShot(sonCorbeau);
            GetComponent<Animator>().SetBool("cri", true);
            Invoke("finCri", 1f);
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
            cameraCaverne.gameObject.SetActive(false);
        }
        //Si le joueur perd sa vie, on termine le jeu
        if (vie == 0)
        {
            ///Scene fin
            SceneManager.LoadScene("Fin Mort");
        }
        //Si c'est possible d'interagir avec le coffre...
        if (coffreOk)
        {   //On signale au joueur qu'il peut inetragir avec le coffre a l'aide d'un outline
            coffreOutline.gameObject.SetActive(true);
            //Si le joueur pese sur "E"...
            if (Input.GetKeyDown(KeyCode.E))
                {
                    //On detruit le coffre
                    Destroy(coffre.gameObject);
                    //On affiche la pierre
                    pierre.gameObject.SetActive(true);
                    //On enleve la cle de l'inventaire
                    cleInventaire.gameObject.SetActive(false);
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D infoCollision)
    {   //Si le joueur recolte une piece de monnaie...
        if (infoCollision.gameObject.name == "Monnaie")
        {
            //On joue le son de la piece de monnaie
            GetComponent<AudioSource>().PlayOneShot(sonMonnaie);
            //On detruit la piece
            Destroy(infoCollision.gameObject);
            //On augmente le compteur
            compteur++;
            nombreMonnaie.text = compteur.ToString();
            //Si le compteur arrive a 10...
            if (compteur == 10)
            {
                //...le joueur a gagne, on affiche la scene de fin
                SceneManager.LoadScene("Fin Succes");
            }
        }
        if (infoCollision.gameObject.name == "Bouteille"){
            GetComponent<AudioSource>().PlayOneShot(sonBouteille);
            Destroy(infoCollision.gameObject);
            bouteilleInventaire.gameObject.SetActive(true);
        }

        if (infoCollision.gameObject.name == "Coffre")
        {   //Si le corbeau est en possession de la cle...
            if (cleInventaire.gameObject.activeSelf == true)
            {   //...on donne la possibilite d'interagir avec le coffre
                coffreOk = true;
            }
        }
        if (infoCollision.gameObject.name == "Pierre")
        {
            Destroy(infoCollision.gameObject);
            pierreInventaire.gameObject.SetActive(true);
        }
        //Interaction teleporteur : si le corbeau entre dans la zone d'un teleporteur, sa position change pour l'autre, et vice-versa
        if (infoCollision.gameObject.name == "Teleporteur1"){
            // Variable qui nous permet d'ajouter un delai
            if (tpOk == true)
            {
                //On change la position du corbeau
                gameObject.transform.position = tp2.transform.position;
                //On change son saclin pour qu'il ne soit pas trop grand
                gameObject.transform.localScale = new Vector2(0.3f, 0.3f);
                //On enlève la possibilité de se téléporter, et on la réactive avec 2 secondes
                tpOk = false;
                Invoke("reactiverTP", 2f);
            }
        }
        //Meme chose pour le deuxieme teleporteur
        if (infoCollision.gameObject.name == "Teleporteur2"){
            if (tpOk == true)
            {
                //On change la position du corbeau
                gameObject.transform.position = tp1.transform.position;
                //On remet sa grandeur normal
                gameObject.transform.localScale = new Vector2(0.4f, 0.4f);
                //On enlève la possibilité de se teleporter, et on la réactive avec 2 secondes
                tpOk = false;
                Invoke("reactiverTP", 2f);
            }
        }
        // Si le corbeau touche le premier pont, la trappe s'active
        if (infoCollision.gameObject.tag == "Pont1"){
            infoCollision.GetComponent<Animator>().enabled = true;
        }
        // Si le corbeau tombe, il meurt
        if (infoCollision.gameObject.name == "Trou"){
            SceneManager.LoadScene("Fin Mort");
        }
    }

    private void OnCollisionEnter2D(Collision2D infoCollision)
    {
        if (Physics2D.OverlapCircle(transform.position, 0.5f))
        {   //si le joueur est sur la plateforme amovible...
            if (infoCollision.gameObject.tag == "plateforme")
            {   //...on les fait bouger
                transform.parent = infoCollision.gameObject.transform;
            }
        }
        //si le joueur entre en collision avec l'ennemi et qu'il est en train d'attaquer...
        if (infoCollision.gameObject.tag == "ennemi" && GetComponent<Animator>().GetBool("cri"))
        {   //...on detruit l'ennemi
            Destroy(infoCollision.gameObject);
        } //s'il n'est pas en train d'attaquer...
        else if (infoCollision.gameObject.tag == "ennemi" && GetComponent<Animator>().GetBool("cri") == false) {
            //le joueur perd sa vie
            vie = 0;
        }
        //Si le joueur entre en collision avec le boss et est en train d'attaquer
        if (infoCollision.gameObject.tag == "Boss" && GetComponent<Animator>().GetBool("cri"))
        {   //Si le boss a encore des vies
            if (vieBoss>0)
            {   //On lui enleve une vie
                vieBoss -= 1;
                //le boss fige, puis son animation est reactive apres 2 secondes
                infoCollision.gameObject.GetComponent<Animator>().speed = 0;
                Invoke("reactiverBoss", 2f);
            } 
            //si le boss n'a plus de vie...
            else if (vieBoss == 0)
            {   //...on le detruit
                Destroy(infoCollision.gameObject);
                //et on active la piece
                monnaieBoss.gameObject.SetActive(true);
            }
        } 
        else if (infoCollision.gameObject.tag == "Boss" && GetComponent<Animator>().GetBool("cri") == false) {
            vie = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D infoCollision)
    {
        transform.parent = null;
    }

    // Fonction pour déplacement vertical avec échelle
    private void OnTriggerStay2D(Collider2D infoCollision)
    {
        if (infoCollision.gameObject.name == "Echelle" && Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, monter);
        } 
        else if (infoCollision.gameObject.name == "Echelle" && Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -monter);
        }
    }

    private void finCri()
    {
        //On desactive le cri
        GetComponent<Animator>().SetBool("cri", false);
    }

    private void reactiverBoss()
    {
        //On reactive l'animation du boss
        boss.GetComponent<Animator>().speed = 1;
    }

    private void reactiverTP()
    {
        //On reactive la possibilite de se teleporter
        tpOk = true;
    }
}

