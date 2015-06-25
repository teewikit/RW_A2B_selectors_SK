using System;
using RimWorld;
using Verse;
using A2B;

namespace A2B_Selector
{
	public class Soft : BeltSelectorComponent
    {
        public override IntVec3 GetDestinationForThing(Thing thing)
        {
            // Test the 'selection' idea ...
            ISlotGroupParent slotParent = parent as ISlotGroupParent;
            if (slotParent == null)
            {
                throw new InvalidOperationException("parent is not a SlotGroupParent!");
            }
            
            var selectionSettings = slotParent.GetStoreSettings();
			if( ( selectionSettings.AllowedToAccept(thing) )&&( IsFreeBelt(this.GetPositionFromRelativeRotation(Rot4.North)) ) )
                return this.GetPositionFromRelativeRotation(Rot4.North);

            // A list of destinations - indexing modulo 2 lets us cycle them and avoid
            // long chains of if-statements.
            IntVec3[] dests = {
                this.GetPositionFromRelativeRotation(Rot4.West),
                this.GetPositionFromRelativeRotation(Rot4.East)
            };

            // Determine where we are going in the destination list (and default to left)
            int index = Math.Max(0, Array.FindIndex(dests, dir => (dir == _splitterDest)));

            // Do we have a new item ?
            if (_mythingID == thing.ThingID && IsFreeBelt(_splitterDest))
            {
                return _splitterDest;
            }
            else
            {
                _mythingID = thing.ThingID;

                // Try the next destination
                index = (index + 1) % 2;
                if (IsFreeBelt(dests[index]))
                {
                    _splitterDest = dests[index];
                    return _splitterDest;
                }

                // Try the one after that
                index = (index + 1) % 2;
                if (IsFreeBelt(dests[index]))
                {
                    _splitterDest = dests[index];
                    return _splitterDest;
                }

                // Give up and use our current destination
                return _splitterDest;
            }
        }

	}
}