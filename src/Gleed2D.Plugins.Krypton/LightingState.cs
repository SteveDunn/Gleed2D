using System ;
using System.IO ;
using System.Windows.Forms;
using System.Xml.Linq ;
using Gleed2D.Core ;
using StructureMap ;

namespace Gleed2D.Plugins.Krypton
{
	public class LightingState : IDisposable, ILightingState
	{
		readonly static string _path = @"{0}\lighting.state".FormatWith(Application.StartupPath) ;

		public static ILightingState FromDiskOrDefault
		{
			get
			{
				var disk = ObjectFactory.GetInstance<IDisk>( ) ;

				try
				{
					if (disk.FileExists(_path))
					{
						XElement x = XElement.Load(_path);
						return x.DeserializedAs<LightingState>();
					}
				}
				catch (IOException)
				{
				}

				return new LightingState(  );
			}
		}

		public bool LightingOn
		{
			get;
			set ;
		}

		public void Dispose(  )
		{
			serialiseToDisk( ) ;
		}

		void serialiseToDisk( )
		{
			this.SerializeToXml( ).Save( _path ) ;
		}

		~LightingState()
		{
			serialiseToDisk( );
		}
	}
}