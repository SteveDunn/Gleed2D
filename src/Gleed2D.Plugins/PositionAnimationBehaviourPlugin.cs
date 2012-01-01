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
	public class BehaviourDragDropHandler : IHandleDragDrop
	{
		readonly Func<ItemEditor, IBehaviour> _buildBehaviour ;

		public BehaviourDragDropHandler(Func<ItemEditor, IBehaviour> buildBehaviour  )
		{
			_buildBehaviour = buildBehaviour ;
		}

		public void WhenDroppedOntoEditor( IEditor editor )
		{
			ItemEditor itemEditor = editor.ItemUnderMouse;

			if (itemEditor == null)
			{
				return;
			}

			IoC.Model.AttachBehaviour(itemEditor, _buildBehaviour(itemEditor));
		}

		public void WhenBeingDraggedOverEditor( IEditor editor, DragEventArgs dragEventArgs )
		{
			ItemEditor itemEditor = editor.ItemUnderMouse;

			if (itemEditor == null)
			{
				dragEventArgs.Effect = DragDropEffects.None;
			}

			dragEventArgs.Effect = DragDropEffects.Link;
		}

		public void WhenEnteringEditor( DragEventArgs dragEventArgs )
		{
		}

		public void WhenLeavingEditor( DragEventArgs dragEventArgs )
		{
		}

		public DragDropEffects DragDropEffects
		{
			get { return DragDropEffects.Link;}
		}
	}

	[Export( typeof( IBehaviourPlugin ) )]
	public class PositionAnimationBehaviourPlugin : IBehaviourPlugin
	{
		readonly BehaviourDragDropHandler _dragDropBehaviour ;

		public PositionAnimationBehaviourPlugin( )
		{
			_dragDropBehaviour=new BehaviourDragDropHandler( buildBehaviour );
		}

		IBehaviour buildBehaviour( ItemEditor forItem )
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

		public IHandleDragDrop DragDropHandler
		{
			get
			{
				return _dragDropBehaviour ;
			}
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