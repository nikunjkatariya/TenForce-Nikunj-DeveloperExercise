# TenForce Developer Test and Taste Exercise

## Overview
This project is an extension of an existing application that interacts with a public API to gather and process data about objects in our Solar System. It currently displays information about planets and their moons on the console and writes this data to a file.

The goal of this exercise is to enhance the application by adding new functionality related to the average temperature of moons.

## Public API
The application uses data from the [Solar System API](https://api.le-systeme-solaire.net/en/), focusing specifically on "bodies" data, which includes planets and their moons.

API documentation and exploration tools are available at the [Swagger Interface](https://api.le-systeme-solaire.net/swagger/).

## Exercise Objectives
- List all planets that have at least one moon. Calculate and display the average temperature of those moons per planet.

## Project Setup
- A local Git repository is used for version control.
- The main development happens on a branch named `TenForce-Nikunj`.
- The project is built as a .NET console application written in C#.

## Code Structure
- **Data Access:** Classes responsible for fetching data from the API.
- **Data Objects:** Classes representing planets, moons, and related data structures.
- **Data Handling:** Logic to process the data, calculate averages, and manage outputs.

## Usage
- The application fetches bodies data from the API.
- It outputs a list of planets with moons and their average moon temperatures to the console.
- During execution, the application informs the user of its progress of API call.
 
## Development Practices
- Regular Git commits are made to ensure progress is tracked and documented.
- Open-source libraries and NuGet packages can be utilized to aid development.

## Notes
- The focus of this exercise is to demonstrate problem-solving skills and clear code organization, rather than optimizing performance or completing the entire feature set.
- For any questions or clarifications, contact the TenForce team via email.

## How to Run
1. Clone the repository.
2. Checkout the `TenForce-Nikunj` branch.
3. Open the solution in Visual Studio or your preferred .NET IDE.
4. Build and run the console project.
5. Follow on-screen instructions and view the output in the console and output file.

---

Thank you for reviewing this project!

