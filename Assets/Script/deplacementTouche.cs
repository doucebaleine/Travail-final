using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacementTouche : MonoBehaviour
{

    public Transform personnage;
    public Vector3 distanceEntre;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = personnage.position + distanceEntre;
    }
}
