using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirInterval=2f;
private enum State
    {
        Roaming
    }

    private State state;
    EnemyPathfinding enemyPathFinding;


private void Awake()
    {
        enemyPathFinding = GetComponent<EnemyPathfinding>();
        state=State.Roaming;
        
    }
    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

   
    private IEnumerator RoamingRoutine()
    {
        while(state==State.Roaming)
        {
            Vector2 roamPosition= GetRoamingPosition();
            enemyPathFinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamChangeDirInterval);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; //normalize: hipotenüs örneği 
    }
}
