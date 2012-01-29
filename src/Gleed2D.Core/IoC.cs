using StructureMap ;

namespace Gleed2D.Core
{
	public static class IoC
	{
		public static ICanvas Canvas
		{
			get
			{
				return ObjectFactory.GetInstance<ICanvas>( ) ;
			}
		}

		public static IMemento Memento
		{
			get
			{
				return ObjectFactory.GetInstance<IMemento>( ) ;
			}
		}

		public static IMainForm MainForm
		{
			get
			{
				return ObjectFactory.GetInstance<IMainForm>( ) ;
			}
		}

		public static IModel Model
		{
			get
			{
				return ObjectFactory.GetInstance<IModel>( ) ;
			}
		}

		public static void Register< T >( T instance )
		{
			ObjectFactory.Configure( i=> i.For<T>( ).Use( instance ) );
		}
	}
}