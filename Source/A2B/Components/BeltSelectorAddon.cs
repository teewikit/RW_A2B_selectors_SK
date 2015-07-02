using System;
using RimWorld;
using Verse;
using A2B;

namespace A2B_Selector
{

	public class BeltSelectorAddon : BeltSelectorComponent
	{
		
		private bool		VersionOk = false;

		public BeltSelectorAddon ()
		{
			VersionOk = A2BSelectorData.IsReady;
		}
	}
}

