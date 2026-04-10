# WeatherDeskk – WPF MVVM Application with Unit Testing

## Overview
WeatherDeskk is a desktop application built using WPF and MVVM architecture.  
The application retrieves weather information for a given city and displays key details such as temperature, humidity, wind speed, and description.

The project was developed as a learning exercise to understand MVVM architecture, dependency injection, localization, theming, validation, and unit testing.

## Features
- MVVM architecture using CommunityToolkit.Mvvm
- Dependency Injection for service management
- Weather data retrieval using service layer
- Localization support (English and Hindi)
- Theme switching (Light and Dark mode)
- Input validation for city search
- Status messages for success and error scenarios
- Unit testing using xUnit and NSubstitute
- Clean and structured project architecture

## Project Structure
WeatherDeskk
│
├── Models
│   └── WeatherInfo.cs
│
├── Services
│   ├── WeatherService.cs
│   ├── LocalizationService.cs
│   ├── ThemeService.cs
│
├── ViewModels
│   └── MainViewModel.cs
│
├── Views
│   └── MainWindow.xaml
│
└── Resources
    └── Strings.resx


WeatherDeskk.Tests
│
└── MainViewModelTests.cs

## Unit Testing
Unit tests were implemented to validate the behavior of MainViewModel.

Test coverage includes:
- Command enable/disable logic
- Weather data retrieval success scenario
- City not found scenario
- Empty input validation
- Exception handling
- Language switching functionality
- Theme switching functionality

Frameworks used:
- xUnit
- NSubstitute (mocking library)

Total Test Cases: 11

## Technologies Used
- C#
- WPF
- MVVM Pattern
- CommunityToolkit.Mvvm
- Dependency Injection
- xUnit
- NSubstitute
- Visual Studio

## Purpose
This project was created to practice:
- MVVM architecture implementation
- Service layer design
- Localization in WPF applications
- Writing unit test cases for ViewModel logic
- Mocking dependencies using NSubstitute
- Structuring maintainable and testable code

## How to Run
1. Clone the repository
2. Open solution in Visual Studio
3. Build the solution
4. Run the application
5. Execute tests via Test Explorer

## Author
Anish Mehta
