﻿using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of organization memberships.
	/// </summary>
	public interface IOrganizationMembershipCollection : IReadOnlyOrganizationMembershipCollection
	{
		/// <summary>
		/// Adds a member to an organization with specified privileges.
		/// </summary>
		/// <param name="member">The member to add.</param>
		/// <param name="membership">The membership type.</param>
		Task Add(IMember member, OrganizationMembershipType membership, CancellationToken ct = default(CancellationToken));

		/// <summary>
		/// Removes a member from an organization.
		/// </summary>
		/// <param name="member">The member to remove.</param>
		Task Remove(IMember member, CancellationToken ct = default(CancellationToken));
	}
}