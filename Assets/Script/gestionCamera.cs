
using System.Collections.Generic;
using UnityEngine;

public class gestionCamera : MonoBehaviour
{
    public GameObject cibleASuivre;

    public float limiteGauche;
    public float limiteDroite;
    public float limiteHaut;
    public float limiteBas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 laPosition = cibleASuivre.transform.position;

        if (laPosition.x < limiteGauche) laPosition.x = limiteGauche;
        if (laPosition.x > limiteDroite) laPosition.x = limiteDroite;
        if (laPosition.y < limiteBas) laPosition.y = limiteBas;
        if (laPosition.y > limiteHaut) laPosition.y = limiteHaut;
        laPosition.z = -20;
        
        transform.position = laPosition;

        if (cibleASuivre.transform.position.x >= 114)
        {
            limiteDroite = 132;
        }
    }
}
