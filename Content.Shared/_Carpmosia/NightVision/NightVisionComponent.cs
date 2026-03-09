using Content.Shared.Actions;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Carpmosia.NightVision;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(true)]
public sealed partial class NightVisionComponent : Component
{
    public bool ToggledLocally;

    [AutoNetworkedField]
    [ViewVariables(VVAccess.ReadWrite)]
    public bool Toggled;

    [DataField("toggleAction"), AutoNetworkedField]
    [ViewVariables(VVAccess.ReadOnly)]
    public EntProtoId ToggleAction = "ActionToggleNightVision";

    [AutoNetworkedField]
    [ViewVariables(VVAccess.ReadOnly)]
    public EntityUid? ToggleActionEntity;

    [DataField("soundOn", required: true), AutoNetworkedField]
    [ViewVariables(VVAccess.ReadWrite)]
    public SoundSpecifier? SoundOn;

    [DataField("soundOff", required: true), AutoNetworkedField]
    [ViewVariables(VVAccess.ReadWrite)]
    public SoundSpecifier? SoundOff;

    [DataField, AutoNetworkedField]
    [ViewVariables(VVAccess.ReadWrite)]
    public Color Tint = new(255, 255, 255);
}

public sealed partial class ToggleNightVisionActionEvent : InstantActionEvent { }
