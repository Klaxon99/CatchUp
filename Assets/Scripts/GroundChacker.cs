using UnityEngine;

public class GroundChacker : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    public bool TryGetGroundContact(out RaycastHit hitInfo)
    {
        return Physics.Raycast(transform.position, Vector3.down, out hitInfo);
    }
}