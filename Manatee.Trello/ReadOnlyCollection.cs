using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides base functionality for a read-only collection.
	/// </summary>
	/// <typeparam name="T">The type of object contained by the collection.</typeparam>
	public abstract class ReadOnlyCollection<T> : IReadOnlyCollection<T>
	{
		private string _ownerId;
		private readonly Func<string> _getOwnerId;

		/// <summary>
		/// Indicates the maximum number of items to return.
		/// </summary>
		public int? Limit { get; set; }
		/// <summary>
		/// Retrieves the item at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The item.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the collection.
		/// </exception>
		public T this[int index] => GetByIndex(index);

		internal string OwnerId => _ownerId ?? (_ownerId = _getOwnerId());
		internal List<T> Items { get; }
		internal TrelloAuthorization Auth { get; }

		/// <summary>
		/// Creates a new instance of the <see cref="ReadOnlyCollection{T}"/> object.
		/// </summary>
		/// <param name="getOwnerId"></param>
		/// <param name="auth"></param>
		protected ReadOnlyCollection(Func<string> getOwnerId, TrelloAuthorization auth)
		{
			_getOwnerId = getOwnerId;
			Auth = auth ?? TrelloAuthorization.Default;

			Items = new List<T>();
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			return Items.GetEnumerator();
		}

		/// <summary>
		/// Manually updates the collection's data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public abstract Task Refresh(CancellationToken ct = default(CancellationToken));

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Adds <see cref="Limit"/> to a list of additional parameters.
		/// </summary>
		/// <param name="additionalParameters">The list of additional parameters.</param>
		protected void IncorporateLimit(Dictionary<string, object> additionalParameters)
		{
			if (!Limit.HasValue) return;

			additionalParameters["limit"] = Limit.Value;
		}

		internal void Update(IEnumerable<T> items)
		{
			Items.Clear();
			Items.AddRange(items);
		}

		private T GetByIndex(int index)
		{
			return this.ElementAt(index);
		}
	}
}