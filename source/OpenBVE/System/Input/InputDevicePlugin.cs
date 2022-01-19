using OpenBveApi.Interface;

namespace OpenBve
{
	internal static partial class MainLoop
	{
		//
		// Fired when the external interface sends a key

		internal static void InputDevicePluginKeyDown(object sender, InputEventArgs e)
		{
			//System.Console.WriteLine("\r\n\r\n======================================================");
			//System.Console.WriteLine("Key Transmitted = {0} {1}", e.Control.Command, e.Control.Option);
			for (int i = 0; i < Interface.CurrentControls.Length; i++)
			{
				//var x = Interface.CurrentControls[i];

				//System.Console.WriteLine("{6}: CurrentControls [{0}] : Control {1} : Option {2} {3} {4} {5}", i, x.Command, x.Option, x.Key, x.LastState, x.Method, System.DateTime.Now);
				if (Interface.CurrentControls[i].Method != ControlMethod.InputDevicePlugin)
				{
					continue;
				}
				bool enableOption = false;
				for (int j = 0; j < Translations.CommandInfos.Length; j++)
				{
					if (Interface.CurrentControls[i].Command == Translations.CommandInfos[j].Command)
					{
						enableOption = Translations.CommandInfos[j].EnableOption;
						break;
					}
				}
				if (e.Control.Command == Interface.CurrentControls[i].Command)
				{
					int a = Interface.CurrentControls[i].Option;

					if (enableOption && e.Control.Option != Interface.CurrentControls[i].Option)
					{
						continue;
					}
					Interface.CurrentControls[i].AnalogState = 1.0;
					Interface.CurrentControls[i].DigitalState = DigitalControlState.Pressed;
					AddControlRepeat(i);
				}
			}
			System.Console.WriteLine("Key Transmitted = {0} {1}", e.Control.Command, e.Control.Option);
		}

		internal static void InputDevicePluginKeyUp(object sender, InputEventArgs e)
		{
			for (int i = 0; i < Interface.CurrentControls.Length; i++)
			{
				if (Interface.CurrentControls[i].Method != ControlMethod.InputDevicePlugin)
				{
					continue;
				}
				bool enableOption = false;
				for (int j = 0; j < Translations.CommandInfos.Length; j++)
				{
					if (Interface.CurrentControls[i].Command == Translations.CommandInfos[j].Command)
					{
						enableOption = Translations.CommandInfos[j].EnableOption;
						break;
					}
				}
				if (e.Control.Command == Interface.CurrentControls[i].Command)
				{
					if (enableOption && e.Control.Option != Interface.CurrentControls[i].Option)
					{
						continue;
					}
					Interface.CurrentControls[i].AnalogState = 0.0;
					Interface.CurrentControls[i].DigitalState = DigitalControlState.Released;
					RemoveControlRepeat(i);
				}
			}
		}
	}
}
