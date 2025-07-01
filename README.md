# StarWarsAPI
Starships Project GoEngineer - README
Overview

This Blazor Server project provides a Star Wars-themed starship management app with:

    Display and CRUD operations for starships using MudBlazor UI components.

    Basic authentication via ASP.NET Core Identity to restrict access to authorized users.

    A seeded database with example starship data.

    User feedback with MudBlazor Snackbar notifications.

    Use of dialogs for editing and adding starships.

Project Features
1. Database and Seeder

    The project uses Entity Framework Core to manage starship data.

    A Seeder method populates the database initially with several Star Wars starships.

    Seed method is called on application startup (usually in Program.cs or during migration).

2. Starship Model

    Typical properties of a starship:

        Id (Primary key)

        Name

        Model

        Manufacturer

        CostInCredits

        Length

        MaxAtmospheringSpeed

        Crew

        Passengers

        CargoCapacity

        HyperdriveRating

        StarshipClass

3. StarshipService (Data Service)

    Handles async CRUD operations with EF Core:

        GetAllAsync() — Returns all starships, ordered by most recent first.

        AddAsync(Starship starship) — Adds a new starship.

        UpdateAsync(Starship starship) — Updates an existing starship, handles EF Core tracking issues by ensuring a single entity instance.

        DeleteAsync(int id) — Deletes starship by ID.

4. Starships Razor Component (UI)

    Displays starships in a MudTable with sorting, hovering, and pagination.

    Supports Add, Edit, Delete via:

        A MudDialog for adding/editing starships, instead of inline form.

        Confirmation before deletion (optional).

    Shows Snackbars on user actions for feedback (Add, Save, Delete, Cancel).

    Authentication-aware UI: The starships table only displays if the user is logged in.

    If unauthorized, shows a friendly message with a cute Star Wars image.

5. Authentication & Authorization

    Uses ASP.NET Core Identity for user registration, login, and auth management.

    Pages are restricted via [Authorize] or conditional UI rendering based on authentication state.

    User must register and login to manage starships.

    Prevents unauthenticated users from seeing the starship table but allows them to visit the page.

    Identity setup includes:

        UserManager, SignInManager, Email confirmation.

        Register page with validation and feedback.

    Authentication state is injected and used in the component to control access.

6. Common Issues & Fixes

    Entity Tracking Conflict during Update:

        EF Core throws if multiple entity instances with the same key are tracked.

        Fixed by attaching entities properly or querying the existing tracked entity before update.

    Snackbars not showing:

        Ensure ISnackbar service is injected and MudBlazor providers are configured in App.razor and MainLayout.razor.

    Page refreshing continuously with auth:

        Check the correct @page directive and avoid conflicting @rendermode directives.

        Use AuthenticationStateProvider to control UI rendering instead of forcing redirects.

    MudDialog for editing:

        Use MudBlazor’s DialogService to open dialogs and pass starship data for editing or adding.

        Dialog returns result to refresh the table.

Getting Started
Prerequisites

    .NET 8 SDK or later installed.

    Postgres(used for compatibility with render.com)

    Visual Studio 2022 or VS Code.

Running the Project

    Run EF Core migrations and seed the database.

    Run the Blazor Server app.

    Register a new user and login.

    Navigate to /starships.

    Add, edit, or delete starships.

    Log out to test unauthorized view with image and restricted UI.
