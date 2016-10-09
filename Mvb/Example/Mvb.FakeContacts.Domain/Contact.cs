using System;
using Mvb.Core.Base;

namespace Mvb.FakeContacts.Domain
{
	public class Contact : MvbBindable
	{
		private string _name;

		public string Name
		{
			get { return this._name; }
			set { this.SetProperty(ref this._name, value); }
		}

		private byte[] _avatarBytes;

		public byte[] AvatarBytes
		{
			get { return this._avatarBytes; }
			set { this.SetProperty(ref this._avatarBytes, value); }
		}

		/// <summary>
		/// Shakes the name.
		/// </summary>
		public void ShakeName()
		{

			var random = new Random(DateTime.Now.Millisecond);
			var nameLenght = this.Name.Length;
			var nameArray = this.Name.ToCharArray();

			for (int i = 0; i < nameLenght; i++)
			{
				var c = this.Name[i];
				var newPos = random.Next(nameLenght);
				nameArray[newPos] = c;
			}

			this.Name = new string(nameArray);
		}
	}
}
