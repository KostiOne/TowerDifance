using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[ExecuteAlways]
public class CoordinateLabel : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;
    [SerializeField] Color defaultColour = Color.white;
    [SerializeField] Color blockColour = Color.grey;

    void Awake(){
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();
        displayCoordinates();
    }
    
    void LabelsBecomeInvisable(){
        if(Input.GetKeyDown(KeyCode.C)){
           label.enabled = !label.IsActive();
        }
    }

    void Update()
    {
        if(!Application.isPlaying){
            //Plaing
            displayCoordinates();
            UpdateObjectname();
        }

        ColorCoordnates();
        LabelsBecomeInvisable();
    }


    void ColorCoordnates(){
        if(waypoint.IsPlacable != true){
            label.color = defaultColour;
        }else{
            label.color = blockColour;
        }
    }

    void displayCoordinates(){
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
  
        label.text =  coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectname(){
        transform.parent.name = coordinates.ToString();
    }
}
