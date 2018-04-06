﻿using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of members.
	/// </summary>
	public class ReadOnlyMemberCollection : ReadOnlyCollection<IMember>, IReadOnlyMemberCollection
	{
		private readonly EntityRequestType _updateRequestType;
		private Dictionary<string, object> _additionalParameters;

		/// <summary>
		/// Retrieves a member which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching member, or null if none found.</returns>
		/// <remarks>
		/// Matches on <see cref="Member.Id"/>, <see cref="Member.FullName"/>, and <see cref="Member.UserName"/>.  Comparison is case-sensitive.
		/// </remarks>
		public IMember this[string key] => GetByKey(key);

		internal ReadOnlyMemberCollection(EntityRequestType requestType, Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
			_updateRequestType = requestType;
			_additionalParameters = new Dictionary<string, object> {{"fields", "all"}};
		}
		internal ReadOnlyMemberCollection(ReadOnlyMemberCollection source, TrelloAuthorization auth)
			: base(() => source.OwnerId, auth)
		{
			_updateRequestType = source._updateRequestType;
			if (source._additionalParameters != null)
				_additionalParameters = new Dictionary<string, object>(source._additionalParameters);
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		public void Filter(MemberFilter filter)
		{
			var filters = filter.GetFlags().Cast<MemberFilter>();
			Filter(filters);
		}

		/// <summary>
		/// Adds a set of filters to the collection.
		/// </summary>
		/// <param name="filters">The filter values.</param>
		public void Filter(IEnumerable<MemberFilter> filters)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object> {{"filter", string.Empty}};
			var filter = _additionalParameters.ContainsKey("filter") ? (string)_additionalParameters["filter"] : string.Empty;
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += filters.Select(a => a.GetDescription()).Join(",");
			_additionalParameters["filter"] = filter;
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected sealed override void Update()
		{
			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonMember>>(Auth, endpoint, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jm =>
				{
					var member = jm.GetFromCache<Member>(Auth);
					member.Json = jm;
					return member;
				}));
		}

		private IMember GetByKey(string key)
		{
			return this.FirstOrDefault(m => key.In(m.Id, m.FullName, m.UserName));
		}
	}
}