using UnityEngine;

namespace _Scripts.GameEntities
{
   public class Poof : MonoBehaviour
   {
      [SerializeField] private ParticleSystem _particle;

      private float _fixedTime;
      private float _timeDelay = 1.5f;
   
      public void Initialize(Vector3 position)
      {
         this.transform.position = position;
         _particle.Play();
      }

      private void FixedUpdate()
      {
         _fixedTime += Time.fixedDeltaTime;
         if (_fixedTime >= _timeDelay)
         {
            _fixedTime = 0;
            DestroyPoof();
         }
      }

      private void DestroyPoof() => 
         Destroy(gameObject);
   }
}
