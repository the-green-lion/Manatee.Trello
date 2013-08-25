/***************************************************************************************

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
 
	File Name:		IJsonBoard.cs
	Namespace:		Manatee.Trello.Json
	Class Name:		IJsonBoard
	Purpose:		Defines the JSON structure for the Board object.

***************************************************************************************/
namespace Manatee.Trello.Json
{
	/// <summary>
	/// Defines the JSON structure for the Board object.
	/// </summary>
	public interface IJsonBoard : IJsonCacheable
	{
		///<summary>
		/// Gets or sets the board's name.
		///</summary>
		string Name { get; set; }
		///<summary>
		/// Gets or sets the board's description.
		///</summary>
		string Desc { get; set; }
		///<summary>
		/// Gets or sets whether this board is closed.
		///</summary>
		bool? Closed { get; set; }
		/// <summary>
		/// Gets or sets the ID of the organization, if any, to which this board belongs.
		/// </summary>
		string IdOrganization { get; set; }
		///<summary>
		/// Gets or sets whether this board is pinned to the user's Boards menu.
		///</summary>
		bool? Pinned { get; set; }
		///<summary>
		/// Gets or sets the URL for this board.
		///</summary>
		string Url { get; set; }
		///<summary>
		/// Gets or sets whether the user is subscribed to this board.
		///</summary>
		bool? Subscribed { get; set; }
	}
}