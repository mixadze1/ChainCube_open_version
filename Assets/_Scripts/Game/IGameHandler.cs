namespace _Scripts.Game
{
    public interface IGameHandler
    {
        void CreateNewGameEntityAfterUsedPrevious();
        void ReclaimGameEntity(GameEntity.GameEntity gameEntity);
    }
}