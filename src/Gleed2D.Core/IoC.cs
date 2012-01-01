using StructureMap ;

namespace Gleed2D.Core
{
	public static class IoC
	{
		public static IEditor Editor
		{
			get
			{
				return ObjectFactory.GetInstance<IEditor>( ) ;
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