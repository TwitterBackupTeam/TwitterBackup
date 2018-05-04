using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackup.Data.DTO
{
	public class UserDto
	{
		[JsonProperty("id")]
		public string Id { get; set; }


		public string Email { get; set; }


		public string UserName { get; set; }
	}

}
