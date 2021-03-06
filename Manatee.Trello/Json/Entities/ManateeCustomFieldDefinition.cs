﻿using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeCustomFieldDefinition : IJsonCustomFieldDefinition, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonBoard Board { get; set; }
		public string FieldGroup { get; set; }
		public string Name { get; set; }
		public IJsonPosition Pos { get; set; }
		public CustomFieldType? Type { get; set; }
		public List<IJsonCustomDropDownOption> Options { get; set; }
		public bool ValidForMerge { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					Board = obj.Deserialize<IJsonBoard>(serializer, "idBoard");
					FieldGroup = obj.TryGetString("fieldGroup");
					Name = obj.TryGetString("name");
					Pos = obj.Deserialize<IJsonPosition>(serializer, "pos");
					Type = obj.Deserialize<CustomFieldType?>(serializer, "type");
					Options = obj.Deserialize<List<IJsonCustomDropDownOption>>(serializer, "options");
					ValidForMerge = true;
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}

		public JsonValue ToJson(JsonSerializer serializer)
		{
			var obj = new JsonObject();

			if (Board != null)
			{
				obj["idModel"] = Board.Id;
				obj["modelType"] = "board";
			}
			Name.Serialize(obj, serializer, "name");
			Pos.Serialize(obj, serializer, "pos");
			Type.Serialize(obj, serializer, "type");

			return obj;
		}
	}
}