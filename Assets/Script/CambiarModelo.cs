using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CambiarModelo : MonoBehaviour
{
    public GameObject[] plantModels; // Array que contiene los modelos de plantas
    private int currentModelIndex = 0; // Índice del modelo actualmente activo

    void Start()
    {
        // Inicializar la escena con el primer modelo de planta
        SwitchPlantModel(currentModelIndex);
    }

   
    public void SwitchToNextPlant()
    {
        currentModelIndex = (currentModelIndex + 1) % plantModels.Length;
        SwitchPlantModel(currentModelIndex);
    }

    public void SwitchToPreviousPlant()
    {
        currentModelIndex = (currentModelIndex - 1 + plantModels.Length) % plantModels.Length;
        SwitchPlantModel(currentModelIndex);
    }

    void SwitchPlantModel(int newIndex)
    {
        foreach (var plantModel in plantModels)
        {
            plantModel.SetActive(false);
        }

        plantModels[newIndex].SetActive(true);
    }

}
