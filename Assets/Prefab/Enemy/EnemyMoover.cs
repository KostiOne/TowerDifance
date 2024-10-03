using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Enemy))]
public class EnemyMoover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f,5f)] float speed = 1f;
    Enemy enemy;
    // Start is called before the first frame update

    void Awake(){

    }
    void OnEnable(){//FindPath();
        FindPath();
        ReturnToStart();
        StartCoroutine(WayToTHePoint());
    }
    void Start(){
        enemy = GetComponent<Enemy>();
    }

    void FindPath(){
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if(waypoint != null){

                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart(){
        transform.position = path[0].transform.position;
    }

    IEnumerator WayToTHePoint()
    {
        foreach (Waypoint Waypoint1 in path)
        {
            //Debug.Log(Waypoint1.name);
            Vector3 strartPosition = transform.position;
            Vector3 endPosition = Waypoint1.transform.position;
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
