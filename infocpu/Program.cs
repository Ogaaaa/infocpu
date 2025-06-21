using System;
using LibreHardwareMonitor.Hardware;

class Program
{
    static void Main()
    {
        Computer computer = new Computer
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
            IsMemoryEnabled = true,
            IsMotherboardEnabled = true
        };

        computer.Open();

        string mainboardName = "", cpuName = "", ramName = "";
        string gpu1Name = "", gpu2Name = "";
        float? cpuTemp = null, gpu1Temp = null, gpu2Temp = null;

        foreach (IHardware hardware in computer.Hardware)
        {
            hardware.Update();

            switch (hardware.HardwareType)
            {
                case HardwareType.Motherboard:
                    mainboardName = hardware.Name;
                    break;

                case HardwareType.Cpu:
                    cpuName = hardware.Name;
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature && cpuTemp == null)
                            cpuTemp = sensor.Value;
                    }
                    break;

                case HardwareType.Memory:
                    ramName = hardware.Name;
                    break;

                case HardwareType.GpuNvidia:
                case HardwareType.GpuAmd:
                    gpu1Name = hardware.Name;
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature && gpu1Temp == null)
                            gpu1Temp = sensor.Value;
                    }
                    break;

                case HardwareType.GpuIntel:
                    gpu2Name = hardware.Name;
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature && gpu2Temp == null)
                            gpu2Temp = sensor.Value;
                    }
                    break;
            }
        }

        // In ra thông tin
        Console.WriteLine("Mainboard: " + mainboardName);
        Console.WriteLine("CPU: " + cpuName);
        Console.WriteLine("CPU Temp: " + (cpuTemp.HasValue ? $"{cpuTemp.Value} °C" : "N/A"));
        Console.WriteLine("RAM: " + ramName);
        Console.WriteLine("GPU 1: " + gpu1Name);
        Console.WriteLine("GPU 1 Temp: " + (gpu1Temp.HasValue ? $"{gpu1Temp.Value} °C" : "N/A"));
        Console.WriteLine("GPU 2: " + gpu2Name);
        Console.WriteLine("GPU 2 Temp: " + (gpu2Temp.HasValue ? $"{gpu2Temp.Value} °C" : "N/A"));

        Console.WriteLine("\nEnter de thoat!!....");
        Console.ReadKey();
        computer.Close();
    }
}
