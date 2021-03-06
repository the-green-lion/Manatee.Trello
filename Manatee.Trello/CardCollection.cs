﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of cards.
	/// </summary>
	public class CardCollection : ReadOnlyCardCollection, ICardCollection
	{
		internal CardCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(typeof (List), getOwnerId, auth) {}

		/// <summary>
		/// Creates a new card.
		/// </summary>
		/// <param name="name">A name</param>
		/// <param name="description">(optional) A description</param>
		/// <param name="position">(optional) The card's position in the list.</param>
		/// <param name="dueDate">(optional) A due date.</param>
		/// <param name="isComplete">(optional) Indicates whether the card is complete.</param>
		/// <param name="members">(optional) A collection of members to add to the card.</param>
		/// <param name="labels">(optional) A collection of labels to add to the card.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The <see cref="ICard"/> generated by Trello.</returns>
		public async Task<ICard> Add(string name, string description = null, Position position = null,
		                             DateTime? dueDate = null,
		                             bool? isComplete = null, IEnumerable<IMember> members = null,
		                             IEnumerable<ILabel> labels = null,
		                             CancellationToken ct = default(CancellationToken))
		{
			var error = NotNullOrWhiteSpaceRule.Instance.Validate(null, name);
			if (error != null)
				throw new ValidationException<string>(name, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonCard>();
			json.Name = name;
			json.Desc = description;
			json.Pos = position == null ? null : Position.GetJson((Position) position);
			json.Due = dueDate;
			json.DueComplete = isComplete;
			json.IdMembers = members?.Select(m => m.Id).Join(",");
			json.IdLabels = labels?.Select(l => l.Id).Join(",");

			return await CreateCard(json, ct);
		}

		/// <summary>
		/// Creates a new card by copying a card.
		/// </summary>
		/// <param name="source">A card to copy.  Default is null.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The <see cref="ICard"/> generated by Trello.</returns>
		public async Task<ICard> Add(ICard source, CancellationToken ct = default(CancellationToken))
		{
			var error = NotNullRule<ICard>.Instance.Validate(null, source);
			if (error != null)
				throw new ValidationException<ICard>(source, new[] {error});

			var json = TrelloConfiguration.JsonFactory.Create<IJsonCard>();
			json.CardSource = ((Card)source).Json;

			return await CreateCard(json, ct);
		}

		/// <summary>
		/// Creates a new card by importing data from a URL.
		/// </summary>
		/// <param name="name">The name of the card to add.</param>
		/// <param name="sourceUrl"></param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The <see cref="ICard"/> generated by Trello.</returns>
		public async Task<ICard> Add(string name, string sourceUrl, CancellationToken ct = default(CancellationToken))
		{
			var error = NotNullOrWhiteSpaceRule.Instance.Validate(null, name);
			if (error != null)
				throw new ValidationException<string>(name, new[] { error });
			error = UriRule.Instance.Validate(null, sourceUrl);
			if (error != null)
				throw new ValidationException<string>(sourceUrl, new[] { error });

			var json = TrelloConfiguration.JsonFactory.Create<IJsonCard>();
			json.Name = name;
			json.UrlSource = sourceUrl;

			return await CreateCard(json, ct);
		}

		private async Task<Card> CreateCard(IJsonCard json, CancellationToken ct)
		{
			json.List = TrelloConfiguration.JsonFactory.Create<IJsonList>();
			json.List.Id = OwnerId;

			var endpoint = EndpointFactory.Build(EntityRequestType.List_Write_AddCard);
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			return new Card(newData, Auth);
		}
	}
}