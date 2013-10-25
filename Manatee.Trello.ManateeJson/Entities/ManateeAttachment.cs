﻿/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ManateeAttachment.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeAttachment
	Purpose:		Implements IJsonAttachment for Manatee.Json.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeAttachment : IJsonAttachment, IJsonCompatible
	{
		public string Id { get; set; }
		public int? Bytes { get; set; }
		public DateTime? Date { get; set; }
		public string IdMember { get; set; }
		public bool? IsUpload { get; set; }
		public string MimeType { get; set; }
		public string Name { get; set; }
		public List<IJsonAttachmentPreview> Previews { get; set; }
		public string Url { get; set; }

		public void FromJson(JsonValue json)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Bytes = (int?) obj.TryGetNumber("bytes");
			var dateString = obj.TryGetString("date");
			DateTime date;
			if (DateTime.TryParse(dateString, out date))
				Date = date;
			IdMember = obj.TryGetString("idMember");
			IsUpload = obj.TryGetBoolean("isUpload");
			MimeType = obj.TryGetString("mimeType");
			Name = obj.TryGetString("name");
			var previews = obj.TryGetArray("previews");
			if (previews != null)
				Previews = previews.FromJson<ManateeAttachmentPreview>()
								   .Cast<IJsonAttachmentPreview>()
								   .ToList();
			Url = obj.TryGetString("url");
		}
		public JsonValue ToJson()
		{
			return new JsonObject
			       	{
			       		{"id", Id},
			       		{"bytes", Bytes},
			       		{"date", Date.HasValue ? Date.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			       		{"idMember", IdMember},
			       		{"isUpload", IsUpload},
			       		{"mimeType", MimeType},
			       		{"name", Name},
			       		{"previews", Previews.Cast<ManateeAttachmentPreview>().ToJson()},
			       		{"url", Url}
			       	};
		}
	}
}
