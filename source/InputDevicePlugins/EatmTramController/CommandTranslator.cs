using OpenBveApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatmTramController
{
	public static class CommandTranslator
	{
		public const int BRAKE_EMERGENCY = 0;

		public const int BRAKE_NOTCH_1 = 1;
		public const int BRAKE_NOTCH_2 = 2;
		public const int BRAKE_NOTCH_3 = 3;
		public const int BRAKE_NOTCH_4 = 4;
		public const int BRAKE_NOTCH_5 = 5;
		public const int BRAKE_NOTCH_6 = 6;
		public const int BRAKE_NOTCH_7 = 7;
		public const int BRAKE_NOTCH_8 = 8;
		public const int BRAKE_NOTCH_9 = 9;

		public const int POWER_NEUTRAL = 10;
		public const int POWER_NOTCH_1 = 11;
		public const int POWER_NOTCH_2 = 12;
		public const int POWER_NOTCH_3 = 13;
		public const int POWER_NOTCH_4 = 14;
		public const int POWER_NOTCH_5 = 15;
		//public const int POWER_NOTCH_6 = 16;
		//public const int POWER_NOTCH_7 = 17;
		//public const int POWER_NOTCH_8 = 18;

		//Reverse = -1,
		//Neutral = 0,
		//Forwards = 1
		public const int REVERSER_BACKWARD = 16;

		public const int REVERSER_NEUTRAL = 17;
		public const int REVERSER_FORWARD = 18;

		public const int SECURITY_S = 19;
		public const int SECURITY_A1 = 20;
		public const int SECURITY_A2 = 21;
		public const int SECURITY_B1 = 22;
		public const int SECURITY_B2 = 23;
		public const int SECURITY_C1 = 24;
		public const int SECURITY_C2 = 25;
		public const int SECURITY_D = 26;
		public const int SECURITY_E = 27;
		public const int SECURITY_F = 28;
		public const int SECURITY_G = 29;
		public const int SECURITY_H = 30;
		public const int SECURITY_I = 31;
		public const int SECURITY_J = 32;
		public const int SECURITY_K = 33;
		public const int SECURITY_L = 34;

		public const int HORN_PRIMARY = 35;
		public const int HORN_SECONDARY = 36;
		public const int HORN_MUSIC = 37;
		public const int DEVICE_CONSTANT_SPEED = 38;

		/// <summary>
		/// These have been mapped the same way as SangYingInput
		/// </summary>
		/// <returns></returns>
		public static InputControl[] Controls()
		{
			InputControl[] Controls = new InputControl[39];

			//Power Handle to idle
			Controls[BRAKE_EMERGENCY].Command = Translations.Command.BrakeEmergency;
			for (int i = 1; i < 10; i++)
			{
				Controls[i].Command = Translations.Command.BrakeAnyNotch;
				Controls[i].Option = 9 - i;
			}
			for (int i = 10; i < 16; i++)
			{
				Controls[i].Command = Translations.Command.PowerAnyNotch;
				Controls[i].Option = i - 10;
			}
			for (int i = 16; i < 19; i++)
			{
				Controls[i].Command = Translations.Command.ReverserAnyPostion;
				Controls[i].Option = 17 - i;
			}
#pragma warning disable 618
			Controls[SECURITY_S].Command = Translations.Command.SecurityS;
			Controls[SECURITY_A1].Command = Translations.Command.SecurityA1;
			Controls[SECURITY_A2].Command = Translations.Command.SecurityA2;
			Controls[SECURITY_B1].Command = Translations.Command.SecurityB1;
			Controls[SECURITY_B2].Command = Translations.Command.SecurityB2;
			Controls[SECURITY_C1].Command = Translations.Command.SecurityC1;
			Controls[SECURITY_C2].Command = Translations.Command.SecurityC2;
			Controls[SECURITY_D].Command = Translations.Command.SecurityD;
			Controls[SECURITY_E].Command = Translations.Command.SecurityE;
			Controls[SECURITY_F].Command = Translations.Command.SecurityF;
			Controls[SECURITY_G].Command = Translations.Command.SecurityG;
			Controls[SECURITY_H].Command = Translations.Command.SecurityH;
			Controls[SECURITY_I].Command = Translations.Command.SecurityI;
			Controls[SECURITY_J].Command = Translations.Command.SecurityJ;
			Controls[SECURITY_K].Command = Translations.Command.SecurityK;
			Controls[SECURITY_L].Command = Translations.Command.SecurityL;
#pragma warning restore 618
			Controls[35].Command = Translations.Command.HornPrimary;
			Controls[36].Command = Translations.Command.HornSecondary;
			Controls[37].Command = Translations.Command.HornMusic;
			Controls[38].Command = Translations.Command.DeviceConstSpeed;
			return Controls;
		}

		public static InputControl TranslateKey(byte keyIn)
		{
			byte[] by = new byte[1];
			by[0] = keyIn;
			InputControl retVal = new InputControl();
			string key = System.Text.Encoding.ASCII.GetString(by);
			key = key.ToUpper();
			switch (key)
			{
				//power notches
				case "1":
					{
						retVal.Command = Translations.Command.PowerAnyNotch;
						retVal.Option = 1;
						break;
					}
				case "2":
					{
						retVal.Command = Translations.Command.PowerAnyNotch;
						retVal.Option = 2;
						break;
					}
				case "3":
					{
						retVal.Command = Translations.Command.PowerAnyNotch;
						retVal.Option = 3;
						break;
					}
				case "4":
					{
						retVal.Command = Translations.Command.PowerAnyNotch;
						retVal.Option = 4;
						break;
					}
				case "5":
					{
						retVal.Command = Translations.Command.PowerAnyNotch;
						retVal.Option = 5;
						break;
					}
				case "6":
					{
						retVal.Command = Translations.Command.PowerAnyNotch;
						retVal.Option = 6;
						break;
					}
				case "7":
					{
						retVal.Command = Translations.Command.PowerAnyNotch;
						retVal.Option = 7;
						break;
					}
				case "8":
					{
						retVal.Command = Translations.Command.PowerAnyNotch;
						retVal.Option = 8;
						break;
					}

				//brake notches
				case "9":
					{
						retVal.Command = Translations.Command.BrakeAnyNotch;
						retVal.Option = 1;
						break;
					}
				case "0":
					{
						retVal.Command = Translations.Command.BrakeAnyNotch;
						retVal.Option = 2;
						break;
					}
				case "-":
					{
						retVal.Command = Translations.Command.BrakeAnyNotch;
						retVal.Option = 3;
						break;
					}
				case "=":
					{
						retVal.Command = Translations.Command.BrakeAnyNotch;
						retVal.Option = 4;
						break;
					}
				//power to idle
				case "Q":
					{
						//retVal.Command = Translations.Command.SingleNeutral;
						retVal.Command = Translations.Command.PowerAnyNotch;
						retVal.Option = 0;
						break;
					}
				//reverser key
				case "R":
					{
						retVal.Command = Translations.Command.ReverserAnyPostion;
						retVal.Option = 1;
						break;
					}
				case "F":
					{
						retVal.Command = Translations.Command.ReverserAnyPostion;
						//Reverse = -1,
						//Neutral = 0,
						//Forwards = 1
						retVal.Option = 0;
						break;
					}
				case "V":
					{
						retVal.Command = Translations.Command.ReverserAnyPostion;
						//Reverse = -1,
						//Neutral = 0,
						//Forwards = 1
						retVal.Option = -1;
						break;
					}
				default:
					{
						retVal.Command = Translations.Command.None;
						retVal.Option = 0;
						break;
					}
			}

			return retVal;
		}
	}
}
