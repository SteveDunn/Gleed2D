using System ;
using System.Drawing ;
using System.Windows.Forms ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.Core.CustomUITypeEditors
{
	class Vector2EditorControl : UserControl
	{
		const float RADIUS = 65 ;
		const string FORMAT = "####0.###" ;
		const int PRECISION = 3 ;

		public Vector2 Value ;

		double _angle ;
		double _length ;
		Vector2 _center = new Vector2( 70, 70 ) ;

		public Vector2EditorControl( Vector2 initialValue )
		{
			InitializeComponent( ) ;
			Value = initialValue ;
			tbX.Text = Value.X.ToString( FORMAT ) ;
			tbY.Text = Value.Y.ToString( FORMAT ) ;
			onValueUpdated( ) ;
		}

		private void pictureBox1Paint( object sender, PaintEventArgs e )
		{
			Graphics graphics = e.Graphics ;

			graphics.DrawEllipse(
				new Pen( System.Drawing.Color.Black, 3 ), _center.X - RADIUS, _center.Y - RADIUS, 2 * RADIUS, 2 * RADIUS ) ;

			if( Value == Vector2.Zero )
			{
				return ;
			}

			//draw blue arc to visualize angle
			graphics.DrawLine( new Pen( System.Drawing.Color.Blue, 1 ), _center.X, _center.Y, _center.X + RADIUS, _center.Y ) ;

			graphics.DrawArc(
				new Pen( System.Drawing.Color.Blue, 1 ),
				_center.X - 20,
				_center.Y - 20,
				40,
				40,
				0,
				(int) MathHelper.ToDegrees( (float) _angle ) ) ;

			//draw black arrow
			Vector2 newpos = Vector2.Transform( Vector2.UnitX * RADIUS, Matrix.CreateRotationZ( (float) _angle ) ) ;
			Vector2 arrowpos1 = Vector2.Transform(
				( Vector2.UnitX + new Vector2( -0.3f, -0.1f ) ) * RADIUS, Matrix.CreateRotationZ( (float) _angle ) ) ;
			Vector2 arrowpos2 = Vector2.Transform(
				( Vector2.UnitX + new Vector2( -0.3f, +0.1f ) ) * RADIUS, Matrix.CreateRotationZ( (float) _angle ) ) ;
			graphics.DrawLine(
				new Pen( System.Drawing.Color.Black, 3 ), _center.X, _center.Y, _center.X + newpos.X, _center.Y + newpos.Y ) ;
			graphics.DrawLine(
				new Pen( System.Drawing.Color.Black, 3 ),
				_center.X + newpos.X,
				_center.Y + newpos.Y,
				_center.X + arrowpos1.X,
				_center.Y + arrowpos1.Y ) ;
			graphics.DrawLine(
				new Pen( System.Drawing.Color.Black, 3 ),
				_center.X + newpos.X,
				_center.Y + newpos.Y,
				_center.X + arrowpos2.X,
				_center.Y + arrowpos2.Y ) ;
		}

		private void onValueUpdated( )
		{
			_length = Value.Length( ) ;
			tbLength.Text = _length.ToString( FORMAT ) ;
			_angle = Math.Atan2( Value.Y, Value.X ) ;
			tbAngle.Text = MathHelper.ToDegrees( (float) _angle ).ToString( FORMAT ) ;
			pictureBox1.Invalidate( ) ;
		}

		private void onAngleOrLengthUpdated( )
		{
			Value.X = (float) Math.Round( Math.Cos( _angle ) * _length, PRECISION ) ;
			tbX.Text = Value.X.ToString( FORMAT ) ;
			Value.Y = (float) Math.Round( Math.Sin( _angle ) * _length, PRECISION ) ;
			tbY.Text = Value.Y.ToString( FORMAT ) ;
			pictureBox1.Invalidate( ) ;
		}

		void canvasMouseMove( object sender, MouseEventArgs e )
		{
			if( e.Button == MouseButtons.Left )
			{
				Vector2 mousepos = new Vector2( e.X, e.Y ) - _center ;
				_angle = Math.Atan2( mousepos.Y, mousepos.X ) ;
				onAngleOrLengthUpdated( ) ;
				onValueUpdated( ) ;
			}
		}

		//todo: TryParse
		private void tbX_KeyUp( object sender, KeyEventArgs e )
		{
			try
			{
				Value.X = (float) Convert.ToDouble( tbX.Text ) ;
				onValueUpdated( ) ;
			}
			catch
			{
			}
		}

		private void tbY_KeyUp( object sender, KeyEventArgs e )
		{
			try
			{
				Value.Y = (float) Convert.ToDouble( tbY.Text ) ;
				onValueUpdated( ) ;
			}
			catch
			{
			}
		}

		private void tbAngle_KeyUp( object sender, KeyEventArgs e )
		{
			try
			{
				_angle = MathHelper.ToRadians( (float) Convert.ToDouble( tbAngle.Text ) ) ;
				onAngleOrLengthUpdated( ) ;
			}
			catch
			{
			}
		}

		private void tbLength_KeyUp( object sender, KeyEventArgs e )
		{
			try
			{
				_length = Convert.ToDouble( tbLength.Text ) ;
				onAngleOrLengthUpdated( ) ;
			}
			catch
			{
			}
		}

		private void InitializeComponent( )
		{
			this.tbX = new System.Windows.Forms.TextBox( ) ;
			this.label1 = new System.Windows.Forms.Label( ) ;
			this.label2 = new System.Windows.Forms.Label( ) ;
			this.tbY = new System.Windows.Forms.TextBox( ) ;
			this.pictureBox1 = new System.Windows.Forms.PictureBox( ) ;
			this.tbLength = new System.Windows.Forms.TextBox( ) ;
			this.label3 = new System.Windows.Forms.Label( ) ;
			this.tbAngle = new System.Windows.Forms.TextBox( ) ;
			this.label5 = new System.Windows.Forms.Label( ) ;
			( (System.ComponentModel.ISupportInitialize) ( this.pictureBox1 ) ).BeginInit( ) ;
			this.SuspendLayout( ) ;
			// 
			// tbX
			// 
			this.tbX.Location = new System.Drawing.Point( 26, 6 ) ;
			this.tbX.Name = "tbX" ;
			this.tbX.Size = new System.Drawing.Size( 68, 20 ) ;
			this.tbX.TabIndex = 1 ;
			this.tbX.KeyUp += new System.Windows.Forms.KeyEventHandler( this.tbX_KeyUp ) ;
			// 
			// label1
			// 
			this.label1.AutoSize = true ;
			this.label1.Location = new System.Drawing.Point( 3, 9 ) ;
			this.label1.Name = "label1" ;
			this.label1.Size = new System.Drawing.Size( 17, 13 ) ;
			this.label1.TabIndex = 2 ;
			this.label1.Text = "X:" ;
			// 
			// label2
			// 
			this.label2.AutoSize = true ;
			this.label2.Location = new System.Drawing.Point( 129, 9 ) ;
			this.label2.Name = "label2" ;
			this.label2.Size = new System.Drawing.Size( 17, 13 ) ;
			this.label2.TabIndex = 4 ;
			this.label2.Text = "Y:" ;
			// 
			// tbY
			// 
			this.tbY.Location = new System.Drawing.Point( 152, 6 ) ;
			this.tbY.Name = "tbY" ;
			this.tbY.Size = new System.Drawing.Size( 68, 20 ) ;
			this.tbY.TabIndex = 2 ;
			this.tbY.KeyUp += new System.Windows.Forms.KeyEventHandler( this.tbY_KeyUp ) ;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point( 6, 35 ) ;
			this.pictureBox1.Name = "pictureBox1" ;
			this.pictureBox1.Size = new System.Drawing.Size( 140, 140 ) ;
			this.pictureBox1.TabIndex = 6 ;
			this.pictureBox1.TabStop = false ;
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler( this.canvasMouseMove ) ;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler( this.pictureBox1Paint ) ;
			// 
			// tbLength
			// 
			this.tbLength.Location = new System.Drawing.Point( 152, 155 ) ;
			this.tbLength.Name = "tbLength" ;
			this.tbLength.Size = new System.Drawing.Size( 68, 20 ) ;
			this.tbLength.TabIndex = 4 ;
			this.tbLength.KeyUp += new System.Windows.Forms.KeyEventHandler( this.tbLength_KeyUp ) ;
			// 
			// label3
			// 
			this.label3.AutoSize = true ;
			this.label3.Location = new System.Drawing.Point( 177, 137 ) ;
			this.label3.Name = "label3" ;
			this.label3.Size = new System.Drawing.Size( 43, 13 ) ;
			this.label3.TabIndex = 8 ;
			this.label3.Text = "Length:" ;
			// 
			// tbAngle
			// 
			this.tbAngle.Location = new System.Drawing.Point( 152, 107 ) ;
			this.tbAngle.Name = "tbAngle" ;
			this.tbAngle.Size = new System.Drawing.Size( 68, 20 ) ;
			this.tbAngle.TabIndex = 3 ;
			this.tbAngle.KeyUp += new System.Windows.Forms.KeyEventHandler( this.tbAngle_KeyUp ) ;
			// 
			// label5
			// 
			this.label5.AutoSize = true ;
			this.label5.Location = new System.Drawing.Point( 183, 89 ) ;
			this.label5.Name = "label5" ;
			this.label5.Size = new System.Drawing.Size( 37, 13 ) ;
			this.label5.TabIndex = 8 ;
			this.label5.Text = "Angle:" ;
			// 
			// Vector2EditorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F ) ;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font ;
			this.BackColor = System.Drawing.SystemColors.Control ;
			this.Controls.Add( this.tbAngle ) ;
			this.Controls.Add( this.label5 ) ;
			this.Controls.Add( this.label3 ) ;
			this.Controls.Add( this.tbLength ) ;
			this.Controls.Add( this.pictureBox1 ) ;
			this.Controls.Add( this.label2 ) ;
			this.Controls.Add( this.tbY ) ;
			this.Controls.Add( this.label1 ) ;
			this.Controls.Add( this.tbX ) ;
			this.Name = "Vector2EditorControl" ;
			this.Size = new System.Drawing.Size( 223, 180 ) ;
			( (System.ComponentModel.ISupportInitialize) ( this.pictureBox1 ) ).EndInit( ) ;
			this.ResumeLayout( false ) ;
			this.PerformLayout( ) ;

		}

		private System.Windows.Forms.TextBox tbX ;
		private System.Windows.Forms.Label label1 ;
		private System.Windows.Forms.Label label2 ;
		private System.Windows.Forms.TextBox tbY ;
		private System.Windows.Forms.PictureBox pictureBox1 ;
		private System.Windows.Forms.TextBox tbLength ;
		private System.Windows.Forms.Label label3 ;
		private System.Windows.Forms.TextBox tbAngle ;
		private System.Windows.Forms.Label label5 ;
	}
}