using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gestionTemps : MonoBehaviour
{

    // Ce code ne m'aappartient pas, je l'ai pris à partir de cette vidéo : https://www.youtube.com/watch?v=hxpUk0qiRGs

    public float TempsRestant;
    public bool TempsMarche = false;

    public TextMeshProUGUI Temps;
    // Start is called before the first frame update
    void Start()
    {
        TempsMarche = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TempsMarche)
        {
            if (TempsRestant>0)
            {
                TempsRestant -= Time.deltaTime;
                mettreAJour(TempsRestant);
            }
            else
            {
                // Scene fin
                SceneManager.LoadScene("Fin Temps");
                TempsRestant = 0;
                TempsMarche = false;
            }
        }
    }

    void mettreAJour(float tempsPresent)
    {
        tempsPresent += 1;

        float minutes = Mathf.FloorToInt(tempsPresent / 60);
        float secondes = Mathf.FloorToInt(tempsPresent % 60);

        Temps.text = string.Format("{0:00} : {1:00}", minutes, secondes);
    }

}


