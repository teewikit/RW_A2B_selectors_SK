using System;
using RimWorld;
using Verse;
using A2B;

namespace A2B_Selector
{
    public class Original_Right : BeltComponent
    {

        private Rot4 nextDest = Rot4.North;
        private bool hasStorageSettings;
//        private string _mythingID;
//        private IntVec3 _splitterDest;

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.LookValue<bool>(ref hasStorageSettings, "hasStorageSettings");
        }

        public override void PostSpawnSetup()
        {
            base.PostSpawnSetup();

            SlotGroupParent slotParent = parent as SlotGroupParent;
            if (slotParent == null)
            {
                throw new InvalidOperationException("parent is not a SlotGroupParent!");
            }

            // we kinda want to not overwrite custom storage settings every save/load...
            if (!hasStorageSettings)
                slotParent.GetStoreSettings().allowances.DisallowAll();

            hasStorageSettings = true;
        }

		public override bool CanAcceptFrom(Rot4 direction)
		{
			return ( direction == Rot4.South )||( direction == Rot4.West );
		}

        public override IntVec3 GetDestinationForThing(Thing thing)
        {
            // Test the 'selection' idea ...
            SlotGroupParent slotParent = parent as SlotGroupParent;
            if (slotParent == null)
            {
                throw new InvalidOperationException("parent is not a SlotGroupParent!");
            }
            
			// Send north
            var selectionSettings = slotParent.GetStoreSettings();
            if (selectionSettings.AllowedToAccept(thing))
                return this.GetPositionFromRelativeRotation(Rot4.East);

            // else, send east
			return this.GetPositionFromRelativeRotation(Rot4.North);
        }

        private bool IsFreeBelt(IntVec3 position)
        {
            BeltComponent destBelt = position.GetBeltComponent();
            return (destBelt != null && destBelt.CanAcceptFrom(this));
        }

    }
}