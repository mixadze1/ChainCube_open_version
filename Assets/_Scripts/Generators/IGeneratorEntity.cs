using _Scripts.Game;
using _Scripts.Models;

namespace _Scripts.Generators
{
    public interface IGeneratorEntity
    { 
        void GenerateEntity(PresetsGameModel presetsGameModel, ContainerGameEntity presetContainerPosition);

        void GenerateMainEntity();
    }
}