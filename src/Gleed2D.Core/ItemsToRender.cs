using System ;

namespace Gleed2D.Core
{
	[Flags]
	public enum ItemsToRender
	{
		HooksOnSelectedItems = 1 << 0,
		SelectionRectangle = 1 << 1,
		SnapPoint = 1 << 2,
		WorldOrigin = 1 <<3 ,
		Grid = 1 << 4,
		Items = 1 << 5,
		Everything=int.MaxValue,
	}
}