using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJobSdkdemoapp345
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log)
        {
            log.WriteLine(message);
            Console.WriteLine(message);
        }

        public static void ProcessEmployeeMessage([QueueTrigger("employeequeue")] Employee emp,
                [Blob("employeeresumes/{Id}.txt")] TextWriter writer)
        {
            Console.WriteLine(emp.Id + " " + emp.Name + " " + emp.Salary);
            writer.WriteLine(emp.Id + " " + emp.Name + " " + emp.Salary);
        }

        public static void CopyBlobPOCO(
         [QueueTrigger("updateemployeeresumesqueue")] Employee blobInfo,
         [Blob("employeeresumes/{Id}.txt", FileAccess.Read)] Stream blobInput,
         [Blob("employeemodifiedresumes/{Name}.txt", FileAccess.Write)] Stream blobOutput)
        {
            blobInput.CopyTo(blobOutput, 4096);
        }
    }
}
