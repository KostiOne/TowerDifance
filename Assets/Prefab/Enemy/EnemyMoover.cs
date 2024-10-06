using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Enemy))]
public class EnemyMoover : MonoBehaviour
{
    List<Node> path = new List<Node>();
    [SerializeField] [Range(0f,5f)] float speed = 1f;
    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;
    // Start is called before the first frame update

    void OnEnable(){//FindPath();
        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake(){
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void RecalculatePath(bool resetPath){
        Vector2Int coordinates = new Vector2Int();

        if(resetPath){
            coordinates = pathfinder.StartDestination;
        }else{
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);

        StartCoroutine(WayToTHePoint());
    }

    void ReturnToStart(){
        //transform.position = path[0].transform.position;
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartDestination);
    }

    IEnumerator WayToTHePoint()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 strartPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(strartPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        Finish();
    }

    private void Finish()
    {
        enemy.PenaltyApply();
        gameObject.SetActive(false);
    }
}
