using OpenBveApi.FileSystem;
using OpenBveApi.Interface;
using OpenBveApi.Runtime;
using System;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace EatmTramController
{
	public class EatmTramControllerInput : IInputDevice
	{
		/// <summary>
		/// Define KeyDown event
		/// </summary>
		public event EventHandler<InputEventArgs> KeyDown;

		/// <summary>
		/// Define KeyUp event
		/// </summary>
		public event EventHandler<InputEventArgs> KeyUp;

		/// <summary>
		/// The control list that is using for plugin
		/// </summary>
		public InputControl[] Controls
		{
			get; private set;
		}

		private ConfigForm m_configForm;
		private bool m_pauseTick;
		private bool m_first;

		private int _reverserPos = 0;
		private int _lastReverserPos = -128;
		private int _handlePos = 0;
		private int _lastHandlePos = -128;

		//Listens on port 11000
		private ListenerSocket listenerSocket = new ListenerSocket();

		/// <summary>
		/// A function call when the plugin is loading
		/// </summary>
		/// <param name="fileSystem">The instance of FileSytem class</param>
		/// <returns>Check the plugin loading process is successfully</returns>
		public bool Load(FileSystem fileSystem)
		{
			//Handle the events
			listenerSocket.KeyDown += ListenerSocket_KeyDown;
			listenerSocket.KeyUp += ListenerSocket_KeyUp;
			//Start the listener thread
			Thread listener = new Thread(new ThreadStart(listenerSocket.StartServer));
			listener.Start();

			m_first = true;
			//JoystickApi.Init();

			m_configForm = new ConfigForm();
			string settingsPath = fileSystem.SettingsFolder + System.IO.Path.DirectorySeparatorChar + "1.5.0";
			m_configForm.loadConfigurationFile(settingsPath);
			m_configForm.Hide();

			_reverserPos = 0;
			_lastReverserPos = -128;
			_handlePos = 0;
			_lastHandlePos = -128;

			//These are public and are required by the plugin Interface
			//You get an error if you remove it.
			Controls = CommandTranslator.Controls();

#pragma warning disable 618

			return true;
		}

		private void ListenerSocket_KeyUp(object sender, byte e)
		{
			SystemSounds.Beep.Play();
			InputControl ic = CommandTranslator.TranslateKey(e);
			InputEventArgs ex = new InputEventArgs(ic);
			if (KeyUp != null)
			{
				KeyUp(this, ex);
			}
		}

		private void ListenerSocket_KeyDown(object sender, byte e)
		{
			SystemSounds.Asterisk.Play();
			InputControl ic = CommandTranslator.TranslateKey(e);
			InputEventArgs ex = new InputEventArgs(ic);
			if (KeyDown != null)
			{
				KeyDown(this, ex);
			}
		}

		/// <summary>
		/// A function call when the plugin is unload
		/// </summary>
		public void Unload()
		{
			//Stop the listener thread
			listenerSocket.StopServer();
			m_configForm.Dispose();
		}

		/// <summary>
		/// A funciton call when the Config button pressed
		/// </summary>
		/// <param name="owner">The owner of the window</param>
		public void Config(IWin32Window owner)
		{
			m_first = false;
			m_pauseTick = true;
			m_configForm.ShowDialog(owner);
			m_pauseTick = false;
		}

		/// <summary>
		/// The function what the notify to the plugin that the train maximum notches
		/// </summary>
		/// <param name="powerNotch">Maximum power notch number</param>
		/// <param name="brakeNotch">Maximum brake notch number</param>
		public void SetMaxNotch(int powerNotch, int brakeNotch)
		{
			_reverserPos = 0;
			_lastReverserPos = -128;
			_handlePos = 0;
			_lastHandlePos = -128;
		}

		/// <summary>
		/// The function what notify to the plugin that the train existing status
		/// </summary>
		/// <param name="data">Data</param>
		public void SetElapseData(ElapseData data)
		{
		}

		/// <summary>
		/// A function that calls each frame
		/// </summary>
		public void OnUpdateFrame()
		{
			if (m_pauseTick)
			{
				return;
			}

			if (m_first)
			{
				//m_configForm.enumerateDevices();
				m_first = false;
			}

			//JoystickApi.Update();

			setReverserPos();
			setHandlePos();
			setSwitchState();
		}

		protected virtual void OnKeyDown(InputEventArgs e)
		{
			if (KeyDown != null)
				KeyDown(this, e);
		}

		protected virtual void OnKeyUp(InputEventArgs e)
		{
			if (KeyUp != null)
			{
				KeyUp(this, e);
			}
		}

		private void setSwitchState()
		{
			//if (JoystickApi.currentDevice == -1)
			//{
			//	return;
			//}

			//var currentButtonState = JoystickApi.GetButtonsState();
			int buttonNum = 6;

			//if (currentButtonState.Count < buttonNum || JoystickApi.lastButtonState.Count < buttonNum)
			{
				return;
			}

			//for (int i = 0; i < buttonNum; ++i)
			//{
			//	if (currentButtonState[i] != JoystickApi.lastButtonState[i])
			//	{
			//		if (currentButtonState[i] == OpenTK.Input.ButtonState.Pressed)
			//		{
			//			int keyIdx = getKeyIdx(i);
			//			if (keyIdx != -1)
			//			{
			//				OnKeyDown(new InputEventArgs(Controls[keyIdx]));
			//			}
			//		}
			//		else if (currentButtonState[i] == OpenTK.Input.ButtonState.Released)
			//		{
			//			int keyIdx = getKeyIdx(i);
			//			if (keyIdx != -1)
			//			{
			//				OnKeyUp(new InputEventArgs(Controls[keyIdx]));
			//			}
			//		}
			//	}
			//}
		}

		private int getKeyIdx(int i)
		{
			int keyIdx = -1;
			//ConfigForm.ConfigFormSaveData config = m_configForm.Configuration;

			//if (config.switchS == i)
			//{
			//	keyIdx = 19;
			//}
			//else if (config.switchA1 == i)
			//{
			//	keyIdx = 20;
			//}
			//else if (config.switchA2 == i)
			//{
			//	keyIdx = 21;
			//}
			//else if (config.switchB1 == i)
			//{
			//	keyIdx = 22;
			//}
			//else if (config.switchB2 == i)
			//{
			//	keyIdx = 23;
			//}
			//else if (config.switchC1 == i)
			//{
			//	keyIdx = 24;
			//}
			//else if (config.switchC2 == i)
			//{
			//	keyIdx = 25;
			//}
			//else if (config.switchD == i)
			//{
			//	keyIdx = 26;
			//}
			//else if (config.switchE == i)
			//{
			//	keyIdx = 27;
			//}
			//else if (config.switchF == i)
			//{
			//	keyIdx = 28;
			//}
			//else if (config.switchG == i)
			//{
			//	keyIdx = 29;
			//}
			//else if (config.switchH == i)
			//{
			//	keyIdx = 30;
			//}
			//else if (config.switchI == i)
			//{
			//	keyIdx = 31;
			//}
			//else if (config.switchJ == i)
			//{
			//	keyIdx = 32;
			//}
			//else if (config.switchK == i)
			//{
			//	keyIdx = 33;
			//}
			//else if (config.switchL == i)
			//{
			//	keyIdx = 34;
			//}
			//else if (config.switchReverserFront == i)
			//{
			//	keyIdx = 16;
			//}
			//else if (config.switchReverserNeutral == i)
			//{
			//	keyIdx = 17;
			//}
			//else if (config.switchReverserBack == i)
			//{
			//	keyIdx = 18;
			//}
			//else if (config.switchHorn1 == i)
			//{
			//	keyIdx = 35;
			//}
			//else if (config.switchHorn2 == i)
			//{
			//	keyIdx = 36;
			//}
			//else if (config.switchMusicHorn == i)
			//{
			//	keyIdx = 37;
			//}
			//else if (config.switchConstSpeed == i)
			//{
			//	keyIdx = 38;
			//}

			return keyIdx;
		}

		private void setHandlePos()
		{
			//var buttonsState = JoystickApi.GetButtonsState();
			//if (InputTranslator.TranslateNotchPosition(buttonsState, out _handlePos))
			//{
			//	return;
			//}

			var isNotchIntermediateEstimated = false;

			// Problem between P1 and P2
			// https://twitter.com/SanYingOfficial/status/1088429762698129408
			//
			// P5 may be output when the notch is between P1 and P2.
			// This is an unintended output and should be excluded.
			if (_handlePos == 5)
			{
				if (_lastHandlePos == 1)
				{
					isNotchIntermediateEstimated = true;
				}
				else if (_lastHandlePos == 2)
				{
					isNotchIntermediateEstimated = true;
				}
			}

			if (!isNotchIntermediateEstimated)
			{
				for (int i = 0; i < 16; i++)
				{
					OnKeyUp(new InputEventArgs(Controls[i]));
				}

				if (_handlePos != _lastHandlePos)
				{
					if (_handlePos <= 0)
					{
						OnKeyDown(new InputEventArgs(Controls[_handlePos + 9]));
					}
					if (_handlePos >= 0)
					{
						OnKeyDown(new InputEventArgs(Controls[_handlePos + 10]));
					}
				}
			}

			_lastHandlePos = _handlePos;
		}

		private void setReverserPos()
		{
			//var axises = JoystickApi.GetAxises();
			//InputTranslator.TranslateReverserPosition(axises, out _reverserPos);

			for (int i = 16; i < 19; i++)
			{
				OnKeyUp(new InputEventArgs(Controls[i]));
			}

			if (_reverserPos != _lastReverserPos)
			{
				OnKeyDown(new InputEventArgs(Controls[17 - _reverserPos]));
			}

			_lastReverserPos = _reverserPos;
		}
	}
}
