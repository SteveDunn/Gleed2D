using System.Windows.Forms;

namespace Gleed2D.Core
{
	public interface IHandleKeyboardCommands
	{
		void HandleKeyDown(KeyEventArgs args);
	}
}