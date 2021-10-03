using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaySheduler : Singleton<WaySheduler>
{
    public UnityEvent OnWayIsPassed;

    [SerializeField] private List<EnemiesCamp> enemiesCamps;

    private bool canBeStarted = true;
    private int currentCampIndex = 0;

    public void StartWay(Player player)
    {
        if (canBeStarted == false)
            return;

        canBeStarted = false;

        currentCampIndex = 0;
        GoNext(player);
    }

    private void GoNext(Player player)
    {
        currentCampIndex++;

        EnemiesCamp nextCamp = enemiesCamps[currentCampIndex];

        player.State = Player.PlayerState.Dislocation;
        player.MoveTo(nextCamp.WayPoint.position);

        StartCoroutine(CheckCameToCamp(nextCamp, delegate { OnCameToCamp(nextCamp, player); }, player.transform, 0.1f, 0.2f));
    }

    private void OnCameToCamp(EnemiesCamp camp, Player player)
    {
        if (currentCampIndex >= enemiesCamps.Count - 1)
        {
            OnWayIsPassed?.Invoke();
            return;
        }

        player.State = Player.PlayerState.Fight;
        if (camp.Status == EnemiesCamp.CampStatus.cleared)
        {
            GoNext(player);
            return;
        }

        camp.OnCampCleared.AddListener(delegate { GoNext(player); });
    }

    public IEnumerator CheckCameToCamp(EnemiesCamp camp, Action callBack, Transform checkedTarget, float checkDelta, float thresholdDistance)
    {
        while(true)
        {
            if(Vector3.Distance(camp.WayPoint.position, checkedTarget.position) < thresholdDistance)
            {
                callBack?.Invoke();
                break;
            }
            yield return new WaitForSeconds(checkDelta);
        }
    }
}
