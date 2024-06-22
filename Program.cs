using System;
using System.Collections.Generic;
using System.Linq;

namespace parking_system
{
    class Program
    {
        static Dictionary<int, Vehicle> parkingLot = new Dictionary<int, Vehicle>();
        static int totalLots = 0;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("$ ");
                string input = Console.ReadLine();
                string[] command = input.Split(' ');

                switch (command[0].ToLower())
                {
                    case "create_parking_lot":
                        CreateParkingLot(command);
                        break;
                    case "park":
                        ParkVehicle(command);
                        break;
                    case "leave":
                        LeaveParkingLot(command);
                        break;
                    case "status":
                        GetParkingStatus();
                        break;
                    case "type_of_vehicles":
                        CountVehiclesByType(command);
                        break;
                    case "registration_numbers_for_vehicles_with_odd_plate":
                        GetVehiclesByOddPlate();
                        break;
                    case "registration_numbers_for_vehicles_with_event_plate":
                        GetVehiclesByEvenPlate(command);
                        break;
                    case "registration_numbers_for_vehicles_with_color":
                        GetVehiclesByColor(command);
                        break;
                    case "slot_numbers_for_vehicles_with_color":
                        GetSlotsByColor(command);
                        break;
                    case "slot_number_for_registration_number":
                        GetSlotByRegistrationNumber(command);
                        break;
                    case "report":
                        GetParkingReport();
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }

        class Vehicle
        {
            public string RegistrationNumber { get; }
            public string Color { get; }
            public string Type { get; }

            public Vehicle(string registrationNumber, string color, string type)
            {
                RegistrationNumber = registrationNumber;
                Color = color;
                Type = type;
            }
        }
        static void CreateParkingLot(string[] command)
        {
            if (int.TryParse(command[1], out totalLots))
            {
                Console.WriteLine($"Created a parking lot with {totalLots} slots");
            }
            else
            {
                PrintErrorMessage("Invalid number of slots");
            }
        }

        static void ParkVehicle(string[] command)
        {
            if (totalLots == 0)
            {
                PrintErrorMessage("Parking lot is not created");
                return;
            }

            if (parkingLot.Count < totalLots)
            {
                if (parkingLot.Any(v => v.Value.RegistrationNumber.Equals(command[1], StringComparison.OrdinalIgnoreCase)))
                {
                    PrintErrorMessage("Vehicle is already parked");
                    return;
                }

                int slotNumber = 0;
                for (int i = 1; i <= totalLots; i++)
                {
                    if (!parkingLot.ContainsKey(i))
                    {
                        slotNumber = i;
                        break;
                    }
                }

                string registrationNumber = command[1];
                string color = command[2];
                string type = command[3];
                
                if (!IsValidVehicleType(type))
                {
                    PrintErrorMessage("Invalid type of vehicle");
                    return;
                }

                parkingLot.Add(slotNumber, new Vehicle(registrationNumber, color, type));
                Console.WriteLine($"Allocated slot number: {slotNumber}");
            }
            else
            {
                PrintErrorMessage("Sorry, parking lot is full");
            }
        }

        static void LeaveParkingLot(string[] command)
        {
            int slotNumber = int.Parse(command[1]);
            if (parkingLot.ContainsKey(slotNumber))
            {
                parkingLot.Remove(slotNumber);
                Console.WriteLine($"Slot number {slotNumber} is free");
            }
            else
            {
                PrintErrorMessage($"Slot number {slotNumber} not found");
            }
        }

        static void GetParkingStatus()
        {
            Console.WriteLine("Slot No.  Type    Registration No.  Color");
            foreach (var kvp in parkingLot)
            {
                Console.WriteLine($"{kvp.Key,-9} {kvp.Value.Type,-7} {kvp.Value.RegistrationNumber,-17} {kvp.Value.Color}");
            }
        }

        static void CountVehiclesByType(string[] command)
        {
            string type = command[1];
            int count = parkingLot.Count(v => v.Value.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(count);
        }

        static void GetVehiclesByOddPlate()
        {
            var oddPlates = parkingLot.Where(v => v.Value.RegistrationNumber.Split('-')[1].Last() % 2 != 0)
                .Select(v => v.Value.RegistrationNumber);
            Console.WriteLine(string.Join(", ", oddPlates));
        }

        static void GetVehiclesByEvenPlate(string[] command)
        {
            var evenPlates = parkingLot.Where(v => v.Value.RegistrationNumber.Split('-')[1].Last() % 2 == 0)
                .Select(v => v.Value.RegistrationNumber);
            Console.WriteLine(string.Join(", ", evenPlates));
        }

        static void GetVehiclesByColor(string[] command)
        {
            string color = command[1];
            var vehicles = parkingLot.Where(v => v.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                .Select(v => v.Value.RegistrationNumber);
            if (vehicles.Count() == 0)
            {
                PrintErrorMessage("Not found");
                return;
            }

            Console.WriteLine(string.Join(", ", vehicles));
        }

        static void GetSlotsByColor(string[] command)
        {
            string color = command[1];
            var slots = parkingLot.Where(v => v.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                .Select(v => v.Key.ToString());
            if (slots.Count() == 0)
            {
                PrintErrorMessage("Not found");
                return;
            }

            Console.WriteLine(string.Join(", ", slots));
        }

        static void GetSlotByRegistrationNumber(string[] command)
        {
            string registrationNumber = command[1];
            var slot = parkingLot.FirstOrDefault(v => v.Value.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));
            if (slot.Key != 0)
            {
                Console.WriteLine(slot.Key);
            }
            else
            {
                PrintErrorMessage("Not found");
            }
        }

        static void GetParkingReport()
        {
            int occupiedSlots = parkingLot.Count;
            Console.WriteLine($"Number of occupied slots: {occupiedSlots}");

            int availableSlots = totalLots - occupiedSlots;
            Console.WriteLine($"Number of available slots: {availableSlots}");

            int oddPlates = parkingLot.Count(v => v.Value.RegistrationNumber.Split('-')[1].Last() % 2 != 0);
            Console.WriteLine($"Number of vehicles with odd registration number: {oddPlates}");

            int evenPlates = parkingLot.Count(v => v.Value.RegistrationNumber.Split('-')[1].Last() % 2 == 0);
            Console.WriteLine($"Number of vehicles with even registration number: {evenPlates}");

            int motor = parkingLot.Count(v => v.Value.Type.Equals("Motor", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine($"Number of Motor vehicles: {motor}");
            int mobil = parkingLot.Count(v => v.Value.Type.Equals("Mobil", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine($"Number of Mobil vehicles: {mobil}");

            var colors = parkingLot.GroupBy(v => v.Value.Color)
                .Select(v => new { Color = v.Key, Count = v.Count() });
            foreach (var color in colors)
            {
                Console.WriteLine($"Number of {color.Color} vehicles: {color.Count}");
            }
        }
        
        static void PrintErrorMessage(string message)
        {
            Console.WriteLine(message);
        }
        
        static bool IsValidVehicleType(string type)
        {
            return type.Equals("Mobil", StringComparison.OrdinalIgnoreCase) ||
                   type.Equals("Motor", StringComparison.OrdinalIgnoreCase);
        }
    }
}
