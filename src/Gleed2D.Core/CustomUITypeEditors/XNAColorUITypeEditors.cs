using System ;
using System.ComponentModel ;
using System.Drawing ;
using System.Drawing.Design ;
using System.Globalization;
using System.Reflection ;
using System.Windows.Forms ;
using System.Windows.Forms.Design ;
using Color = Microsoft.Xna.Framework.Color ;

namespace Gleed2D.Core.CustomUITypeEditors
{
	/// <summary>
	/// Class extending the <see cref="ColorEditor"/> which adds the capability to change the 
	/// alpha value of the color. For use on a property of type: Microsoft.Xna.Framework.Graphics.Color.
	/// </summary>
	public class XnaColorUiTypeEditor : ColorEditor
	{
		private ColorUiWrapper _colorUi ;

		/// <summary>
		/// Wrapper for the private ColorUI class nested within <see cref="ColorEditor"/>.
		/// It publishes its internals via reflection and adds a <see cref="TrackBar"/> to
		/// adjust the alpha value.
		/// </summary>
		class ColorUiWrapper
		{
			private readonly Control _control ;
			private readonly MethodInfo _startMethodInfo ;
			private readonly MethodInfo _endMethodInfo ;
			private readonly PropertyInfo _valuePropertyInfo ;
			private readonly TrackBar _tbAlpha ;
			private readonly Label _lblAlpha ;
			private bool _inSizeChange ;

			/// <summary>
			/// Creates a new instance.
			/// </summary>
			/// <param name="colorEditor">The editor this instance belongs to.</param>
			public ColorUiWrapper( XnaColorUiTypeEditor colorEditor )
			{
				Type colorUiType = typeof( ColorEditor ).GetNestedType(
					"ColorUI", BindingFlags.CreateInstance | BindingFlags.NonPublic ) ;
				ConstructorInfo constructorInfo = colorUiType.GetConstructor(
					new[ ]
						{
							typeof( ColorEditor )
						} ) ;
				_control = (Control) constructorInfo.Invoke(
					new object[ ]
						{
							colorEditor
						} ) ;

				_control.BackColor = SystemColors.Control ;

				var alphaPanel = new Panel
					{
						BackColor = SystemColors.Control,
						Dock = DockStyle.Right,
						Width = 28
					} ;
				_control.Controls.Add( alphaPanel ) ;

				_tbAlpha = new TrackBar
					{
						Orientation = Orientation.Vertical,
						Dock = DockStyle.Fill,
						TickStyle = TickStyle.None,
						Maximum = byte.MaxValue,
						Minimum = byte.MinValue
					} ;

				_tbAlpha.ValueChanged += onTrackBarAlphaValueChanged ;

				alphaPanel.Controls.Add( _tbAlpha ) ;

				_lblAlpha = new Label
					{
						Text = @"0",
						Dock = DockStyle.Bottom,
						TextAlign = ContentAlignment.MiddleCenter
					} ;

				alphaPanel.Controls.Add( _lblAlpha ) ;

				Type type = _control.GetType( ) ;

				_startMethodInfo = type.GetMethod( "Start" ) ;
				_endMethodInfo = type.GetMethod( "End" ) ;
				_valuePropertyInfo = type.GetProperty( "Value" ) ;

				_control.SizeChanged += onControlSizeChanged ;
			}

			/// <summary>
			/// The control to be shown when a color is edited.
			/// The concrete type is ColorUI which is privately hidden
			/// within System.Drawing.Design.
			/// </summary>
			public Control Control
			{
				get
				{
					return _control ;
				}
			}

			/// <summary>
			/// Gets the edited color with applied alpha value.
			/// </summary>
			public object Value
			{
				get
				{
					object result = _valuePropertyInfo.GetValue( _control, new object[ 0 ] ) ;
					if( result is System.Drawing.Color )
						result = System.Drawing.Color.FromArgb( _tbAlpha.Value, (System.Drawing.Color) result ) ;
					return result ;
				}
			}

			public void Start( IWindowsFormsEditorService service, object value )
			{
				if( value is System.Drawing.Color )
					_tbAlpha.Value = ( (System.Drawing.Color) value ).A ;
				_startMethodInfo.Invoke(
					_control,
					new[ ]
						{
							service, value
						} ) ;
			}

