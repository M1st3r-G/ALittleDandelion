using UnityEngine;

public class TableController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    public Vector3 CamPosition => transform.position + offset;
}