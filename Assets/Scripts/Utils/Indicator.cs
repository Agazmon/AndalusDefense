using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Indicator
{
    //Valores
    [SerializeField]
    private int currentValue;
    public int maxValue;
    public int initValue;

    //Gestion recuperación/deterioro automatico
    public int autoUpdateRate = 0;
    public int UpdateRateSeconds = 1;
    public bool autoUpdate = false;
    //Coroutine
    public Coroutine AutoUpdateCoroutine;
    public Image icon;
    
    //Inicialización
    public virtual void RestartStats()
    {
        CurrentValue = initValue;
        autoUpdateRate = 0;
    }

    //Operaciones
    public float GetPercentage()
   {
       return CurrentValue / maxValue;
   }

   //Get y Set publico
   public int CurrentValue
   {
       get => currentValue;
       set
       {
           currentValue = value >= 0 ? value : 0;
           if (OnIndicatorChange != null)
               OnIndicatorChange(value);
       }
   }
   public void setAutoUpdate(bool newState)
   {
        this.autoUpdate = newState;
   }

    public IEnumerator AutoUpdateStart()
    {
        while (autoUpdate) { 
            yield return new WaitForSeconds(UpdateRateSeconds);
            CurrentValue += autoUpdateRate;
        }
    }
   

    //Eventos para detectar cambios en el indicador
    public delegate void OnIndicatorChangeDelegate(int newValue);
    public event OnIndicatorChangeDelegate OnIndicatorChange;
}
