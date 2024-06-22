# Console Application for Parking System Management Using .NET

A console application in C# and .NET 6.0 for managing a virtual parking system. Allows users to allocate parking slots, park vehicles based on registration number and color, and generate various parking reports.

## Requirements
- .NET 6.0 SDK
- JetBrains Rider 2024.1.3

## Installation
- Clone this repository
  ```bash
  git clone https://github.com/snditrie/Parking-System.git
  ```
- Navigate to the project directory
  ```bash
  cd Parking-System
  ```

## Usage
- Build the project
  ```bash
  dotnet build
  ```
- Run the application
  ```bash
  dotnet run
  ```
- Follow the on-screen instruction to use different commands like `park`, `leave`, `status`, etc.

## Command Input Format
### Create a parking lot
```bash
create_parking_lot <totalLot>
```
### Park a Vehicle
```bash
park <resgistrationNo> <color> <vehicle>
```
### Leave Parking Lot
```bash
leave <slotNumber>
```
### Show Parking Lot Status
```bash
status
```
### Show Total Vehicle By Type
```bash
type_of_vehicles <vehicle>
```
### Show Registration Number By Odd Plate
```bash
registration_numbers_for_vehicles_with_odd_plate
```
### Show Resgistration Number By Even Plate
```bash
registration_numbers_for_vehicles_with_event_plate
```
### Show Resgistration Number By Color
```bash
registration_numbers_for_vehicles_with_color <color>
```
### Show Slot Number By Color
```bash
slot_numbers_for_vehicles_with_color <color>
```
### Show Slot Number By Registration Number
```bash
slot_number_for_registration_number <registrationNo>
```
### Exit Command
```bash
exit
```
