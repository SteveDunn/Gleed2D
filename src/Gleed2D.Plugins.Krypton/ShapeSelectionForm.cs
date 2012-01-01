using System;
using System.Collections ;
using System.ComponentModel ;
using System.Drawing ;
using System.Drawing.Design ;
using System.Windows.Forms;
using Gleed2D.Core ;
using Gleed2D.Core.Controls ;
using Microsoft.Xna.Framework ;
using StructureMap ;

namespace Gleed2D.Plugins.Krypton
{
	public partial class ShapeSelectionForm : Form
	{
		readonly Action<Vector2[ ]> _whenShapeChanges ;
		static readonly PropertyValueUIItemInvokeHandler _uiItemNullHandler = delegate
		                                                                      	{
		                                                                      	} ;
		static readonly Image _uiItemErrorImage = SystemIcons.Error.ToBitmap( ) ;
		
		IShapeProvider _currentShape ;
		GlyphService _glyphService ;

		public ShapeSelectionForm(Action<Vector2[]> whenShapeChanges )
		{
			Action=DialogResult.Cancel;
			
			_whenShapeChanges = whenShapeChanges ;
			
			InitializeComponent();
		}

		public DialogResult Action
		{
			get ;
			private set ;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Vertices vertices = Vertices.CreateGear( 100, 10, 2, 20 ) ;

			_whenShapeChanges( vertices.ToArray( ) ) ;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Vertices vertices = Vertices.CreateCapsule( 250, 25, 6 ) ;

			_whenShapeChanges( vertices.ToArray( ) ) ;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Vertices vertices = Vertices.CreateEllipse( 250, 150, 15 ) ;

			_whenShapeChanges( vertices.ToArray( ) ) ;
		}

		private void ShapeSelectionForm_Load(object sender, EventArgs e)
		{
			var extensibility = ObjectFactory.GetInstance<IExtensibility>( ) ;
			
			extensibility.ShapeProviders.ForEach(
				sp => uiShapeList.Items.Add(
					new ListViewItem( sp.Name )
						{
							Tag = sp 
						} ) ) ;

			uiShapeList.View=View.List;

			uiShapePropertyGrid.PropertyValueChanged += ( s, args ) => _whenShapeChanges( _currentShape.Vertices ) ;

			var site = new SimpleSite
				{
					Name = "GridSite",
					Component = uiShapePropertyGrid,
				} ;

			_glyphService = new GlyphService( ) ;

			_glyphService.QueryPropertyUIValueItems += verifyDataErrorInfo ;

			site.AddService<IPropertyValueUIService>( _glyphService ) ;

			uiShapePropertyGrid.Site = site ;

			uiShapeList.SelectedItems.Clear(  );
			uiShapeList.MultiSelect = false ;
			uiShapeList.Items[ 0 ].Selected = true ;
		}

		static void verifyDataErrorInfo(
			ITypeDescriptorContext context, 
			PropertyDescriptor propDesc, 
			ArrayList valueUiItemList )
		{
			IDataErrorInfo errInfo = context == null ? null : context.Instance as IDataErrorInfo ;

			string propName = propDesc == null ? null : propDesc.Name ;

			if( errInfo != null && !string.IsNullOrEmpty( propName ) )
			{
				string errMsg = errInfo[ propName ] ;

				if( !string.IsNullOrEmpty( errMsg ) )
				{
					valueUiItemList.Add( new PropertyValueUIItem( _uiItemErrorImage, _uiItemNullHandler, errMsg ) ) ;
				}
			}
		}

		private void uiApplyButton_Click(object sender, EventArgs e)
		{
			Action=DialogResult.OK;
			Close( ) ;
		}

		private void uiCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void uiShapeList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if( uiShapeList.SelectedItems.Count == 0 )
			{
				return ;
			}
			
			_currentShape = (IShapeProvider) uiShapeList.SelectedItems[0].Tag ;
			
			uiShapePropertyGrid.SelectedObject = _currentShape.Variables ;

			_whenShapeChanges( _currentShape.Vertices ) ;
		}
	}
}
