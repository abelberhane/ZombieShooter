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
- **Example**: 
  ```
