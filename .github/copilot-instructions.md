# GitHub Copilot Instructions

This file contains instructions for GitHub Copilot to help you work effectively with the Baubit.Networking project.

## About This Project

This repository contains the Baubit.Networking .NET9 solution, including:

- CircleCI configuration for build, test, pack/publish, and release workflows
- Code coverage reporting with Codecov
- NuGet package publishing

## Key Configuration Files

- `.circleci/config.yml` - CircleCI pipeline configuration for Baubit.Networking
- `codecov.yml` - Code coverage configuration
- `README.md` - Project documentation and setup instructions

## Working with Baubit.Networking

- All code should follow .NET9 and C# best practices. Use modern language features and prefer clear, maintainable code.
- Organize code by feature and keep test files in the `Baubit.Networking.Test` project.

## Testing and Coverage

- All new features and bug fixes should include corresponding unit or integration tests.
- Run tests locally before pushing changes. Use `dotnet test` for .NET projects.
- Code coverage is reported via Codecov. Ensure coverage does not decrease for critical modules.

## CI/CD Standards

- CircleCI runs build, test, and publish workflows automatically on push and pull requests.
- Only the master branch publishes NuGet packages; release branch publishes to NuGet.org.
- GitHub releases are automatically generated when a pull request merges the master branch into release. The pull request’s title and description are used as the release notes, so ensure they are complete, accurate, and provide all relevant details.

## Release Creation Instructions

When asked to "Create a release":
- Merge the latest changes from `master` into `release` via a pull request.
- Ensure the pull request title and description are detailed and accurate, as they will be used for the GitHub release notes.
- After merging, a GitHub release is automatically created with these notes.

## Project Structure and Technology Notes

- Main code is in `Baubit.Networking`.
- Tests are in `Baubit.Networking.Test`.
- .NET MAUI is used for cross-platform UI; see project files for details.
- JavaScript/TypeScript code should be placed in appropriate directories and follow project conventions.

## Additional Guidelines

- Keep dependencies up to date and prefer official NuGet packages.
- Document public APIs and major design decisions in `README.md`.
- Use pull requests for all changes; ensure code review and CI checks pass before merging.
- Always look for opportunities to refine and improve this document as new project information, standards, or best practices are learned. If an update is deemed beneficial, submit a revision to keep instructions current and relevant.