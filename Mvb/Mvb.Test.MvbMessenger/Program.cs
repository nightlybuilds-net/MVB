using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvb.Test.MvbMessenger
{
    class Program
    {
        static void Main(string[] args)
        {
            var sender = new Sender();
            var receiver = new Receiver();
            receiver.Subscribe();
            sender.Send();
            Console.ReadLine();
            Console.WriteLine("Trying unsubscribe from main thread");
            receiver.Unsubscribe();
            Console.WriteLine("Send again");
            sender.Send();
            Console.WriteLine("No received (i hope)");
            Console.ReadLine();
            Console.WriteLine("Subscribe again and send");
            receiver.Subscribe();
            sender.Send();
            Console.WriteLine("I Received (i hope)");
            Console.ReadLine();
            Console.WriteLine("Trying unsubscribe from another thread");
            var t = Task.Run(() => receiver.Unsubscribe());
            t.Wait();
            Console.WriteLine("Send again");
            sender.Send();
            Console.WriteLine("No received (i hope)");
            Console.ReadLine();


        }
    }
    internal class Sender
    {
        public void Send()
        {
            Core.Components.MvbMessenger.Send(this, "hi");
        }
    }

    internal class Receiver
    {
        

        public void Subscribe()
        {
            Core.Components.MvbMessenger.Subscribe<Sender>(this,"hi",sender => Console.WriteLine("I Received hi!"));

        }

        public void Unsubscribe()
        {
            Core.Components.MvbMessenger.Unsubscribe<Sender>(this, "hi");
        }
    }
}
