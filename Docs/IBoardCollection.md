# IBoardCollection

A collection of boards.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- IBoardCollection

## Methods

### Task&lt;IBoard&gt; Add(string name, IBoard source, CancellationToken ct)

Creates a new board.

**Parameter:** name

The name of the board to create.

**Parameter:** source

A board to use as a template.

**Parameter:** ct

(Optional) A cancellation token for async processing.

**Returns:** The [IBoard](IBoard#iboard) generated by Trello.
