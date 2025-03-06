using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;
using Zenject;
using Zenject.Asteroids;

public class Hanging : MonoBehaviour
{
    [SerializeField] Transform bottomPoint;
    [SerializeField] Transform topPoint;
    [SerializeField] private float hangRayDistance;
    [SerializeField] private LayerMask hangRayLayerMask;

    private Player _player;
    private PlayerStateMachine _stateMachine;
    public bool isOnWall = false;
    public bool isOnLedge = false;


    [Inject]
    public void Construct(Player player, PlayerStateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
    }


    public bool IsCanHang()
    {
        Ray hangRay = new Ray(bottomPoint.position,bottomPoint.forward);
        Ray topRay = new Ray(topPoint.position,bottomPoint.forward);

        Debug.DrawRay(bottomPoint.position, bottomPoint.forward * hangRayDistance, Color.blue,0.2f);
        Debug.DrawRay(topPoint.position, topPoint.forward * hangRayDistance * 2f, Color.red,0.2f);

        //todo Определение может ли цепляться доделать 
        if (Physics.Raycast(hangRay, out RaycastHit hit, hangRayDistance,hangRayLayerMask))
        {
            Debug.Log($"Bottom ray is catch {hit.collider.name}");
            isOnWall = true;            
        }
        else isOnWall = false;

        if (isOnWall && !(Physics.Raycast(topRay, out RaycastHit topHit, hangRayDistance * 2, hangRayLayerMask)))
        {
            Debug.Log($"PLAYER ON THE LEDGE{this}");
            isOnLedge = true;
            Hang();
            return true;
        }
        else
        {
            isOnLedge = false;
        }

        return false;
    }

    private void Start()
    {
        StartCoroutine(TryHangingCoroutine());
    }

    private IEnumerator TryHangingCoroutine()
    {
        while ( true )
        {
            yield return new WaitForSeconds(0.02f);
            if (!isOnLedge)
            {
                IsCanHang(); 
            }
        }
    }
    private void Hang()
    {
        Debug.Log($"Player is hanging {this}");
        _stateMachine.SetState<OnWallState>();
        //_player.isGravityActive = false;
    }

    //private IEnumerator StartHangingCoroutine()
    //{
    //    StartCoroutine( HangingCoroutine());
    //    yield return new WaitForSeconds(1.5f);
    //    StopHang();
    //}






    //public void StartHang()
    //{

    //    Debug.Log($"Start try hang {this}");
    //    StartCoroutine(StartHangingCoroutine());
    //}

    //public void StopHang()
    //{
    //    StopCoroutine(HangingCoroutine());
    //    Debug.Log($"Stop try hang {this}");
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(bottomPoint.position,new Vector3(bottomPoint.position.x,bottomPoint.position.y, bottomPoint.position.z + hangRayDistance));
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(topPoint.position,new Vector3(topPoint.position.x, topPoint.position.y, topPoint.position.z + hangRayDistance));
    //}

}
