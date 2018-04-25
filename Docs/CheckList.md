# CheckList

Represents a checklist.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- CheckList

## Constructors

### CheckList(string id, TrelloAuthorization auth)

Creates a new instance of the [CheckList](CheckList#checklist) object.

**Parameter:** id

The check list&#39;s ID.

**Parameter:** auth

(Optional) Custom authorization parameters. When not provided, [TrelloAuthorization.Default](TrelloAuthorization#static-trelloauthorization-default--get-) will be used.

## Properties

### static Manatee.Trello.CheckList+Fields DownloadedFields { get; set; }

Specifies which fields should be downloaded.

### [IBoard](IBoard#iboard) Board { get; }

Gets the board on which the checklist belongs.

### [ICard](ICard#icard) Card { get; set; }

Gets or sets the card on which the checklist belongs.

### [ICheckItemCollection](ICheckItemCollection#icheckitemcollection) CheckItems { get; }

Gets the collection of items in the checklist.

### DateTime CreationDate { get; }

Gets the creation date of the checklist.

### string Id { get; }

Gets the checklist&#39;s ID.

### [ICheckItem](ICheckItem#icheckitem) this[string key] { get; }

Retrieves a check list item which matches the supplied key.

**Parameter:** key

The key to match.

#### Returns

The matching check list item, or null if none found.

#### Remarks

Matches on checkitem ID and name. Comparison is case-sensitive.

### [ICheckItem](ICheckItem#icheckitem) this[int index] { get; }

Retrieves the check list item at the specified index.

**Parameter:** index

The index.

**Exception:** System.ArgumentOutOfRangeException

*index* is less than 0 or greater than or equal to the number of elements in the collection.

#### Returns

The check list item.

### string Name { get; set; }

Gets the checklist&#39;s name.

### [Position](Position#position) Position { get; set; }

Gets the checklist&#39;s position.

## Events

### Action&lt;ICheckList, IEnumerable&lt;string&gt;&gt; Updated

Raised when data on the check list is updated.

## Methods

### Task Delete(CancellationToken ct)

Deletes the checklist.

**Parameter:** ct

(Optional) A cancellation token for async processing.

#### Remarks

This permanently deletes the checklist from Trello&#39;s server, however, this object will remain in memory and all properties will remain accessible.

### Task Refresh(CancellationToken ct)

Refreshes the checklist data.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### string ToString()

Returns a string that represents the current object.

**Returns:** A string that represents the current object.
