using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeColorOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MeshRenderer model; // A reference to the mesh renderer that needs its color changed
    public Color normalColor; // The default color of the model
    public Color hoverColor; // The color that should be applied on the model when the pointer is hovering over it

    // Start is called before the first frame update
    void Start()
    {
        model.material.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData) // Called when the pointer enters the GameObject 
    {
        model.material.color = hoverColor; // Changes the color of the model's material
    }

    public void OnPointerExit(PointerEventData eventData) // Called when the pointer exits the GameObject.
    {
        model.material.color = normalColor;  // Resets the color of the material to its normal value
    }

}
