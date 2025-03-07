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

    [Inject]
    public void Construct(Player player, PlayerStateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
    }


    public bool IsCanHang()
    {
        Ray bottomRay = new Ray(bottomPoint.position,bottomPoint.forward);
        Ray topRay = new Ray(topPoint.position,bottomPoint.forward);

        if (Physics.Raycast(bottomRay, out RaycastHit hit, hangRayDistance,hangRayLayerMask))
        {
            Debug.Log($"Bottom ray is catch {hit.collider.name}");
            isOnWall = true;            
        }
        else isOnWall = false;

        if (isOnWall && !(Physics.Raycast(topRay, out RaycastHit topHit, hangRayDistance * 2, hangRayLayerMask)))
        {
            Debug.Log($"PLAYER ON THE LEDGE{this}");
            isOnLedge = true;

            //todo Нужно пофиксить пока не работает
            // player Y position correction
            float fromTopOffset = 0;
            
            var twoRaysYDistance = topPoint.position.y - bottomPoint.position.y;
            if (Physics.Raycast(thirdRayOrigin, Vector3.down, out RaycastHit topDownHit, twoRaysYDistance, hangRayLayerMask))
            {
                fromTopOffset = topDownHit.point.y - thirdRayOrigin.y;
                Debug.Log($"Catch cube with fromTopOffset  {fromTopOffset}");
            }
                Debug.Log($"fromTopOffset {fromTopOffset}");
            _player.StartYAdjust(fromTopOffset);


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
        thirdRayOrigin = topPoint.forward * hangRayDistance * 2f;
    }

    private void Update()
    {
        if (isTryToHang && !isOnLedge)
        {  
            IsCanHang();
        }

        Debug.DrawRay(bottomPoint.position, bottomPoint.forward * hangRayDistance, Color.blue);
        Debug.DrawRay(topPoint.position, topPoint.forward * hangRayDistance * 2f, Color.red);
        Debug.DrawRay(thirdRayOrigin, Vector3.down, Color.green );

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
