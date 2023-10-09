namespace _Scripts.Game
{
    public class GameEntityTouchHandler
    {
        public void OnFindSameEntity(GameEntity.GameEntity gameEntity)
        {
            gameEntity.ReclaimEntity();
        }
    }
}