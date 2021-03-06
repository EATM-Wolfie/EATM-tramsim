using OpenBveApi.Colors;

namespace RouteManager2.Climate
{
	/// <summary>Defines a region of fog</summary>
	public struct Fog
	{
		/// <summary>The offset at which the fog starts</summary>
		public float Start;
		
		/// <summary>The offset at which the fog ends</summary>
		public float End;
		
		/// <summary>The color of the fog</summary>
		public Color24 Color;
		
		/// <summary>The track position at which the fog is placed</summary>
		public double TrackPosition;

		/// <summary>The fog density value</summary>
		public float Density;

		/// <summary>Stores whether the fog is linear</summary>
		public bool IsLinear;

		/// <summary>Creates a new region of fog</summary>
		public Fog(float Start, float End, Color24 Color, double TrackPosition, bool IsLinear = true, float Density = 0.0f)
		{
			this.Start = Start;
			this.End = End;
			this.Color = Color;
			this.TrackPosition = TrackPosition;
			this.Density = Density;
			this.IsLinear = IsLinear;
		}
	}
}
