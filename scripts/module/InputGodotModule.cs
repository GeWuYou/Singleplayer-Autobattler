
using GFramework.Game.input;
using GFramework.Godot.input;
using SingleplayerAutobattler.scripts.architecture;

namespace SingleplayerAutobattler.scripts.module;

public class InputGodotModule: AbstractGodotInputModule<GameArchitecture>
{
    protected override bool EnableDefaultTranslator => true;

    protected override void RegisterTranslator(InputSystem inputSystem)
    {
        
    }
}