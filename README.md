# CyberBot
# Cyber Awareness Assistant (CyberbotProject)

A lightweight console-based chatbot that provides a cyber awareness assistant with a voice greeting. Built in C# targeting .NET 8.

## Features
- Console chatbot interaction with a friendly greeting
- Plays a voice greeting audio file on startup via `AudioPlayer`
- Simple user initialization and conversational loop
- Small, easy-to-read codebase for learning and extension

## Prerequisites
- .NET 8 SDK
- Visual Studio 2022 (or any editor that supports .NET 8)

## Build & Run
1. Clone the repository:
2. Build and run:
- Visual Studio: open the solution and build/run.
- CLI:
  ```
  dotnet build
  dotnet run --project CyberbotProject
  ```

## Usage
- On startup the app displays a logo and plays the greeting audio file.
- When prompted, enter your name to initialize the session.
- Conversation prompt appears as: `YourName >`
- Type messages to interact with the bot. Type `exit` or `quit` to terminate.

Default greeting file: `Buhle.wav`. Place this WAV file in the app working directory (or update the path in `Program.cs`).

## Project Structure
- `Program.cs` — application entry point, UI loop and onboarding
- `ChatBot.cs` — bot logic and response generation
- `AudioPlayer.cs` — audio playback helper
- `User.cs` — user model

## Audio Assets
- Use WAV format audio. Place `Buhle.wav` (or another WAV) into the output folder or provide an absolute path in `Program.cs` when calling `AudioPlayer.PlayGreeting`.

## Contributing & Coding Standards
- This project targets C# 12 and .NET 8. Open a pull request against `master` and follow contribution guidelines.

## License
No license file is included.
