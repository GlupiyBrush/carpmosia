using Content.Shared.Actions;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Carpmosia.PersonalLight;

[RegisterComponent, NetworkedComponent]
public sealed partial class PersonalLightComponent : Component
{
    [ViewVariables]
    public bool Toggled;

    [DataField, ViewVariables]
    public EntProtoId ToggleAction = "ActionTogglePersonalLight";

    [ViewVariables]
    public EntityUid? ToggleActionEntity;
}


public sealed partial class TogglePersonalLightActionEvent : InstantActionEvent { }
