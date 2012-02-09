using System.Windows.Forms ;
using Gleed2D.Core.UserActions ;
using StructureMap ;

namespace Gleed2D.Core
{
	public class HandleKeyboardCommands : IHandleKeyboardCommands
	{
		IMainForm _mainForm;

		public void HandleKeyDown(KeyEventArgs args)
		{
			if (args.KeyCode == Keys.N)
			{
				ObjectFactory.GetInstance<IHandleUserActions>().ProcessAction(new AddNewLayerAction());
			}

			if (args.KeyCode == Keys.F7)
			{

				ObjectFactory.GetInstance<IHandleUserActions>().ProcessAction(new MoveItemUpAction(summonMainForm(  ).LevelExplorer.SelectedEntity));
			}
			
			if (args.KeyCode == Keys.F8)
			{
				ObjectFactory.GetInstance<IHandleUserActions>().ProcessAction(new MoveItemDownAction(summonMainForm(  ).LevelExplorer.SelectedEntity));
			}
			
			if (args.KeyCode == Keys.F4 && !args.Control)
			{
				ObjectFactory.GetInstance<IHandleUserActions>().ProcessAction(new CenterViewAction(summonMainForm(  ).LevelExplorer.SelectedEntity));
			}
			
			if (args.KeyCode == Keys.F2)
			{
				ObjectFactory.GetInstance<IHandleUserActions>().ProcessAction(new RenameInPlaceAction());
			}
			
			if (args.KeyCode == Keys.D && args.Control)
			{
				ObjectFactory.GetInstance<IHandleUserActions>().ProcessAction(new DuplicateLayerAction(summonMainForm().LevelExplorer.SelectedEntity as LayerEditor));
			}
		}

		IMainForm summonMainForm()
		{
			if (_mainForm == null)
			{
				_mainForm = ObjectFactory.GetInstance<IMainForm>();
			}

			return _mainForm;
		}

	}
}