using System;
using System.ComponentModel ;
using System.Diagnostics ;
using System.Drawing.Design ;
using System.Windows.Forms;
using GLEED2D.Forms ;
using Gleed2D.Core ;
using Gleed2D.Core.CustomUITypeEditors ;
using Gleed2D.InGame ;
using Microsoft.Xna.Framework ;
using StructureMap ;

namespace GLEED2D
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				Application.EnableVisualStyles( ) ;

				ObjectFactory.Configure(
					a =>
						{
							a.For<IDisk>( ).Singleton( ).Use<Disk>( ) ;
							a.For<IMenuItems>( ).Singleton( ).Use<MainFormMenuItems>( ) ;
							a.For<IGleedRenderer>( ).Singleton( ).Use<GleedRenderer>( ) ;
							a.For<IMemento>( ).Singleton( ).Use<Mememto>( ) ;
							a.For<IGetAssemblyInformation>( ).Singleton( ).Use<GetAssemblyInformation>( ) ;
							a.For<IModel>( ).Singleton( ).Use<Model>( ) ;
							a.For<IHandleUserActions>( ).Singleton( ).Use<HandleUserActions>( ) ;
							a.For<IHandleKeyboardCommands>( ).Singleton( ).Use<HandleKeyboardCommands>( ) ;
							a.For<IImageRepository>( ).Singleton( ).Use<ImageRepository>( ).Named( @"iconImages" ) ;
							a.For<IImageRepository>( ).Singleton( ).Use<ImageRepository>( ) ;
							a.For<ILoadImages>( ).Singleton( ).Use<LoadImages>( ) ;
							a.For<IExtensibility>( ).Singleton( ).Use<Extensibility>( ) ;
							a.For<IGame>( ).Singleton( ).Use<XnaGame>( ) ;
							a.For<IMainForm>( ).Singleton( ).Use<MainForm>( ) ;
							a.For<IEditor>( ).Singleton( ).Use<Editor>( ) ;
							a.For<ITextureStore>( ).Singleton( ).Use<TextureStore>( ) ;
							a.For<IDrawing>( ).Singleton( ).Use<Drawing>( ) ;
						} ) ;

				TypeDescriptor.AddAttributes(
					typeof(Color),
					new EditorAttribute(
						typeof(XnaColorUiTypeEditor),
						typeof(UITypeEditor)));

				TypeDescriptor.AddAttributes(
					typeof( PathToFolder ),
					new EditorAttribute(
						typeof( PathToFolderUiTypeEditor ),
						typeof( UITypeEditor ) ) ) ;

				TypeDescriptor.AddAttributes(
					typeof( PathToFolder ),
					new TypeConverterAttribute(
						typeof( PathToFolderConverter ) ) ) ;

				TypeDescriptor.AddAttributes(
					typeof( PathToFile ),
					new EditorAttribute(
						typeof( PathToFileUiTypeEditor ),
						typeof( UITypeEditor ) ) ) ;

				TypeDescriptor.AddAttributes(
					typeof( LinkedItem ),
					new EditorAttribute(
						typeof( LinkedItemTypeConverter ),
						typeof( UITypeEditor ) ) ) ;

				TypeDescriptor.AddAttributes(
					typeof( PathToFile ),
					new TypeConverterAttribute(
						typeof( PathToFileConverter ) ) ) ;

				var form = ObjectFactory.GetInstance<IMainForm>( ) ;

				form.Show( ) ;

				runForever( ) ;
			}
			catch (Exception e)
			{
				reportError(e);
			}
			finally
			{
				Logger.Instance.log("Application ended.");
			}
		}

		static void runForever( )
		{
			var game = ObjectFactory.GetInstance<IGame>( ) ;
			//while (!game.HasExited)
			{
				try
				{
					game.Run( ) ;
				}
				catch( Exception e )
				{
					reportError( e, true ) ;
				}
				finally
				{
					Logger.Instance.log( "Application ended." ) ;
				}
			}
		}

		static void reportError(Exception e)
		{
			reportError( e, false ) ;
		}

		static void reportError(Exception e, bool @continue)
		{
			Logger.Instance.log( string.Format( @"Exception caught: 

 {0}

{1}", e.Message, e.StackTrace ) ) ;

			if( e.InnerException != null )
			{
				Logger.Instance.log( string.Format( "Inner Exception: {0}", e.InnerException.Message ) ) ;
			}

			if( !Debugger.IsAttached )
			{
				MessageBox.Show( @"An exception was caught. Application will end. Please check the file log.txt." ) ;
			}
			else
			{
				if( !@continue )
				{
					throw e ;
				}
			}
		}
	}
}