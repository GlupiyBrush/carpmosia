using Content.Client.Overlays;
using Content.Shared._Carpmosia.Overlays;
using Content.Shared._Carpmosia.PersonalLight;
using Content.Shared.Actions;
using Content.Shared.Inventory;
using Content.Shared.Inventory.Events;
using Content.Shared.Overlays;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;

namespace Content.Client._Carpmosia.Overlays;

public sealed partial class NightVisionOverlaySystem : EquipmentHudSystem<NightVisionOverlayComponent>
{
    [Dependency] private readonly IOverlayManager _overlayMan = default!;
    [Dependency] private readonly PersonalLightSystem _personalLight = default!;

    private NightVisionOverlay? _overlay;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<NightVisionOverlayComponent, ToggleNightVisionActionEvent>(OnToggled);
    }

    public void OnToggled(EntityUid uid, NightVisionOverlayComponent component, ToggleNightVisionActionEvent args)
    {
        if (args.Handled || !TryComp<PersonalLightComponent>(uid, out var persLight))
            return;

        component.Toggled = !component.Toggled;

        _personalLight.ToggleLight(uid, persLight, component.Toggled);

        args.Handled = true;
    }

    protected override void UpdateInternal(RefreshEquipmentHudEvent<NightVisionOverlayComponent> component)
    {
        base.UpdateInternal(component);

        if (component.Components.Count > 0)
        {
            _overlay = new NightVisionOverlay(component.Components[0]);
            _overlayMan.AddOverlay(_overlay);
        }
    }

    protected override void DeactivateInternal()
    {
        base.DeactivateInternal();

        if (_overlay != null)
            _overlayMan.RemoveOverlay(_overlay);
        _overlay = null;
    }
}
