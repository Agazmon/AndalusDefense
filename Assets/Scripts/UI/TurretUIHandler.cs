using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurretUIHandler : MonoBehaviour
{
   
    [Header("Turret Settings")]
    public bool isBuyed;
    public CannonController turret;
    [Header("Buy Settings")]
    public int turretPrice = 300;
    public ResourceType resourceCost = ResourceType.GOLD;
    [Header("Upgrade Settings")]
    public int resourceCostIncrementPerLevel = 1;
    [Header("Change Element Settings")]
    public int ChangeElementCost = 1;
    public bool firstTime = true;
    [Header("UI Settings")]
    public Color BlinkWrongColor;
    [Header("Private Resources")]
    public GameObject associatedTurret;
    public ResourcesController resourcesController;
    public Button PurchaseButton;
    public Button LevelUpButton;
    public Button SwitchToFireButton;
    public Button SwitchToPlantButton;
    public Button SwitchToWaterButton;
    public Button ReloadButton;

    void Start()
    {
        resourcesController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ResourcesController>();
        turret = associatedTurret.transform.GetChild(0).gameObject.GetComponent<CannonController>();
        if (!isBuyed)
            ChangeTurretVisibility(false);
    }

    private void ChangeTurretVisibility(bool newState)
    {
        MeshRenderer[] lista = associatedTurret.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in lista)
        {
            renderer.enabled = newState;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBuyed)
            CheckBuyRequirements();
        else
        {
            CheckUpgradeRequirements();
            CheckChangeElementRequirements(SwitchToFireButton, ResourceType.FIRE);
            CheckChangeElementRequirements(SwitchToPlantButton, ResourceType.PLANT);
            CheckChangeElementRequirements(SwitchToWaterButton, ResourceType.WATER);
        }
    }

    private void CheckBuyRequirements()
    {
        if (resourcesController.GetResource(resourceCost) > turretPrice)
            PurchaseButton.interactable = true;
    }
    private void CheckUpgradeRequirements()
    {
        if (resourcesController.GetResourceByElement(turret.activeStats.Element.Element) > turret.activeStats.CurrentLevel * resourceCostIncrementPerLevel)
            LevelUpButton.interactable = true;
        else if (LevelUpButton.enabled)
            LevelUpButton.interactable = false;
    }
    private void CheckChangeElementRequirements(Button button, ResourceType resourceType)
    {
        if (resourcesController.GetResourceByElement(turret.activeStats.Element.Element) > ChangeElementCost)
            button.enabled = true;
        else if (button.enabled)
            button.enabled = false;
    }

    public void buyTurret()
    {
        if (!resourcesController.RemoveResource(resourceCost, turretPrice))
        {
            StartCoroutine(blinkButton(PurchaseButton));
        } else
        {
            isBuyed = true;
            enableTurret();
        }
    }

    private void enableTurret()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        ChangeTurretVisibility(true);
        turret.ChangeStatus(CannonStatus.IDLE);
    }

    public void levelUpTurret()
    {
        if(resourcesController.RemoveResourceByElement(turret.activeStats.Element.Element, turret.activeStats.CurrentLevel * resourceCostIncrementPerLevel))
        {
            //Comprada mejora
            //SFX?
        }
        else
        {
            //NO Comprada mejora
        }
    }
    public void reloadTurret()
    {
        //Recargar torreta
    }
    public void ChangeToElement(ElementType type, Button button)
    {
        if (firstTime)
        {
            firstTime = false;
            turret.SetActiveStats(type);
        }
        else
        {
            if (resourcesController.RemoveResourceByElement(type, ChangeElementCost))
                turret.SetActiveStats(type);
            else
                StartCoroutine(blinkButton(button));
        }
    }
    public void ChangeToFire() {
        ChangeToElement(ElementType.Fire, SwitchToFireButton);
    }
    public void ChangeToWater()
    {
        ChangeToElement(ElementType.Water, SwitchToFireButton);
    }
    public void ChangeToPlant()
    {
        ChangeToElement(ElementType.Grass, SwitchToFireButton);
    }

    private IEnumerator blinkButton(Button button)
    {
        float timeLeft = 1.0f;
        List<Color> colors = new List<Color>() {
            BlinkWrongColor,
            button.GetComponent<Image>().color
        };
        foreach(Color newColor in colors)
        {
            timeLeft = 1.0f;
            while (timeLeft >= 0)
            {
                button.GetComponent<Image>().color = Color.Lerp(button.GetComponent<Image>().color, newColor, Time.deltaTime / timeLeft);
                timeLeft -= Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
