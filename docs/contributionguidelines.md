# Contribution Guidelines for ZombieShooter

Welcome to the ZombieShooter project! This document outlines the rules and patterns to follow when contributing to the repository to ensure consistency and maintain the quality of the project.

## Table of Contents

1. [Coding Standards](#coding-standards)
2. [Branching Strategy](#branching-strategy)
3. [Commit Message Guidelines](#commit-message-guidelines)
4. [Pull Request Process](#pull-request-process)
5. [Issue Reporting](#issue-reporting)
6. [Code Review](#code-review)

## Coding Standards

- **Language**: The project is developed in C#. Follow the C# coding conventions.
- **Naming Conventions**: 
  - Use `PascalCase` for class names and `camelCase` for method names.
  - Use clear and descriptive names for variables and methods.
- **Commenting**: 
  - Use XML comments for public methods and classes.
  - Add comments to explain complex logic or code blocks.
- **Formatting**: 
  - Use consistent indentation (4 spaces).
  - Limit line length to 120 characters.

## Branching Strategy

- **Main Branch**: The `main` branch contains the stable codebase. All releases are made from this branch.
- **Feature Branches**: Create new branches for new features or bug fixes. Name them using the format `feature/<description>` or `bugfix/<description>`.
- **Development Branch**: Use a `develop` branch for integrating features before merging into `main`.

## Commit Message Guidelines

- **Format**: Use the format `<type>(<scope>): <subject>`, where `<type>` is one of `feat`, `fix`, `docs`, `style`, `refactor`, `test`, or `chore`.
- **Description**: Provide a concise description of the changes made.

  ```

## Pull Request Process

1. **Fork the Repository**: Create a fork of the repository to work on your changes.
2. **Create a Branch**: Create a new branch for your feature or bugfix.
3. **Make Changes**: Make your changes in the new branch.
4. **Commit Changes**: Commit your changes with a descriptive commit message.
5. **Push Branch**: Push your branch to your forked repository.
6. **Create Pull Request**: Open a pull request against the `develop` branch.
7. **Review Process**: Your pull request will be reviewed by the maintainers. Make necessary changes if requested.
8. **Merge**: Once approved, your pull request will be merged into the `develop` branch.

## Issue Reporting

- **Search Existing Issues**: Before opening a new issue, check if it has already been reported.
- **Create a New Issue**: If not found, create a new issue with a detailed description. Use the provided issue templates.
- **Include Steps to Reproduce**: Provide steps to reproduce the issue, if applicable.

## Code Review

- **Peer Review**: All changes must be peer-reviewed before merging.
- **Review Feedback**: Address any feedback provided during the review process.
- **Approval**: A pull request must be approved by at least one maintainer before merging.

By following these guidelines, you help ensure that the ZombieShooter project remains maintainable and of high quality. Thank you for your contributions!
