using Robust.Client.GameObjects;
using Robust.Shared.Player;

namespace Content.Shared._Carpmosia.PersonalLight;

public sealed partial class PersonalLightSystem : EntitySystem
{
    [Dependency] private readonly PointLightSystem _pointLightSystem = default!;
    private EntityUid? _uidRelay;
    private PointLightComponent? _lightRelay;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PersonalLightComponent, LocalPlayerDetachedEvent>(OnPlayerDetached);

        SubscribeLocalEvent<PersonalLightComponent, TogglePersonalLightActionEvent>(ToggleLight);
    }

    public void OnPlayerAttached(EntityUid uid, PersonalLightComponent comp, LocalPlayerAttachedEvent args)
    {
        comp.Toggled = false;

        if (_uidRelay != null)
            UpdateVisuals(false);
    }

    public void OnPlayerDetached(EntityUid uid, PersonalLightComponent comp, LocalPlayerDetachedEvent args)
    {
        comp.Toggled = false;

        _uidRelay = uid;
        UpdateVisuals(false);
    }

    public void ToggleLight(EntityUid uid, PersonalLightComponent comp, TogglePersonalLightActionEvent args)
    {
        if (args.Handled)
            return;

        ToggleLight(uid, comp);

        args.Handled = true;
    }

    public void ToggleLight(EntityUid uid, PersonalLightComponent comp)
    {
        comp.Toggled = !comp.Toggled;
        _uidRelay = uid;
        UpdateVisuals(comp.Toggled);
    }

    public void ToggleLight(EntityUid uid, PersonalLightComponent comp, bool state)
    {
        comp.Toggled = state;
        _uidRelay = uid;
        UpdateVisuals(comp.Toggled);
    }

    public void UpdateVisuals(bool toggled)
    {
        if (!TryComp<PointLightComponent>(_uidRelay, out var pointLight))
            return;

        _lightRelay = pointLight;
        _pointLightSystem.SetEnabled(_uidRelay.Value, toggled, _lightRelay);
    }
}
