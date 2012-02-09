using System ;
using System.ComponentModel.Composition ;
using System.Reflection ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using Gleed2D.Core.Behaviour ;
using Gleed2D.Core.Controls ;
using Gleed2D.InGame ;
using Gleed2D.InGame.Interpolation ;

namespace Gleed2D.Plugins
{
	[Export( typeof( IBehaviourPlugin ) )]
	public class ScaleAnimationBehaviourPlugin : IBehaviourPlugin
	{
		public ImageProperties Icon
		{
			get
			{
				return Images.SummonImage(
					Assembly.GetExecutingAssembly( ), @"Gleed2D.Plugins.Resources.scale_animation_behaviour_icon.png" ) ;
			}
		}

		public IHandleDragDrop CreateDragDropHandler(IEntityCreationProperties entityCreationProperties)
		{
			return new BehaviourDragDropHandler(buildBehaviour);
		}

		IBehaviour buildBehaviour(ITreeItem forEditor)
		{
			ItemProperties itemProperties = forEditor.ItemProperties ;

			var behaviour = new ScaleAnimationBehaviour(
				itemProperties,
				new FloatAnimationBehaviourProperties
					{
						DurationInSeconds = .75f,
						Easing = Easing.EaseInOut,
						From = 1,
						To = 2,
						Name = @"scale_animation_{0}".FormatWith( IoC.Model.NextItemNumber ),
						NameOfPropertyToModify = @"Scale",
						ClrTypeOfInterpolator = typeof( Quartic ).AssemblyQualifiedName
					} ) ;

			return behaviour ;
		}

		public string CategoryName
		{
			get
			{
				return @"Behaviour/Scale animation" ;
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
				return @"Scale animation" ;
			}
		}
	}
}