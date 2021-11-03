using UnityEngine;

public class DestroyerPlane : MonoBehaviour
{
  void OnCollisionEnter(Collision other)
  {
    Destroy(other.gameObject);
  }
}
