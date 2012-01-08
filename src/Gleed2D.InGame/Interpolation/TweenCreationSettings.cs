using System ;
using System.Xml.Linq ;

namespace Gleed2D.InGame.Interpolation
{
	public class TweenCreationSettings_old 
	{
		public XElement ToXml()
		{
			return new XElement(@"TweenCreationSettings",
				new XElement(@"Type", Type.AssemblyQualifiedName),
				new XElement(@"From", From),
				new XElement(@"To", To),
				new XElement(@"Easing", Easing),
				new XElement(@"DurationInMilliseconds", Duration.TotalMilliseconds) ) ;
		}

		public TweenCreationSettings_old( XElement xml )
		{
			xml = xml.CertainElement( @"TweenCreationSettings" ) ;
			
			Type = Type.GetType( xml.CertainElement( @"Type" ).Value ) ;
			From = (float)xml.CertainElement( @"From" );
			To = (float)xml.CertainElement( @"To" );
			Easing = xml.CertainElement( @"Easing" ).Value.AsEnum<Easing>();
			Duration = TimeSpan.FromMilliseconds((int)xml.CertainElement( @"DurationInMilliseconds" )) ;
		}

		public TweenCreationSettings_old( Type type, Easing easing, float from, float to, TimeSpan duration )
		{
			From = @from ;
			To = to ;
			Type = type ;
			Easing = easing ;
			Duration = duration ;
		}

		public float From
		{
			get ;
			set ;
		}

		public float To
		{
			get ;
			set ;
		}

		public Type Type
		{
			get ;
			set ;
		}

		public Easing Easing
		{
			get ;
			set ;
		}

		public TimeSpan Duration
		{
			get ;
			set ;
		}
	}
}