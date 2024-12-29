using System;
using System.Collections.Generic;

public interface IObserver
{
    void Update(string message);
}

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify(string message);
}

public class NotificationService : ISubject
{
    private readonly List<IObserver> _observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
        Console.WriteLine("Observer added.");
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
        Console.WriteLine("Observer removed.");
    }

    public void Notify(string message)
    {
        foreach (var observer in _observers)
        {
            observer.Update(message);
        }
    }
}

public class EmailSubscriber : IObserver
{
    public void Update(string message)
    {
        Console.WriteLine($"Email Subscriber received: {message}");
    }
}

public class SmsSubscriber : IObserver
{
    public void Update(string message)
    {
        Console.WriteLine($"SMS Subscriber received: {message}");
    }
}

class ConsoleApp4
{
    static void Main(string[] args)
    {
        // Subject
        var notificationService = new NotificationService();

        // Observers
        var emailSubscriber = new EmailSubscriber();
        var smsSubscriber = new SmsSubscriber();

        notificationService.Attach(emailSubscriber);
        notificationService.Attach(smsSubscriber);

        notificationService.Notify("New notification available!");

        notificationService.Detach(emailSubscriber);

        notificationService.Notify("Another notification!");

        Console.ReadLine();
    }
}
