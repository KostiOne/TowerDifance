using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
   [SerializeField] Tower towerPrefab;
   [SerializeField] bool isPlaceble;
   public bool Isplaceable {get{return isPlaceble;}}

   GridManager gridManager;
   Pathfinder pathfinder;
   Vector2Int coordinates = new Vector2Int();

   void Awake(){
    gridManager = FindObjectOfType<GridManager>();
    pathfinder = FindObjectOfType<Pathfinder>();
   }

    void Start(){
        if(gridManager != null){
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if(!isPlaceble){
                gridManager.BlockNode(coordinates);
            }
        }
    }

   void OnMouseDown(){
    if(gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates)){
        bool isSuccsesfull = towerPrefab.CreateTower(towerPrefab,transform.position);
        if(isSuccsesfull){

            gridManager.BlockNode(coordinates);
            pathfinder.NotifyRecievers();
        }
        //isPlaceble = !isPlaceble;
    }
   }
}
