using Content.Shared.Actions;
using Content.Shared.Inventory;

namespace Content.Shared._Carpmosia.Overlays;

public partial class NightVisionOverlaySystem : EntitySystem
{
    [Dependency] private readonly InventorySystem _inventorySystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<NightVisionOverlayComponent, GetItemActionsEvent>(OnGetActions);
    }

    private void OnGetActions(EntityUid uid, NightVisionOverlayComponent component, GetItemActionsEvent args)
    {
        args.AddAction(ref component.ToggleActionEntity, component.ToggleAction);
        Dirty(uid, component);
    }
}
