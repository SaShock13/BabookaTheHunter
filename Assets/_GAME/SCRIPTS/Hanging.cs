using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;
using Zenject;
using Zenject.Asteroids;
using System;

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
    public bool isTryToHang = false;
    private Vector3 thirdRayOrigin;
    private float twoRaysYDistance;
    private Vector3 hangNormal;

    [Inject]
    public void Construct(Player player, PlayerStateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
    }

    private void Start()
    {
        twoRaysYDistance = topPoint.position.y - bottomPoint.position.y;
    }

    public bool IsCanHang()
    {
        Ray bottomRay = new Ray(bottomPoint.position,bottomPoint.forward);
        Ray topRay = new Ray(topPoint.position,bottomPoint.forward);

        if (Physics.Raycast(bottomRay, out RaycastHit hit, hangRayDistance,hangRayLayerMask))
        {
            Debug.Log($"Bottom ray is catch {hit.collider.name}");
            hangNormal = hit.normal;
            isOnWall = true;            
        }
        else isOnWall = false;

        if (isOnWall && !(Physics.Raycast(topRay, out RaycastHit topHit, hangRayDistance * 2, hangRayLayerMask)))
        {
            Debug.Log($"PLAYER ON THE LEDGE{this}");
            isOnLedge = true;

            float fromTopOffset = 0;
            if (Physics.Raycast(thirdRayOrigin, Vector3.down, out RaycastHit topDownHit, twoRaysYDistance * 1.1f, hangRayLayerMask))
            {
                fromTopOffset = topDownHit.point.y - thirdRayOrigin.y;
                Debug.Log($"Catch cube with fromTopOffset  {fromTopOffset}");
            }
                _player.StartYAdjust(fromTopOffset, hangNormal);
            

            Hang();
            return true;
        }
        else
        {
            isOnLedge = false;
        }
       
        return false;
    }

    private void Update()
    {
        if (isTryToHang && !isOnLedge)
        {  
            IsCanHang();
        }
        thirdRayOrigin = topPoint.position + topPoint.forward * hangRayDistance * 2f;
        //Debug.DrawRay(bottomPoint.position, bottomPoint.forward * hangRayDistance, Color.blue);
        //Debug.DrawRay(topPoint.position, topPoint.forward * hangRayDistance * 2f, Color.red);
        //Debug.DrawRay(thirdRayOrigin, Vector3.down * twoRaysYDistance * 1.1f , Color.green );

    }

    private void Hang()
    {
        Debug.Log($"Player is hanging {this}");
        _stateMachine.SetState<OnWallState>();
    }

    public void StartTryHang()
    {

        Debug.Log($"Start try hang {this}");
        isTryToHang = true;
    }

    public void StopTryHang()
    {
        Debug.Log($"Stop try hang {this}");
        isTryToHang = false;
    }

}
