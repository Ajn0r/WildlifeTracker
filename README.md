# Wildlife Tracker

WildlifeTracker is a simple application for tracking and managing information about various animals. The application allows users to add new animals, view details about a single animal, and track different species of animals.

This project is part of the Programming in C# 2 course at Malmö University.

## Features
- Add Animals: Users can add new animals by specifying their name, age, gender, color, and other specific attributes of the animal.
- View Animal Information: It is possible to view detailed information about a single animal, including its name, age, gender, color, and other attributes.
- Animal Categorization: Animals can be categorized as mammals or birds, and specific properties can be loaded based on the selected category.
- GUI Updates: The user interface dynamically updates depending on the selected category and other user actions.
- Limited Image Support: Users can add images for animals, but the image functionality is limited and supports only certain image formats.

## Usage
To use WildlifeTracker, follow these steps:

1. Start the application by running the main program (MainWindow.xaml.cs).
2. Choose a category (Mammal or Bird) for the animal you want to add.
3. Fill in all required fields and specify the desired attributes for the animal.
4. Press the "Add Animal" button to save the animal.
5. If there are any errors or required fields left unfilled, an error message dialog will appear with information about the issue.
6. To view details about an animal, click the "View Animal" button. The details will be displayed in a new window.

## Development
The project is developed using C# and WPF (Windows Presentation Foundation) for the user interface. It follows a simple modular structure to handle different animal categories and their attributes.
