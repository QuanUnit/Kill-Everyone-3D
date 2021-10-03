using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private Player attachedPlayer;

    private void Awake()
    {
        attachedPlayer = GetComponent<Player>();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                DoInput(Input.mousePosition);
            return;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
                DoInput(Input.touches[0].position);
            return;
        }
    }

    private void DoInput(Vector3 screenPosition)
    {
        switch (attachedPlayer.State)
        {
            case Player.PlayerState.Fight:
                attachedPlayer.Shot(ThrowRayFromCameraToPosition(screenPosition));
                break;

            case Player.PlayerState.Idle:
                WaySheduler.Instance.StartWay(attachedPlayer);
                break;
            default:
                break;
        }
    }

    private Vector3 ThrowRayFromCameraToPosition(Vector3 position)
    {
        Ray ray = mainCamera.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out var hit))
            return hit.point;

        return ray.direction * 100000;
    }
}
