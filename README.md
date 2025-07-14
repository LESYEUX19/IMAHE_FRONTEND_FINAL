# Imaheprojects

A cross-platform .NET MAUI Blazor application for AI-powered photo sorting and management.

## Prerequisites

- **Visual Studio 2022** (latest version recommended)
  - Workload: ".NET MAUI development" (includes .NET 7/8/9, Android/iOS/Windows targets)
  - Optional: "ASP.NET and web development" for Blazor
- **.NET 9 SDK** (or the version specified in the project)
- **Git** (for cloning the repository)

## Getting Started

### 1. Clone the Repository

```
git clone https://github.com/YOUR-USERNAME/YOUR-REPO.git
cd Imaheprojects
```

### 2. Open the Solution

- Open `Imaheprojects.sln` in Visual Studio 2022.

### 3. Restore NuGet Packages

- Visual Studio will usually restore packages automatically.
- If not, right-click the solution in Solution Explorer and select **Restore NuGet Packages**.

### 4. Build the Project

- Select your target platform (Windows, Android, iOS, etc.) in the toolbar.
- Click **Build > Build Solution** or press `Ctrl+Shift+B`.

### 5. Run the App

- Click the green **Start** button or press `F5` to run with the debugger.
- Or use `Ctrl+F5` to run without debugging.

### 6. Troubleshooting

- If you see errors about missing SDKs or workloads, open the Visual Studio Installer and ensure ".NET MAUI development" is checked.
- For Android/iOS: You may need an emulator or a physical device connected.
- For Windows: Make sure you are running on Windows 10 1809+ or later.

### 7. Project Structure

- `Components/Pages/` — Main app pages (Home, About, Developer, etc.)
- `Components/Layout/` — Layout components
- `wwwroot/Assets/` — Static images and assets
- `wwwroot/css/app.css` — Main stylesheet
- `.gitignore` — Ensures no build artifacts or user-specific files are committed

### 8. Security

- The `.gitignore` is set up to exclude all build output, user settings, and sensitive files.
- No secrets or credentials are included in this repository.

---

## Need Help?
If you have any issues running the project, please open an issue or contact the project maintainer.

**Contact:**
- Email: johnorlandsudoy49@gmail.com
- Discord: JohnSudoy 