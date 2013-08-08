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
 
	File Name:		CreateListAction.cs
	Namespace:		Manatee.Trello
	Class Name:		CreateListAction
	Purpose:		Indicates a list was added to a board.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a list was added to a board.
	/// </summary>
	public class CreateListAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private List _list;
		private readonly string _listId;
		private readonly string _listName;
		private readonly string _boardName;
		private string _stringFormat;

		/// <summary>
		/// Gets the board associated with the action.
		/// </summary>
		public Board Board
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Retrieve<Board>(_boardId)) : _board;
			}
		}
		/// <summary>
		/// Gets the list associated with the action.
		/// </summary>
		public List List
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_list == null) || (_list.Id != _listId)) && (Svc != null) ? (_list = Svc.Retrieve<List>(_listId)) : _list;
			}
		}

		/// <summary>
		/// Creates a new instance of the CreateListAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public CreateListAction(Action action)
			: base(action.Svc, action.Id)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board","id");
			_boardName = action.Data.TryGetString("board","name");
			_listId = action.Data.TryGetString("list","id");
			_listName = action.Data.TryGetString("list","name");
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return _stringFormat ?? (_stringFormat = string.Format("{0} created list '{1}' on board '{2}' on {3}",
			                                                       MemberCreator.FullName,
			                                                       List != null ? List.Name : _listName,
			                                                       Board != null ? Board.Name : _boardName,
			                                                       Date));
		}
	}
}