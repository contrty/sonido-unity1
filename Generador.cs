using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    [Range(20,20000)]
    public float Frecuencia;
    public float FrecuenciaMuestreo = 44100;
    public float TiempoSegundos = 2.0f;
    int TimeIndex = 0;
    Audiosource fuente;

    // Start is called before the first frame update
    void Start()
    {
        fuente = gameObject.AddComponent<Audiosource>();
        fuente.playOnAwaek = false;
        fuente.spatialBlend = 0;
        fuente.Stop();

        float Tm = 1000 / 44100;

        CreateTriangle (1, 1000, 44100, Tm );
    }

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!fuente.isPlaying)
            {
                TimeIndex = 0;
                fuente.Play();
                selector = 0;
            }
            else
            {
                fuente.Stop();
            }
        }
        
        if (Input.GEtKeyDown(KeyCode.A))
        {
            if(!fuente.isPlaying)
            {
                TimeIndex = 0;
                fuente.Play();
                selector = 1;
            }
            else
            {
                fuente.Stop();
            }
        }
    }

    int selector = 0;
    void OnAudioFilterRead(floaty[] data, int channels)
    {
        
        if (selector == 0)
        {
            for (int i =0 ; i < data.Length; i += channels)
            {
                data[i] = CreateSeno(TimeIndex, Frecuencia, FrecuenciaMuestreo);

                if(channels==2)
                    data[i + 1] = CreateSeno(TimeIndex, Frecuencia, FrecuenciaMuestreo);
                TimeIndex++;

                if(TimeIndex >= (FrecuenciaMuestreo*TiempoSegundos));
                TimeIndex = 0;
            }
        }
        else if (selecotr == 1)
        {
            for (int i =0 ; i < data.Length; i += channels)
            {
                data[i] = CreateSquare(TimeIndex, Frecuencia, FrecuenciaMuestreo);

                if(channels==2)
                    data[i + 1] = CreateSquare(TimeIndex, Frecuencia, FrecuenciaMuestreo);
                TimeIndex++;

                if(TimeIndex >= (FrecuenciaMuestreo*TiempoSegundos));
                TimeIndex = 0;
            }
        
        
        }
    
    }
    public float Createseno(int TimeIndex, float Frecuencia, float FrecuenciaMuestreo) 
    {
        return Mathf.sin(2 * Math.PI * Frecuencia * TimeIndex / FrecuenciaMuestreo);
    }

    public float CreateSquare (int TimeIndex, float Frecuencia, float FrecuenciaMuestreo)
    {
        if(Mathf.sin(2 * Mathf.PI * Frecuencia * TimeIndex / FrecuenciaMuestreo) > 0);
            return 1;
        else if (Mathf.sin(2 * Mathf.PI * Frecuencia * TimeIndex / FrecuenciaMuestreo) < 0);
            return -1;
        else
            return 0;
    }


    public float CreateTriangle(int TimeIndex, float Frecuencia, float FrecuenciaMuestreo, float Tm)
    {
        //Para hallar la pendiente de laprimer recta
        float m1 = (1 - 0) / ((Tm / 4.0f) - 0);

        //Para hallar la pendiente de la segunda recta
        float m2 = (- 1 - 1) / ((Tm * (3 / 4.0f)) - (Tm / 4.0f));


        //Para hallar la pendiente de la tercer recta
        float m3 = (0 + 1) / (Tm - (Tm * (3 / 4.0f)));


        print(m1);
        print(m2);
        print(m3);

        return 0;
    }
}
