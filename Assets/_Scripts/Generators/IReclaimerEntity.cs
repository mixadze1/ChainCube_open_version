using _Scripts.GameEntities;

namespace _Scripts.Generators
{
    public interface IReclaimerEntity
    {
        void ReclaimEntity(GameEntity gameEntity);
        void ClearEntities();
    }
}