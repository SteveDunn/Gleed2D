using System ;
using System.ComponentModel.Composition ;
using System.Reflection ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using Gleed2D.Core.Behaviour ;
using Gleed2D.Core.Controls ;
using Gleed2D.InGame ;
using Gleed2D.InGame.Interpolation ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.Plugins
{
	[Export( typeof( IBehaviourPlugin ) )]
	public class PositionAnimationBehaviourPlugin : IBehaviourPlugin
	{
		IBehaviour buildBehaviour( ITreeItem forItem )
		{
			ItemProperties itemProperties = forItem.ItemProperties;

			var interpolationBehaviour = new PositionAnimationBehaviour(
				itemProperties,
				new PositionAnimationBehaviourProperties
					{
						DurationInSeconds = .75f,
						Easing = Easing.EaseInOut,
						From = itemProperties.Position,
						To = Vector2.Zero,
						Name = @"movement_animation_{0}".FormatWith(IoC.Model.NextItemNumber),
						NameOfPropertyToModify = @"Position",
						ClrTypeOfInterpolator = typeof(Quartic).AssemblyQualifiedName,
					});


			return interpolationBehaviour ;
		}

		public ImageProperties Icon
		{
			get
			{
				return Images.SummonImage(
					Assembly.GetExecutingAssembly( ), @"Gleed2D.Plugins.Resources.position_animation_behaviour_icon.png" ) ;
			}
		}

		public IHandleDragDrop CreateDragDropHandler(IEntityCreationProperties entityCreationProperties)
		{
			return new BehaviourDragDropHandler( buildBehaviour );
		}

		public string CategoryName
		{
			get
			{
				return @"Behaviour/Position animation" ;
			}
		}

		public Control ControlForAboutBox
		{
			get
			{
				return new PluginDescriptionControl( this ) ;
			}
		}

		public void InitialiseInUi( IMainForm mainForm )
		{
		}

		public string Name
		{
			get
			{
				return @"Position animation" ;
			}
		}
	}
}