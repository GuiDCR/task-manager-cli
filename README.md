# TaskTracker CLI

A simple command-line task manager built with C# and .NET. Add, update, delete and track the status of your tasks directly from the terminal — all data is stored locally in a JSON file.

This project is based on the [Task Tracker challenge from roadmap.sh](https://roadmap.sh/projects/task-tracker).

## Features

- Add, update and delete tasks
- Mark tasks as `todo`, `in-progress` or `done`
- List all tasks or filter by status
- Delete all tasks at once (with confirmation)
- Persistent storage in a local JSON file
- Handles missing or corrupted data files gracefully

## Requirements

- [.NET SDK 8.0](https://dotnet.microsoft.com/download) or later

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/GuiDCR/task-manager-cli.git
   cd task-manager-cli
   ```

2. Pack the project:
   ```bash
   dotnet pack -c Release
   ```

3. Install it as a global tool:
   ```bash
   dotnet tool install --global --add-source ./bin/Release TaskTracker.GuiDCR
   ```

4. Verify the installation:
   ```bash
   tasktracker help
   ```

## Usage

```bash
tasktracker <command> [<args>]
```

### Commands

| Command | Description |
|---|---|
| `add <description>` | Add a new task |
| `update <id> <description>` | Update a task's description |
| `delete <id>` | Delete a task |
| `delete --all` | Delete all tasks (asks for confirmation) |
| `mark-todo <id>` | Mark a task as todo |
| `mark-in-progress <id>` | Mark a task as in-progress |
| `mark-done <id>` | Mark a task as done |
| `list` | List all tasks |
| `list <status>` | List tasks filtered by status (`todo`, `in-progress`, `done`) |
| `help` | Show all available commands |

### Examples

```bash
tasktracker add "Buy groceries"
tasktracker update 1 "Buy groceries and cook dinner"
tasktracker mark-in-progress 1
tasktracker mark-done 1
tasktracker list done
tasktracker delete 1
```

## Data Storage

Tasks are stored in a `tasks.json` file. Each task has the following structure:

```json
{
  "Id": 1,
  "Description": "Buy groceries",
  "Status": "todo",
  "CreatedAt": "2026-06-12T10:30:00",
  "UpdatedAt": "2026-06-12T10:30:00"
}
```

If the file doesn't exist, it's created automatically. If it's corrupted or invalid, the app will notify you instead of crashing.

## Author

Built by [GuiDCR](https://github.com/GuiDCR)