			public void End( )
			{
				_endMethodInfo.Invoke( _control, new object[ 0 ] ) ;
			}

			private void onControlSizeChanged( object sender, EventArgs e )
			{
				if( _inSizeChange )
					return ;
				try
				{
					_inSizeChange = true ;
					var tabControl = (TabControl) _control.Controls[ 0 ] ;
					
					Size size = tabControl.TabPages[ 0 ].Controls[ 0 ].Size ;

					_control.Size = new Size(
						_tbAlpha.Width + size.Width, size.Height + tabControl.GetTabRect( 0 ).Height ) ;
				}
				finally
				{
					_inSizeChange = false ;
				}
			}

			private void onTrackBarAlphaValueChanged( object sender, EventArgs e )
			{
				_lblAlpha.Text = _tbAlpha.Value.ToString(CultureInfo.InvariantCulture) ;
			}
		}

		/// <summary>
		/// Edits the given value.
		/// </summary>
		/// <param name="context">Context infromation.</param>
		/// <param name="provider">Service provider.</param>
		/// <param name="value">Value to be edited.</param>
		/// <returns>An edited value.</returns>
		public override object EditValue( ITypeDescriptorContext context, IServiceProvider provider, object value )
		{
			if( provider != null )
			{
				var service = (IWindowsFormsEditorService) provider.GetService( typeof( IWindowsFormsEditorService ) ) ;

				if( service == null )
				{
					return value ;
				}

				if( _colorUi == null )
				{
					_colorUi = new ColorUiWrapper( this ) ;
				}

				var xnaColor = (Color) value ;

				_colorUi.Start( service, System.Drawing.Color.FromArgb( xnaColor.A, xnaColor.R, xnaColor.G, xnaColor.B ) ) ;

				service.DropDownControl( _colorUi.Control ) ;

				if( ( _colorUi.Value != null ) )
				{
					var rescolor = (System.Drawing.Color) _colorUi.Value ;

					value = new Color( rescolor.R, rescolor.G, rescolor.B, rescolor.A ) ;
				}

				_colorUi.End( ) ;
			}

			return value ;
		}

		public override void PaintValue( PaintValueEventArgs e )
		{
			if( e.Value is Color && ( (Color) e.Value ).A <= byte.MaxValue )
			{
				var xnacolor = (Color) e.Value ;
				System.Drawing.Color syscolor = System.Drawing.Color.FromArgb( xnacolor.A, xnacolor.R, xnacolor.G, xnacolor.B ) ;

				Graphics graphics = e.Graphics ;

				using( var brush = new SolidBrush( System.Drawing.Color.DarkGray ) )
				{
					graphics.FillRectangle( brush, new Rectangle( e.Bounds.X + 1, e.Bounds.Y + 1, 4, 4 ) ) ;
					graphics.FillRectangle( brush, new Rectangle( e.Bounds.X + 9, e.Bounds.Y + 1, 4, 4 ) ) ;
					graphics.FillRectangle( brush, new Rectangle( e.Bounds.X + 17, e.Bounds.Y + 1, 2, 4 ) ) ;

					graphics.FillRectangle( brush, new Rectangle( e.Bounds.X + 5, e.Bounds.Y + 5, 4, 4 ) ) ;
					graphics.FillRectangle( brush, new Rectangle( e.Bounds.X + 13, e.Bounds.Y + 5, 4, 4 ) ) ;

					graphics.FillRectangle( brush, new Rectangle( e.Bounds.X + 1, e.Bounds.Y + 9, 4, 3 ) ) ;
					graphics.FillRectangle( brush, new Rectangle( e.Bounds.X + 9, e.Bounds.Y + 9, 4, 3 ) ) ;
					graphics.FillRectangle( brush, new Rectangle( e.Bounds.X + 17, e.Bounds.Y + 9, 2, 3 ) ) ;

				}
				using( var brush = new SolidBrush( syscolor ) )
				{
					graphics.FillRectangle( brush, new Rectangle( e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height - 1 ) ) ;
				}
			}

			if( e.Value is System.Drawing.Color )
			{
				base.PaintValue( e ) ;
			}
		}
	}
}
