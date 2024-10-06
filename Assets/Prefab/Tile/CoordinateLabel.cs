using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabel : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    //Waypoint waypoint;
    GridManager gridManager;
    [SerializeField] Color defaultColour = Color.white;
    [SerializeField] Color blockColour = Color.grey;
    [SerializeField] Color pathColor = Color.yellow;
    [SerializeField] Color exploredColor = Color.red;

    void Awake(){
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        //waypoint = GetComponentInParent<Waypoint>();
        displayCoordinates();
    }
    
    void ToogleLabels(){
        if(Input.GetKeyDown(KeyCode.C)){
            Debug.Log("1");
           label.enabled = !label.IsActive();
        }
    }

    void Update()
    {
        if(!Application.isPlaying){
            //Plaing
            UpdateObjectname();
            displayCoordinates();
        }
        label.enabled = true;
        SetLabelColour();
        ToogleLabels();
    }


    void SetLabelColour(){

        if(gridManager == null){return;}

        Node node = gridManager.GetNode(coordinates);

        if(node == null){return;}

        if(!node.isWalkable){
            label.color = blockColour;
        }
        else if(node.isPath){
            label.color = exploredColor;
        }
        else if(node.isExplored){
            label.color = pathColor;
        }
        else{
            label.color = defaultColour;
        }
        //if(waypoint.IsPlacable != true){
        //    label.color = defaultColour;
        //}else{
        //    label.color = blockColour;
        //}
    }

    void displayCoordinates(){
        if(gridManager == null){return;}
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
  
        label.text =  coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectname(){
        transform.parent.name = coordinates.ToString();
    }
}
