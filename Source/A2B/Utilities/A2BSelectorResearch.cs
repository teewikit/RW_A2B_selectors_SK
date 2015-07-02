using Verse;
using A2B;
using System;

namespace A2B_Selector {

	public static class A2BSelectorData {

		private static A2BSelectorDataDef	_def;

		public static Version               RequiredVersion;

		public static A2BSelectorDataDef def
		{
			get {
				if( _def == null )
					_def = DefDatabase<A2BSelectorDataDef>.GetNamed("A2BSelector");
				if( _def == null )
					_def = DefDatabase<A2BSelectorDataDef>.GetRandom();
				return _def;
			}
		}

		static A2BSelectorData() {
			if( !A2BData.IsReady )
			{
				Log.ErrorOnce( "A2B Core not ready!", 0 );
				return;
			}
			if (def == null) {
				Log.ErrorOnce( "A2B_Selector - Unable to load selector data!", 0 );
				return;
			}

			RequiredVersion = new Version(def.RequiredVersion);
			A2BData.CheckVersion(RequiredVersion);

			Log.Message( "A2B_Selector initialized" );
		}

		public static bool IsReady
		{
			get { return def != null; }
		}

	}

}