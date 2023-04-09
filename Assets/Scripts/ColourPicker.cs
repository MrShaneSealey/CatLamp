using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ColourPicker : MonoBehaviour
{

    [Header("Object Colour Controller")]
    [SerializeField] private Renderer rend;
    public Color masterColour;

    //public List<string> shaderColourStrings;
    private String _refColour = "_refColour"; //name of shader Colour Varible Datatype

    [Header("Colour Picker")]
    [SerializeField] private RectTransform rectT;
    [SerializeField] private Texture2D refSprite; //colour picker png
    [SerializeField] private bool toggle = true;

    [Header("Objects")]
    [SerializeField] private GameObject[] gameObjects;



    [Header("Lerp")]
    [Range(0f, 4f)] public float lerpTime; //time it takes
    [Range(0f, 4f)] public float lerpRate = 0.9f;
    [SerializeField] private Color[] colour;
    private int colorIndex = 0;
    private float time = 0f;
    private int len;

    [Header("Colour Corection")]
    private Color convertColour; // convert Colour
    private float H, S, V;


    private void Start()
    {

        for (int i = 0; i < gameObjects.Length; i++)
        {
            rend = gameObjects[i].GetComponent<Renderer>();
        }

        foreach(GameObject obj in gameObjects)
        {
            rend = obj.GetComponent<Renderer>();
        }

        len = colour.Length;
    }

    private void SetColour()
    {
        Vector3 imagePos = rectT.position;

        ////////////////////////////////////////////////////////////////////////////
        // Mouse Postions - Make Sure Panel or Button is anochred to bottom right //
        ////////////////////////////////////////////////////////////////////////////
        float globalPosX = Input.mousePosition.x - imagePos.x;
        float globalPosY = Input.mousePosition.y - imagePos.y;

        int localPosX = (int)(globalPosX * (refSprite.width/rectT.rect.width));
        int localPosY = (int)(globalPosY * (refSprite.height/rectT.rect.height));

        Color c = refSprite.GetPixel(localPosX, localPosY);

        HSVConvert(c);

        //setcolour of Particle & Object shader
        rend.material.SetColor(_refColour, masterColour);

    }

    private void HSVConvert(Color c)
    {
        //HSV COLOUR /####/ H = 0 // S = 1 // V = 2 /####/

        //Convert
        Color.RGBToHSV(c, out H, out S, out V);
        Debug.Log(H + "," + S + "," + V );

        //Reconvert to RGB//     \/ Making sure its always its brightess
        c = Color.HSVToRGB(H, S, 1.0f);

        masterColour = c;
    }

    public void OnClickPicker()
    {
        //button Interaction
        SetColour();
    }
    void ColourLerp()
    {
        rend.material.SetColor(_refColour, masterColour);

        convertColour = Color.Lerp(rend.material.GetColor(_refColour), colour[colorIndex], lerpTime * Time.deltaTime);

        HSVConvert(convertColour);


        //# set renderer to colour lerp( Colour of material, colour index, lerp time)
        //# transitions to from colour to  

        masterColour.a = 1.0f; //fixed alpha

        time = Mathf.Lerp(time, 1f, lerpTime*Time.deltaTime);
        if (time > lerpRate) //float makes Lerp Faster when decreased
        {
            time = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0: colorIndex; //len is colour amouunt
        }
    }
    public void Toggle()
    {
        if (!toggle)
        {
            toggle = true;
        }
        else if (toggle)
        {
            toggle = false;
        }
    }

    void Update()
    {
        


        if (toggle)
        {
            ColourLerp();
        }
        
    }
}

