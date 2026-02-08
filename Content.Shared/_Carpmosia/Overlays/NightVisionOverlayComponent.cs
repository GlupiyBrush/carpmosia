using System.Numerics;
using Content.Shared.Actions;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Carpmosia.Overlays;

[RegisterComponent, NetworkedComponent]
public sealed partial class NightVisionOverlayComponent : Component
{
    [ViewVariables]
    public bool Toggled;

    [DataField, ViewVariables]
    public EntProtoId ToggleAction = "ActionToggleNightVision";

    [ViewVariables]
    public EntityUid? ToggleActionEntity;

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public Color Tint = new(255, 255, 255);
}

public sealed partial class ToggleNightVisionActionEvent : InstantActionEvent { }
