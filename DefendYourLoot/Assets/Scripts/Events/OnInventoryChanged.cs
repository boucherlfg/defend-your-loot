using System.Collections.Generic;
using Level;
using Managers;
using UI;

namespace Events
{
    public class OnInventoryChanged : BaseEvent<List<PlaceableItem>> {}
}