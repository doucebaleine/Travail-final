using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defilementarriereplan : MonoBehaviour
{
    public float vitesse;
    public float positionFin;
    public float positionDebut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(-vitesse, 0, 0);

        if (transform.position.x < positionFin){

            transform.position = new Vector2(positionDebut, transform.position.y);
        }
    } 
    
}
