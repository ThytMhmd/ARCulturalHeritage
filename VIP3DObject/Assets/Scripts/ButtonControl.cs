using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ButtonControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Text objectNameText; // Legacy UI Text component
    public GameObject infoCanvas;

    public GameObject displayer;
    public GameObject placePic;

    public RawImage coverImage;
    public RawImage covertext;
    public RawImage coverPlace;

    public Outline imageOutline;
    public Outline textOutline;
    public Outline placemtnOutline;


    void Start()
    {
        
        showJustText();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showJustText()
    {
        imageOutline.enabled = false;
        placemtnOutline.enabled = false;
        textOutline.enabled = true;

        coverImage.enabled = false;
        coverPlace.enabled = false;
        covertext.enabled = true;

        objectNameText.gameObject.SetActive(true);
        displayer.SetActive(false);
        placePic.SetActive(false);
        //rawImage.texture = null;
       
    }

    public void showJustImages()
    {
        objectNameText.gameObject.SetActive(false);
        placePic.SetActive(false);
        displayer.SetActive(true);
            
        imageOutline.enabled = true;
        textOutline.enabled = false;
        placemtnOutline.enabled = false;

        coverImage.enabled = true;
        covertext.enabled = false;
        coverPlace.enabled = false;
    }

    public void showPlacement()
    {
        //tab.SetActive(false);
        objectNameText.gameObject.SetActive(false);
        displayer.SetActive(false);
        placePic.SetActive(true);

        imageOutline.enabled = false;
        textOutline.enabled = false;
        placemtnOutline.enabled = true;

        coverImage.enabled = false;
        covertext.enabled = false;
        coverPlace.enabled = true;
    }
}
