﻿using System;
using RimWorld;
using Verse;
using A2B;

namespace A2B_Selector
{
	public class Original_Left : BeltSelectorAddon
    {
		public override bool CanAcceptFrom(Rot4 direction)
		{
			return ( direction == Rot4.South )||( direction == Rot4.East );
		}

        public override IntVec3 GetDestinationForThing(Thing thing)
        {
            // Test the 'selection' idea ...
            ISlotGroupParent slotParent = parent as ISlotGroupParent;
            if (slotParent == null)
            {
                throw new InvalidOperationException("parent is not a SlotGroupParent!");
            }
            
			// Send north
            var selectionSettings = slotParent.GetStoreSettings();
            if (selectionSettings.AllowedToAccept(thing))
                return this.GetPositionFromRelativeRotation(Rot4.West);

            // else, send east
			return this.GetPositionFromRelativeRotation(Rot4.North);
        }
	}
}