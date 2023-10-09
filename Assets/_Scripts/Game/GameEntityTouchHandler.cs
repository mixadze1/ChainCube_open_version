using _Scripts.GameEntities;

namespace _Scripts.Game
{
    public class GameEntityTouchHandler
    {
        public void OnFindSameEntity(GameEntity gameEntity)
        {
            gameEntity.ReclaimEntity();
        }
    }
}